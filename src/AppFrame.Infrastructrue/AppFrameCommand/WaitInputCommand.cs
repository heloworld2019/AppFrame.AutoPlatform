using AppFrame.Common.AppAttribute.Filter;
using AppFrame.Core.AppFrameCommand.Interfaces;
using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppFrame.Infrastructrue.AppFrameCommand
{
    /// <summary>
    /// 等待输入信号命令
    /// </summary>
    internal class WaitInputCommand : GlobalObjectManager, IAppCommand
    {
        protected readonly static object _lock = new object();
        protected bool _commandDone;
        protected bool _isCommandExecuting;
        protected CancellationTokenSource _token;
        private AppInputElement _appInput;
        public string CommandName { get; set; }

        public string CommandDescription { get; set; }
        [ConfigFilter]
        public bool IsCommandExecuteDone => _commandDone;
        [ConfigFilter]
        public bool IsCommandExecuting => _isCommandExecuting;

        public double CommandExecuteTimeOut { get ; set ; }

        public Guid CommandGuid { get; set; }
        public bool IsEnable { get ; set ; }
        public int Index { get ; set ; }
        public string CommandToken { get ; set ; }
        public string LinkParameterName { get ; set ; }
        public object CommandParameterValue { get ; set ; }

        public WaitInputCommand(string parameterName, double timeout, bool condition, Guid commandGuid)
        {
            CommandExecuteTimeOut = timeout;
            CommandGuid = commandGuid == Guid.Empty ? Guid.NewGuid() : commandGuid;
            IsEnable = true;
            CommandName = nameof(WaitInputCommand);
            CommandDescription = "等待一个信号满足条件";
            CommandToken = nameof(WaitInputCommand);
            LinkParameterName = parameterName;
            CommandParameterValue = condition;
        }

        public void ExecuteCommand()
        {
            lock (_lock)
            {
                _isCommandExecuting = true;
                _token = new CancellationTokenSource();
                Task.Factory.StartNew(WaitCommandDone, _token.Token);
            }
        }

        private void WaitCommandDone()
        {
            _appInput = AppConfigs.AppInput.FirstOrDefault(p => p.ParameterName.Equals(LinkParameterName));
            if (_appInput == null)
                throw new ArgumentException($"{LinkParameterName}参数无效");
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
                if (_appInput.Value == bool.Parse(CommandParameterValue.ToString()))
                {
                    _commandDone = true;
                    _isCommandExecuting = false;
                    break;
                }
            }
            _token.Cancel();
        }

        public async Task ExecuteCommandAsync()
        {
            _isCommandExecuting = true;
            _token = new CancellationTokenSource();
            await Task.Factory.StartNew(WaitCommandDone, _token.Token);
        }
    }
}
