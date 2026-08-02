using AppFrame.Infrastructrue.AppFrameCommand.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppFrame.Infrastructrue.AppFrameCommand
{
    /// <summary>
    /// 电磁阀到动点命令
    /// </summary>
    internal class SolenoidGoCommand : SolenoidBaseCommand
    {

        public SolenoidGoCommand(string parameterName, Guid commandGuid) : base(parameterName, commandGuid)
        {
            CommandName = nameof(SolenoidGoCommand);
            CommandDescription = "指定电磁阀到动点";
            CommandToken = nameof(SolenoidGoCommand);
            CommandParameterValue = "null";
        }
        public override void ExecuteCommand()
        {
            lock (_lock)
            {
                _appSolenoid = AppSolenoids[LinkParameterName];
                if (_appSolenoid == null)
                    throw new ArgumentException($"{LinkParameterName}参数无效");
                _isCommandExecuting = true;
                _appSolenoid.ActionGo();
                _token = new CancellationTokenSource();
                Task.Factory.StartNew(WaitCommandDone, _token.Token);
            }
        }
        protected override void WaitCommandDone()
        {
            var startTime = DateTime.Now;
            while (!_token.IsCancellationRequested)
            {
                var span = DateTime.Now.Subtract(startTime);
                if (span.TotalSeconds >= CommandExecuteTimeOut)
                {
                    _commandDone = false;
                    _isCommandExecuting = false;
                    break;
                }
                if (_appSolenoid.GoDone)
                {
                    _commandDone = true;
                    _isCommandExecuting = false;
                    break;
                }
                Thread.Sleep(4);
            }
            _token.Cancel();
        }
        public override async Task ExecuteCommandAsync()
        {
            _appSolenoid = AppSolenoids[LinkParameterName];
            if (_appSolenoid == null)
                throw new ArgumentException($"{LinkParameterName}参数无效");
            _isCommandExecuting = true;
            _appSolenoid.ActionGo();
            _token = new CancellationTokenSource();
            await Task.Factory.StartNew(WaitCommandDone, _token.Token);
        }
    }
}
