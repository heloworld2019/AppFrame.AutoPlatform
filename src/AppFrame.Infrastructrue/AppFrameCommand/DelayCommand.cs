using AppFrame.Common.AppAttribute.Filter;
using AppFrame.Core.AppFrameCommand.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppFrame.Infrastructrue.AppFrameCommand
{
    /// <summary>
    /// 延时命令
    /// </summary>
    internal class DelayCommand : IAppCommand
    {
        protected readonly static object _lock = new object();
        protected bool _commandDone;
        protected bool _isCommandExecuting;
        protected CancellationTokenSource _token;
        public string CommandName { get; set; }

        public string CommandDescription { get; set; }
        [ConfigFilter]
        public bool IsCommandExecuteDone => _commandDone;
        [ConfigFilter]
        public bool IsCommandExecuting => _isCommandExecuting;

        public double CommandExecuteTimeOut { get ; set ; }

        public Guid CommandGuid { get;  set; }
        public bool IsEnable { get ; set ; }
        public int Index { get ; set ; }
        public string CommandToken { get ; set ; }
        public string LinkParameterName { get ; set ; }
        public object CommandParameterValue { get ; set ; }

        public DelayCommand(double delayTime, Guid commandGuid)
        {
            CommandGuid = commandGuid == Guid.Empty ? Guid.NewGuid() : commandGuid;
            IsEnable = true;
            CommandName = nameof(DelayCommand);
            CommandDescription = "延时一段时间";
            CommandToken = nameof(DelayCommand);
            LinkParameterName = nameof(DelayCommand);
            CommandParameterValue = delayTime;
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
            var startTime = DateTime.Now;
            while (!_token.IsCancellationRequested)
            {
                var span = DateTime.Now.Subtract(startTime);
                if (span.TotalSeconds >= double.Parse(CommandParameterValue.ToString()))
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
