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
    /// 绝对定位命令
    /// </summary>
    internal class AxisAbsCommand : AxisBaseCommand
    {

        public AxisAbsCommand(string parameterName, double position, Guid commandGuid) : base(parameterName, commandGuid)
        {
            CommandName = nameof(AxisAbsCommand);
            CommandDescription = "指定轴进行绝对定位操作";
            CommandToken = nameof(AxisAbsCommand);
            CommandParameterValue = position;
        }
        public override void ExecuteCommand()
        {
            lock (_lock)
            {
                _appAxis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(LinkParameterName));
                if (_appAxis == null)
                    throw new ArgumentException($"{LinkParameterName}参数无效");
                _isCommandExecuting = true;
                _appAxis.AbsoluteMove(double.Parse(CommandParameterValue.ToString()));
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
            _appAxis.AbsoluteMove(double.Parse(CommandParameterValue.ToString()));
            _token = new CancellationTokenSource();
            await Task.Factory.StartNew(WaitCommandDone, _token.Token);
        }
    }
}
