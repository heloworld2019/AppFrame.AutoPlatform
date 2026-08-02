using AppFrame.Common.AppResult;
using AppFrame.Core.AppFrameCommand.Interfaces;
using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameIndustrialControl.Interfaces;
using AppFrame.Infrastructrue.AppFrameCommand.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace AppFrame.Infrastructrue.AppFrameCommand
{
    /// <summary>
    /// 回零命令
    /// </summary>
    internal class AxisHomeCommand : AxisBaseCommand
    {

        public AxisHomeCommand(string parameterName, Guid commandGuid) : base(parameterName, commandGuid)
        {
            CommandName = nameof(AxisHomeCommand);
            CommandDescription = "指定轴进行回零操作";
            CommandToken = nameof(AxisHomeCommand);
            CommandParameterValue = "null";
        }
        public override void ExecuteCommand()
        {
            lock (_lock)
            {
                _appAxis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(LinkParameterName));
                if (_appAxis == null)
                    throw new ArgumentException($"{LinkParameterName}参数无效");
                _isCommandExecuting = true;
                _appAxis.Home();
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
                if (_appAxis.HomeDone)
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
            _appAxis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(LinkParameterName));
            if (_appAxis == null)
                throw new ArgumentException($"{LinkParameterName}参数无效");
            _isCommandExecuting = true;
            _appAxis.Home();
            _token = new CancellationTokenSource();
            await Task.Factory.StartNew(WaitCommandDone, _token.Token);
        }
    }
}
