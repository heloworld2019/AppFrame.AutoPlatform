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
    /// 相对定位命令
    /// </summary>
    internal class AxisRelCommand : AxisBaseCommand
    {

        public AxisRelCommand(string parameterName, double distance, Guid commandGuid) : base(parameterName, commandGuid)
        {
            CommandName = nameof(AxisRelCommand);
            CommandDescription = "指定轴进行绝对定位操作";
            CommandToken = nameof(AxisRelCommand);
            CommandParameterValue = distance;
        }
        public override void ExecuteCommand()
        {
            lock (_lock)
            {
                _appAxis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(LinkParameterName));
                if (_appAxis == null) 
                    throw new ArgumentException($"{LinkParameterName}参数无效");
                _isCommandExecuting = true;
                _appAxis.RelativeMove(double.Parse(CommandParameterValue.ToString()));
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
                if (_appAxis.MoveDone)
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
            _appAxis.RelativeMove(double.Parse(CommandParameterValue.ToString()));
            _token = new CancellationTokenSource();
            await Task.Factory.StartNew(WaitCommandDone, _token.Token);
        }
    }
}
