using AppFrame.Common.Extensions.ValueType;
using AppFrame.Core.AppFrameCommon.Events;
using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameCommon.Global.Events;
using AppFrame.Core.AppFrameException.MotionAxisException;
using AppFrame.Core.AppFrameIndustrialControl.Interfaces;
using AppFrame.Entity.Enum;
using AppFrame.Infrastructrue.AppFrameIndustrialControl.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace AppFrame.Infrastructrue.AppFrameIndustrialControl
{
    internal class AppAxisBus : GlobalObjectManager, IAppAxisBus, IDisposable
    {
        private CancellationTokenSource[] _cancellationTokenSources;
        private CancellationTokenSource _composeCancelToken;
        private CancellationToken[] _tokens;
        private static readonly object _lock = new object();
        public AppAxisBus(ushort cardId)
        {
            CardId = cardId;
            _cancellationTokenSources = new CancellationTokenSource[2];
            _tokens = new CancellationToken[2];
            for (int i = 0; i < 2; i++)
            {
                _cancellationTokenSources[i] = new CancellationTokenSource();
                _tokens[i] = _cancellationTokenSources[i].Token;
            }
            _composeCancelToken = CancellationTokenSource.CreateLinkedTokenSource(_tokens);
            EventBus.Subscribe<AppDisposeEvent, AppDisposeEventArgs>(DisposeCallback);
            //订阅输出控制事件
            EventBus.Subscribe<AppOutputChangedEvent, AppOutputChangedEventArgs>(WriteBitCallback);
        }

        private void WriteBitCallback(object arg1, AppOutputChangedEventArgs arg2)
        {
            if (arg2.Value == null) return;
            WriteOutput(arg2.Value.Index, arg2.Value.Value);
        }
        private void DisposeCallback(object arg1, AppDisposeEventArgs arg2)
        {
            Dispose();
        }

        public bool IsInitialized { get; private set; }

        public bool IsSoftReset { get; private set; }

        public bool IsCoolReset { get; private set; }

        public ushort CardId { get; private set; }

        public MotionAxisBusState MasterState { get; private set; }

        public short BusErrorCode { get; private set; }

        public string BusErrorMessage { get; private set; }

        /// <summary>
        /// 初始化 总线卡
        /// </summary>
        public async Task InitMotionCardAsync()
        {
            IsInitialized = false;
            var cardNums = LeadshineMotionApi.dmc_board_init();
            if (cardNums <= 0 || cardNums > 8)
            {
                throw new MotionAxisInitFaildException();
            }
            ushort _num = 0;
            ushort[] cardIds = new ushort[8];
            uint[] cardTypes = new uint[8];
            BusErrorCode = LeadshineMotionApi.dmc_get_CardInfList(ref _num, cardTypes, cardIds);
            if (BusErrorCode != 0)
            {
                throw new MotionAxisGetCardInfoFaildException();
            }
            var cardExist = cardIds.Any(p => p == CardId);
            if (!cardExist)
            {
                throw new MotionAxisCardNotExistException($"控制卡[卡号：{CardId}]不存在,请检查!");
            }
            ushort err = 0;
            BusErrorCode = LeadshineMotionApi.nmc_get_errcode(CardId, 2, ref err);//获取总线状态
            if (BusErrorCode == 0 && err != 0) //总线报错，需要复位
            {
                var token = new CancellationTokenSource();
                DateTime startTime = DateTime.Now;
                await Task.Run(() =>
                {
                    BusErrorCode = LeadshineMotionApi.dmc_soft_reset(CardId);//复位总线错误
                    if (BusErrorCode != 0) throw new MotionAxisMasterResetFaildException();
                    while (true)
                    {
                        var span = DateTime.Now.Subtract(startTime);
                        if (span.TotalMilliseconds >= 10000.0)//超时退出循环
                        {
                            throw new MotionAxisMasterResetTimeoutException();
                        }
                        BusErrorCode = LeadshineMotionApi.nmc_get_errcode(CardId, 2, ref err);
                        if (BusErrorCode == 0 && err == 0)
                        {
                            break; //复位完成退出
                        }
                    }
                    token.Cancel();
                }, token.Token);
            }
            uint totalAxis = 0;
            BusErrorCode = LeadshineMotionApi.nmc_get_total_axes(CardId, ref totalAxis);
            if (totalAxis <= 0)
            {
                throw new MotionAxisSlaveNotFoundException();
            }
            AppAxises.Clear();
            for (int i = 0; i < (int)totalAxis; i++)
            {
                if (i >= AppConfigs.AppAxisConfigs.Count) break;
                AppConfigs.AppAxisConfigs[i].CardId = CardId;
                //此处根据本地配置的轴ID实例化轴
                var model = AppConfigs.AppAxisConfigs.FirstOrDefault(p => p.AxisId.Equals(i));
                if (model == null) continue;
                var axis = new AppAxis(model);
                AppAxises.Add(axis);
                axis.PowerOn();
            }
            IsInitialized = true;
            await Task.Delay(1000);
            StartAxisStateThread();
        }
        /// <summary>
        /// 启动线程读取轴状态
        /// </summary>
        private void StartAxisStateThread()
        {
            Task.Factory.StartNew(ReadAxisStatus, _composeCancelToken.Token);
            Task.Factory.StartNew(ReadInputStatus, _composeCancelToken.Token);
        }
        /// <summary>
        /// 硬复位
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public async Task CoolResetAsync()
        {
            IsCoolReset = true;
            IsInitialized = false;
            BusErrorCode = LeadshineMotionApi.dmc_board_reset();
            if (BusErrorCode != 0)
            {
                IsCoolReset = false;
                throw new MotionAxisMasterResetFaildException();
            }
            LeadshineMotionApi.dmc_board_close();
            //复位耗时15 s
            await Task.Delay(16000);
            LeadshineMotionApi.dmc_board_init();
            IsInitialized = true;
        }
        /// <summary>
        /// 软复位
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SoftResetAsync()
        {
            IsSoftReset = true;
            IsInitialized = false;
            BusErrorCode = LeadshineMotionApi.dmc_soft_reset(CardId);
            if (BusErrorCode != 0)
            {
                IsSoftReset = false;
                throw new MotionAxisMasterResetFaildException();
            }
            LeadshineMotionApi.dmc_board_close();
            //复位耗时15 s
            await Task.Delay(16000);
            LeadshineMotionApi.dmc_board_init();
            IsInitialized = true;
        }
        /// <summary>
        /// 急停
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void EStop()
        {
            BusErrorCode = LeadshineMotionApi.dmc_emg_stop(CardId);
            BusErrorMessage = BusErrorCode == 0 ? "" : $"急停指令错误,错误码:{BusErrorCode}";
        }
        /// <summary>
        /// 清除总线错误
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void ClearBusError()
        {
            BusErrorCode = LeadshineMotionApi.nmc_clear_errcode(CardId, 2);//清除总线错误
            BusErrorMessage = BusErrorCode == 0 ? "" : $"清除指令错误,错误码:{BusErrorCode}";
        }
        /// <summary>
        /// 输出控制
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void WriteOutput(int index, bool value)
        {
            lock(_lock)
            {
                if (index < 0) throw new ArgumentOutOfRangeException("index");
                LeadshineMotionApi.dmc_write_outbit(CardId, (ushort)index, value ? (ushort)1 : (ushort)0);
            }
        }
        /// <summary>
        /// 读取轴状态
        /// </summary>
        private void ReadAxisStatus()
        {
            while (true)
            {
                if (IsCoolReset) continue;
                if (IsSoftReset) continue;
                foreach (var item in AppAxises)
                {
                    var axis = (AppAxis)item;
                    ushort stateMachine = 0;
                    //轴状态机
                    LeadshineMotionApi.nmc_get_axis_state_machine(CardId, axis.AppAxisConfig.AxisId, ref stateMachine);
                    axis.AxisState = (MotionAxisStateMachine)stateMachine;
                    //总线状态
                    ushort errcode = 0;
                    LeadshineMotionApi.nmc_get_errcode(CardId, 2, ref errcode);
                    MasterState = errcode == 0 ? MotionAxisBusState.Normal : MotionAxisBusState.Error;
                    //轴错误码
                    ushort errorCode = 0;
                    LeadshineMotionApi.nmc_get_axis_errcode(CardId, axis.AppAxisConfig.AxisId, ref errorCode);
                    axis.ErrorCode = (short)errorCode;
                    //轴状态字
                    int statusWord = 0;
                    LeadshineMotionApi.nmc_get_axis_statusword(CardId, axis.AppAxisConfig.AxisId, ref statusWord);
                    axis.StatuWord = statusWord;
                    //轴控制字
                    int controlWord = 0;
                    LeadshineMotionApi.nmc_get_axis_contrlword(CardId, axis.AppAxisConfig.AxisId, ref controlWord);
                    axis.ControlWord = controlWord;
                    //轴运动信号
                    var ioStatu = LeadshineMotionApi.dmc_axis_io_status(CardId, axis.AppAxisConfig.AxisId);
                    axis.IsAlarming = ioStatu.GetBitValue(0);
                    axis.PositiveLimitSwitch = ioStatu.GetBitValue(1);
                    axis.NegitiveSoftLimitSwitch = ioStatu.GetBitValue(2);
                    axis.IsErrorStop = ioStatu.GetBitValue(3);
                    axis.HomeSwitch = ioStatu.GetBitValue(4);
                    axis.PositiveSoftLimitSwitch = ioStatu.GetBitValue(6);
                    axis.NegitiveSoftLimitSwitch = ioStatu.GetBitValue(7);
                    //当前速度
                    double currVel = 0;
                    LeadshineMotionApi.dmc_read_current_speed_unit(CardId, axis.AppAxisConfig.AxisId, ref currVel);
                    axis.CurrentVelocity = currVel;
                    //当前位置
                    double currPos = 0;
                    LeadshineMotionApi.dmc_get_position_unit(CardId, axis.AppAxisConfig.AxisId, ref currPos);
                    axis.CurrentPosition = currPos; //指令位置
                    axis.CurrentPulsePosition = LeadshineMotionApi.dmc_get_position(CardId, axis.AppAxisConfig.AxisId); //脉冲位置
                }
                Thread.Sleep(4);
            }
        }
        /// <summary>
        /// 读取输入状态
        /// </summary>
        private void ReadInputStatus()
        {
            while (true)
            {
                if (IsCoolReset) continue;
                if (IsSoftReset) continue;
                for (int i = 0; i < AppConfigs.AppInput.Count; i++)
                {
                    var res = LeadshineMotionApi.dmc_read_inbit(CardId, (ushort)i);
                    AppConfigs.AppInput[i].Value = res == 1;
                }
                Thread.Sleep(4);
            }
        }
        /// <summary>
        /// 释放资源 关闭卡
        /// </summary>
        public void Dispose()
        {
            _cancellationTokenSources[0].Cancel();
            _cancellationTokenSources = null;
            _composeCancelToken = null;
            _tokens = null;
            LeadshineMotionApi.dmc_board_close();
        }       
    }
}
