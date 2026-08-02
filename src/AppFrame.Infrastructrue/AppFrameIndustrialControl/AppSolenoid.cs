using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameElement.Models;
using AppFrame.Core.AppFrameException.SloenoidException;
using AppFrame.Core.AppFrameIndustrialControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppFrame.Infrastructrue.AppFrameIndustrialControl
{
    internal class AppSolenoid : GlobalObjectManager, IAppSolenoid
    {
        private const int _msUnit = 1000;
        private static readonly object _locker = new object();
        private readonly AppInputElement _goInput;
        private readonly AppInputElement _backInput;
        private readonly AppOutputElement _goOutput;
        private readonly AppOutputElement _backOutput;
        private readonly AppAlarmElement _goAlarm;
        private readonly AppAlarmElement _backAlarm;
        private bool _isGoFlag;
        private bool _isBackFlag;
        private CancellationTokenSource _goToken;
        private CancellationTokenSource _backToken;
        public AppSolenoid(AppSolenoidElement appSolenoidElement)
        {
            if (appSolenoidElement is null)
            {
                throw new ArgumentNullException(nameof(appSolenoidElement));
            }
            AppSolenoidConfig = appSolenoidElement;
            _goInput = AppConfigs.AppInput[AppSolenoidConfig.GoSensorInputId];
            _backInput = AppConfigs.AppInput[AppSolenoidConfig.BackSensorInputId];
            _goOutput = AppConfigs.AppOutput[AppSolenoidConfig.GoOutputId];
            _backOutput = AppConfigs.AppOutput[AppSolenoidConfig.BackOutputId];
            _goAlarm = AppConfigs.AppAlarm[AppSolenoidConfig.GoAlarmId];
            _backAlarm = AppConfigs.AppAlarm[AppSolenoidConfig.BackAlarmId];
            if (_goInput == null || _backInput == null || _goOutput == null || _backOutput == null || _goAlarm == null || _backAlarm == null)
                throw new SolenoidConfigExceptoin();
        }

        public AppSolenoidElement AppSolenoidConfig { get; private set; }

        public bool GoDone { get; private set; }

        public bool BackDone { get; private set; }

        public string TipMessage { get; private set; }

        public void ActionBack()
        {
            lock (_locker)
            {
                if (_isBackFlag || _isGoFlag)
                {
                    TipMessage = $"{AppSolenoidConfig.SolenoidName}正在动作中,请勿重复操作!";
                    return;
                }
                BackDone = false;
                if (_goOutput.Enable)
                    _goOutput.Value = false;
                if (AppSolenoidConfig.IsDoubleCoil)
                    _backOutput.Value = true;
                _backToken = new CancellationTokenSource();
                Task.Run(ReadBackStatu, _backToken.Token);
            }
        }
        public void ActionGo()
        {
            lock (_locker)
            {
                if (_isBackFlag || _isGoFlag)
                {
                    TipMessage = $"{AppSolenoidConfig.SolenoidName}正在动作中,请勿重复操作!";
                    return;
                }
                GoDone = false;
                if (_goOutput.Enable)
                    _goOutput.Value = true;
                if (AppSolenoidConfig.IsDoubleCoil)
                    _backOutput.Value = false;
                _goToken = new CancellationTokenSource();
                Task.Run(ReadGoStatu, _goToken.Token);
            }
        }
        private void ReadGoStatu()
        {
            DateTime _beginTime = DateTime.Now;
            while (!_goToken.IsCancellationRequested)
            {
                if (AppSolenoidConfig.GoSensorShield) //屏蔽感应则使用时间控制
                {
                    Thread.Sleep((int)(AppSolenoidConfig.ActionTimeIfGoSensorShield * _msUnit));
                    GoDone = true;
                    _isGoFlag = false;
                    _goToken.Cancel();
                    break;
                }
                else
                {
                    if (_goInput.Value) //未屏蔽则使用感应器控制
                    {
                        Thread.Sleep((int)(AppSolenoidConfig.GoDoneDelay * _msUnit));
                        GoDone = true;
                        _isGoFlag = false;
                        _goToken.Cancel();
                        break;
                    }
                    else
                    {
                        TimeSpan span = DateTime.Now - _beginTime;
                        if (span.TotalMilliseconds >= (int)(AppSolenoidConfig.GoAlarmTime * _msUnit)) //感应超时
                        {
                            _goAlarm.Value = true;
                            GoDone = false;
                            _isGoFlag = false;
                            _goToken.Cancel();
                            break;
                        }
                    }
                }
            }
        }
        private void ReadBackStatu()
        {
            DateTime _beginTime = DateTime.Now;
            while (!_backToken.IsCancellationRequested)
            {
                if (AppSolenoidConfig.BackSensorShield || AppSolenoidConfig.IsVacuum) //屏蔽感应或者是真空阀 使用时间控制
                {
                    Thread.Sleep((int)(AppSolenoidConfig.ActionTimeIfBackSensorShield * _msUnit));
                    BackDone = true;
                    _isBackFlag = false;
                    if (AppSolenoidConfig.IsVacuum) _backOutput.Value = false;
                    _backToken.Cancel();
                    break;
                }
                else
                {
                    if (_backInput.Value) //未屏蔽则使用感应器控制
                    {
                        Thread.Sleep((int)(AppSolenoidConfig.BackDoneDelay * _msUnit));
                        BackDone = true;
                        _isBackFlag = false;
                        _backToken.Cancel();
                        break;
                    }
                    else
                    {
                        TimeSpan span = DateTime.Now - _beginTime;
                        if (span.TotalMilliseconds >= (int)(AppSolenoidConfig.BackAlarmTime * _msUnit)) //感应超时
                        {
                            _backAlarm.Value = true;
                            BackDone = false;
                            _isBackFlag = false;
                            _backToken.Cancel();
                            break;
                        }
                    }
                }
            }
        }
        public async Task<bool> ActionGoAsync()
        {
            if (_isBackFlag || _isGoFlag)
            {
                TipMessage = $"{AppSolenoidConfig.SolenoidName}正在动作中,请勿重复操作!";
                return false;
            }
            GoDone = false;
            if (_goOutput.Enable)
                _goOutput.Value = true;
            if (AppSolenoidConfig.IsDoubleCoil)
                _backOutput.Value = false;
            _goToken = new CancellationTokenSource();
            await Task.Run(ReadGoStatu, _goToken.Token);
            return true;
        }
        public async Task<bool> ActionBackAsync()
        {
            if (_isBackFlag || _isGoFlag)
            {
                TipMessage = $"{AppSolenoidConfig.SolenoidName}正在动作中,请勿重复操作!";
                return false;
            }
            BackDone = false;
            if (_goOutput.Enable)
                _goOutput.Value = false;
            if (AppSolenoidConfig.IsDoubleCoil)
                _backOutput.Value = true;
            _backToken = new CancellationTokenSource();
            await Task.Run(ReadBackStatu, _backToken.Token);
            return true;
        }
    }
}
