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
    /// 写入输出信号命令
    /// </summary>
    internal class WriteOutputCommand : GlobalObjectManager, IAppCommand
    {
        protected readonly static object _lock = new object();
        protected bool _commandDone;
        protected bool _isCommandExecuting;
        private AppOutputElement _appOutput;
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

        public WriteOutputCommand(string parameterName, bool value, double timeout, Guid commandGuid)
        {
            
            CommandExecuteTimeOut = timeout;
            CommandGuid = commandGuid == Guid.Empty ? Guid.NewGuid() : commandGuid;
            IsEnable = true;
            CommandName = nameof(WriteOutputCommand);
            CommandDescription = "更改一个输出信号的值";
            CommandToken = nameof(WriteOutputCommand);
            LinkParameterName = parameterName;
            CommandParameterValue = value;
        }
        public void ExecuteCommand()
        {
            lock (_lock)
            {
                _appOutput = AppConfigs.AppOutput.FirstOrDefault(p => p.ParameterName.Equals(LinkParameterName));
                if (_appOutput == null)
                    throw new ArgumentException($"{LinkParameterName}参数无效");
                _isCommandExecuting = true;
                _appOutput.Value = bool.Parse(CommandParameterValue.ToString());
                _commandDone = true;
                _isCommandExecuting = false;
            }
        }

        public async Task ExecuteCommandAsync()
        {
            _appOutput = AppConfigs.AppOutput.FirstOrDefault(p => p.ParameterName.Equals(LinkParameterName));
            if (_appOutput == null)
                throw new ArgumentException($"{LinkParameterName}参数无效");
            _isCommandExecuting = true;
            _appOutput.Value = bool.Parse(CommandParameterValue.ToString());
            _commandDone = true;
            _isCommandExecuting = false;
            await Task.Delay(1);
        }
    }
}
