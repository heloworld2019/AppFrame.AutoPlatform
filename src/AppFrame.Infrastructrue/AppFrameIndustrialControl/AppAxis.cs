using AppFrame.Common.Extensions.ValidateAttribute;
using AppFrame.Core.AppFrameElement.Models;
using AppFrame.Core.AppFrameException.MotionAxisException;
using AppFrame.Core.AppFrameIndustrialControl.Interfaces;
using AppFrame.Entity.Enum;
using AppFrame.Infrastructrue.AppFrameIndustrialControl.Helper;
using log4net.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace AppFrame.Infrastructrue.AppFrameIndustrialControl
{
    internal class AppAxis : IAppAxis
    {
        public AppAxis(AppAxisElement appAxisElement)
        {
            if (appAxisElement is null)
            {
                throw new ArgumentNullException(nameof(appAxisElement));
            }
            if (!string.IsNullOrWhiteSpace(appAxisElement.Validate()))
            {
                throw new MotionAxisConfigException();
            }
            AppAxisConfig = appAxisElement;
            SetPulseEquiv();
        }

        #region 字段
        private static readonly object _lock = new object();
        private CancellationTokenSource _locationToken;
        private CancellationTokenSource _homeToken;
        private bool _isRelative;
        private bool _isAbsolute;
        private bool _isHome;
        private bool _isJogP;
        private bool _isJogN;
        #endregion

        #region 属性
        public AppAxisElement AppAxisConfig { get; private set; }

        public bool MoveDone { get; private set; }

        public bool HomeDone { get; private set; }

        public bool IsPowerOn { get; private set; }

        public bool PositiveLimitSwitch { get; internal set; }

        public bool NegitiveLimitSwitch { get; internal set; }

        public bool HomeSwitch { get; internal set; }

        public bool PositiveSoftLimitSwitch { get; internal set; }

        public bool NegitiveSoftLimitSwitch { get; internal set; }

        public MotionAxisStateMachine AxisState { get; internal set; }

        public short ErrorCode { get; internal set; }

        public int StatuWord { get; internal set; }

        public int ControlWord { get; internal set; }

        public bool IsAlarming { get; internal set; }

        public bool IsErrorStop { get; internal set; }

        public double CurrentVelocity { get; internal set; }

        public double CurrentPosition { get; internal set; }

        public double CurrentPulsePosition { get; internal set; }

        public string ErrorMessage { get; private set; }
        #endregion

        /// <summary>
        /// 设置脉冲当量
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetPulseEquiv()
        {
            lock (_lock)
            { 
                if (IsPowerOn)
                {
                    ErrorMessage = $"请断开使能后再操作";
                    return ;
                }
                ErrorCode = LeadshineMotionApi.dmc_set_equiv(AppAxisConfig.CardId, AppAxisConfig.CardId, AppAxisConfig.PulseEquiv);  //设置脉冲当量
                if (ErrorCode != 0)
                {
                    ErrorMessage = $"设置脉冲当量指令错误,错误码:{ErrorCode}";
                }
            }           
        }
        /// <summary>
        /// 回零 [可等待]
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> HomeAsync()
        {
            if (!IsPowerOn)
            {
                ErrorMessage = $"请使能后再操作";
                return false;
            }
            if (_isJogP || _isJogN || _isRelative || _isAbsolute || _isHome)
            {
                ErrorMessage = $"轴正在定位中,请勿重复操作!";
                return false;
            }
            _isHome = true;
            HomeDone = false;
            //设置回零参数
            ErrorCode = LeadshineMotionApi.nmc_set_home_profile(AppAxisConfig.CardId, AppAxisConfig.AxisId, AppAxisConfig.HomeMode, AppAxisConfig.HomeLowVelocity,
                                                    AppAxisConfig.HomeHighVelocity, AppAxisConfig.RunAcc, AppAxisConfig.RunAcc, AppAxisConfig.HomeOffset);
            if (ErrorCode != 0)
            {
                ErrorMessage = $"设置回零参数指令错误,错误码:{ErrorCode}";
                return false;
            }
            //启动回零
            ErrorCode = LeadshineMotionApi.nmc_home_move(AppAxisConfig.CardId, AppAxisConfig.AxisId);
            if (ErrorCode != 0)
            {
                ErrorMessage = $"启动回零指令错误,错误码:{ErrorCode}";
                return false;
            }
            _homeToken = new CancellationTokenSource();
            await Task.Factory.StartNew(ReadHomeStatu, _homeToken.Token);
            return true;
        }
        /// <summary>
        /// 回零
        /// </summary>
        public void Home()
        {
            lock (_lock)
            {
                if (!IsPowerOn)
                {
                    ErrorMessage = $"请使能后再操作";
                    return;
                }
                if (_isJogP || _isJogN || _isRelative || _isAbsolute || _isHome)
                {
                    ErrorMessage = $"轴正在定位中,请勿重复操作!";
                    return;
                }
                _isHome = true;
                HomeDone = false;
                //设置回零参数
                ErrorCode = LeadshineMotionApi.nmc_set_home_profile(AppAxisConfig.CardId, AppAxisConfig.AxisId, AppAxisConfig.HomeMode, AppAxisConfig.HomeLowVelocity,
                                                        AppAxisConfig.HomeHighVelocity, AppAxisConfig.RunAcc, AppAxisConfig.RunAcc, AppAxisConfig.HomeOffset);
                if (ErrorCode != 0)
                {
                    ErrorMessage = $"设置回零参数指令错误,错误码:{ErrorCode}";
                    return;
                }
                //启动回零
                ErrorCode = LeadshineMotionApi.nmc_home_move(AppAxisConfig.CardId, AppAxisConfig.AxisId);
                if (ErrorCode != 0)
                {
                    ErrorMessage = $"启动回零指令错误,错误码:{ErrorCode}";
                    return;
                }
                _homeToken = new CancellationTokenSource();
                Task.Factory.StartNew(ReadHomeStatu, _homeToken.Token);
            } 
        }
        /// <summary>
        /// 读取回零状态
        /// </summary>
        private void ReadHomeStatu()
        {
            var startTime = DateTime.Now;
            while (!_homeToken.IsCancellationRequested)
            {
                var res = LeadshineMotionApi.dmc_check_done(AppAxisConfig.CardId, AppAxisConfig.AxisId);
                if (res == 0) continue;
                var span = DateTime.Now.Subtract(startTime);
                if (span.TotalSeconds >= AppAxisConfig.HomeTimeOut)
                {
                    ErrorMessage = $"回零超时";
                    HomeDone = false;
                    _isHome = false;
                    _homeToken.Cancel();
                    break;
                }
                ushort homeState = 0;
                res = LeadshineMotionApi.dmc_get_home_result(AppAxisConfig.CardId, AppAxisConfig.AxisId, ref homeState);
                if (res == 0 && homeState == 1)
                {
                    HomeDone = true;
                    _isHome = false;
                    _homeToken.Cancel();
                    break;
                }
                else
                {
                    ErrorMessage = $"回零失败,错误码:{ErrorCode}";
                    HomeDone = false;
                    _isHome = false;
                    _homeToken.Cancel();
                    break;
                }
            }
        }
        /// <summary>
        /// 绝对定位 [可等待]
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> AbsoluteMoveAsync(double position)
        {
            if (!IsPowerOn)
            {
                ErrorMessage = $"请使能后再操作";
                return false;
            }
            if (_isAbsolute || _isRelative || _isJogP || _isJogN || _isHome)
            {
                ErrorMessage = $"轴正在定位中,请勿重复操作!";
                return false;
            }
            if (!HomeDone)
            {
                ErrorMessage = $"轴未回零完成,无法定位!";
                return false;
            }
            _isAbsolute = true;
            MoveDone = false;
            if (!Locate(position, true))
            {
                ErrorMessage = $"设置定位参数错误或定位出错,错误码:{ErrorCode}";
                return false;
            }
            _locationToken = new CancellationTokenSource();
            await Task.Factory.StartNew(ReadMoveStatu, _locationToken.Token);
            return true;
        }
        /// <summary>
        /// 相对定位 [可等待]
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool>  RelativeMoveAsync(double distance)
        {
            if (!IsPowerOn)
            {
                ErrorMessage = $"请使能后再操作";
                return false;
            }
            if (_isAbsolute || _isRelative || _isJogP || _isJogN || _isHome)
            {
                ErrorMessage = $"轴正在定位中,请勿重复操作!";
                return false;
            }
            if (!HomeDone)
            {
                ErrorMessage = $"轴未回零完成,无法定位!";
                return false;
            }
            _isRelative = true;
            MoveDone = false;
            if (!Locate(distance, true))
            {
                ErrorMessage = $"设置定位参数错误或定位出错,错误码:{ErrorCode}";
                return false;
            }
            _locationToken = new CancellationTokenSource();
            await Task.Factory.StartNew(ReadMoveStatu, _locationToken.Token);
            return true;
        }
        /// <summary>
        /// 绝对定位
        /// </summary>
        /// <param name="position"></param>
        public void AbsoluteMove(double position)
        {
            lock (_lock)
            {
                if (!IsPowerOn)
                {
                    ErrorMessage = $"请使能后再操作";
                    return ;
                }
                if (_isAbsolute || _isRelative || _isJogP || _isJogN || _isHome)
                {
                    ErrorMessage = $"轴正在定位中,请勿重复操作!";
                    return;
                }
                if (!HomeDone)
                {
                    ErrorMessage = $"轴未回零完成,无法定位!";
                    return;
                }
                _isAbsolute = true;
                MoveDone = false;
                if (!Locate(position, true))
                {
                    ErrorMessage = $"设置定位参数错误或定位出错,错误码:{ErrorCode}";
                    return;
                }
                _locationToken = new CancellationTokenSource();
                Task.Factory.StartNew(ReadMoveStatu, _locationToken.Token);
            }
        }
        /// <summary>
        /// 相对定位
        /// </summary>
        /// <param name="distance"></param>
        public void RelativeMove(double distance)
        {
            lock (_lock)
            {
                if (!IsPowerOn)
                {
                    ErrorMessage = $"请使能后再操作";
                    return;
                }
                if (_isAbsolute || _isRelative || _isJogP || _isJogN || _isHome)
                {
                    ErrorMessage = $"轴正在定位中,请勿重复操作!";
                    return;
                }
                if (!HomeDone)
                {
                    ErrorMessage = $"轴未回零完成,无法定位!";
                    return;
                }
                _isRelative = true;
                MoveDone = false;
                if (!Locate(distance, true))
                {
                    ErrorMessage = $"设置定位参数错误或定位出错,错误码:{ErrorCode}";
                    return;
                }
                _locationToken = new CancellationTokenSource();
                Task.Factory.StartNew(ReadMoveStatu, _locationToken.Token);
            }
        }
        /// <summary>
        /// 定位
        /// </summary>
        private bool Locate(double value, bool isAbs)
        {
            ErrorCode = LeadshineMotionApi.dmc_set_profile_unit(AppAxisConfig.CardId, AppAxisConfig.AxisId, AppAxisConfig.StartVelocity,
                                                            AppAxisConfig.RunVelocity, AppAxisConfig.RunAcc, AppAxisConfig.RunDec, AppAxisConfig.StopVelocity);//设置速度参数
            if (ErrorCode != 0) return false;
            ErrorCode = LeadshineMotionApi.dmc_set_s_profile(AppAxisConfig.CardId, AppAxisConfig.AxisId, 0, AppAxisConfig.StopTime);//设置S段速度参数
            if (ErrorCode != 0) return false;
            ErrorCode = LeadshineMotionApi.dmc_set_dec_stop_time(AppAxisConfig.CardId, AppAxisConfig.AxisId, AppAxisConfig.RunDec); //设置减速停止时间
            if (ErrorCode != 0) return false;
            ErrorCode = LeadshineMotionApi.dmc_pmove_unit(AppAxisConfig.CardId, AppAxisConfig.AxisId, value, isAbs ? (ushort)1 : (ushort)0);//定长运动
            if (ErrorCode != 0) return false;
            return true;
        }
        /// <summary>
        /// 读取定位状态
        /// </summary>
        private void ReadMoveStatu()
        {
            var startTime = DateTime.Now;
            while (!_locationToken.IsCancellationRequested)
            {
                var res = LeadshineMotionApi.dmc_check_done(AppAxisConfig.CardId, AppAxisConfig.AxisId);
                var span = DateTime.Now.Subtract(startTime);
                if (span.TotalSeconds >= AppAxisConfig.LocationTimeOut)
                {
                    ErrorMessage = $"定位超时,错误码:{ErrorCode}";
                    MoveDone = false;
                    if (_isAbsolute) _isAbsolute = false;
                    if (_isRelative) _isRelative = false;
                    _locationToken.Cancel();
                    break;
                }
                if (res == 1)
                {
                    MoveDone = true;
                    if (_isAbsolute) _isAbsolute = false;
                    if (_isRelative) _isRelative = false;
                    _locationToken.Cancel();
                    break;
                }
                Thread.Sleep(4);
            }
        }
        /// <summary>
        /// 正寸动
        /// </summary>
        /// <param name="inchValue"></param>
        public void PositiveInch(double inchValue)
        {
            RelativeMove(inchValue);
        }
        /// <summary>
        /// 负寸动
        /// </summary>
        /// <param name="inchValue"></param>
        public void NegativeInch(double inchValue)
        {
            RelativeMove(inchValue);
        }
        /// <summary>
        /// 正点动
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void PositiveJog()
        {
            lock (_lock)
            {
                if (!IsPowerOn)
                {
                    ErrorMessage = $"请使能后再操作";
                    return;
                }
                if (_isJogN || _isRelative || _isAbsolute || _isHome)
                {
                    ErrorMessage = $"轴正在定位中,请勿重复操作!";
                    return;
                }
                _isJogP = true;
                if (!Jog(true))
                {
                    ErrorMessage = $"设置点动参数失败或点动出错,错误码:{ErrorCode}";
                }
                _isJogP = false;
            }            
        }
        /// <summary>
        /// 负点动
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void NegativeJog()
        {
            lock (_lock)
            {
                if (!IsPowerOn)
                {
                    ErrorMessage = $"请使能后再操作";
                    return;
                }
                if (_isJogP || _isRelative || _isAbsolute || _isHome)
                {
                    ErrorMessage = $"轴正在定位中,请勿重复操作!";
                    return;
                }
                _isJogN = true;
                if (!Jog(false))
                {
                    ErrorMessage = $"设置点动参数失败或点动出错,错误码:{ErrorCode}";
                }
                _isJogN = false;
            }        
        }
        /// <summary>
        /// 点动
        /// </summary>
        private bool Jog(bool dir)
        {
            ErrorCode = LeadshineMotionApi.dmc_set_profile_unit(AppAxisConfig.CardId, AppAxisConfig.AxisId, AppAxisConfig.StartVelocity,
                                                    AppAxisConfig.JogVelocity, AppAxisConfig.JogAcc, AppAxisConfig.JogDec, AppAxisConfig.StopVelocity);//设置速度参数
            if (ErrorCode != 0) return false;
            ErrorCode = LeadshineMotionApi.dmc_set_s_profile(AppAxisConfig.CardId, AppAxisConfig.AxisId, 0, AppAxisConfig.StopTime);//设置S段速度参数
            if (ErrorCode != 0) return false;
            ErrorCode = LeadshineMotionApi.dmc_set_dec_stop_time(AppAxisConfig.CardId, AppAxisConfig.AxisId, AppAxisConfig.JogDec); //设置减速停止时间
            if (ErrorCode != 0) return false;
            ErrorCode = LeadshineMotionApi.dmc_vmove(AppAxisConfig.CardId, AppAxisConfig.AxisId, dir ? (ushort)1 : (ushort)0);//连续运动
            if (ErrorCode != 0) return false;
            return true;
        }
        /// <summary>
        /// 失能
        /// </summary>
        public void PowerOff()
        {
            lock (_lock)
            {
                ErrorCode = LeadshineMotionApi.nmc_set_axis_disable(AppAxisConfig.CardId, AppAxisConfig.AxisId);
                if (ErrorCode != 0)
                {
                    ErrorMessage = $"失能指令错误,错误码:{ErrorCode}";
                    return;
                }
                IsPowerOn = false;
            }     
        }
        /// <summary>
        /// 使能
        /// </summary>
        public void PowerOn()
        {
            lock (_lock)
            {
                ErrorCode = LeadshineMotionApi.nmc_set_axis_enable(AppAxisConfig.CardId, AppAxisConfig.AxisId);
                if (ErrorCode != 0)
                {
                    ErrorMessage = $"使能指令错误,错误码:{ErrorCode}";
                    return;
                }
                IsPowerOn = true;
            } 
        }
        /// <summary>
        /// 清除轴错误
        /// </summary>
        public void Reset()
        {
            lock (_lock)
            {
                ErrorCode = LeadshineMotionApi.nmc_clear_axis_errcode(AppAxisConfig.CardId, AppAxisConfig.AxisId);//清除轴错误
                ErrorMessage = ErrorCode == 0 ? "" : $"清除指令错误,错误码:{ErrorCode}";
            }      
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public void Stop()
        {
            lock (_lock)
            {
                ErrorCode = LeadshineMotionApi.dmc_stop(AppAxisConfig.CardId, AppAxisConfig.AxisId, 0);
                if (ErrorCode != 0)
                {
                    ErrorMessage = $"停止指令错误,错误码:{ErrorCode}";
                }
            }   
        }
        /// <summary>
        /// 停止jog
        /// </summary>
        public void StopJog()
        {
            Stop();
        }
        /// <summary>
        /// 读取对象字典
        /// </summary>
        /// <param name="index"></param>
        /// <param name="subindex"></param>
        /// <param name="length"></param>
        /// <param name="value"></param>
        public void ReadObjectDictionary(string index, string subindex, ushort length, ref int value)
        {
            lock (_lock)
            {
                ushort.TryParse(index, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out ushort _index);
                ushort.TryParse(subindex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out ushort _subindex);
                ErrorCode = LeadshineMotionApi.nmc_get_node_od(AppAxisConfig.CardId, 2, (ushort)(1000 + AppAxisConfig.AxisId), _index, _subindex, length, ref value);
                if (ErrorCode != 0)
                {
                    ErrorMessage = $"读取对象字典出错,错误码:{ErrorCode}";
                }
            }  
        }
        /// <summary>
        /// 写入对象字典
        /// </summary>
        /// <param name="index"></param>
        /// <param name="subindex"></param>
        /// <param name="length"></param>
        /// <param name="value"></param>
        public void WriteObjectDictionary(string index, string subindex, ushort length, int value)
        {
            lock (_lock)
            {
                ushort.TryParse(index, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out ushort _index);
                ushort.TryParse(subindex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out ushort _subindex);
                ErrorCode = LeadshineMotionApi.nmc_set_node_od(AppAxisConfig.CardId, 2, (ushort)(1000 + AppAxisConfig.AxisId), _index, _subindex, length, value);
                if (ErrorCode != 0)
                {
                    ErrorMessage = $"写入对象字典出错,错误码:{ErrorCode}";
                }
            }    
        }
    }
}
