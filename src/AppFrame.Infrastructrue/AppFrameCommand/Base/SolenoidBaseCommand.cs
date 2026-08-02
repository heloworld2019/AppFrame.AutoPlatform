using AppFrame.Common.AppAttribute.Filter;
using AppFrame.Core.AppFrameCommand.Interfaces;
using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameIndustrialControl.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppFrame.Infrastructrue.AppFrameCommand.Base
{
    internal class SolenoidBaseCommand : GlobalObjectManager, IAppCommand
    {
        protected readonly static object _lock = new object();
        protected bool _commandDone;
        protected bool _isCommandExecuting;
        protected CancellationTokenSource _token;
        protected IAppSolenoid _appSolenoid;
        public virtual string CommandName { get; set; }

        public virtual string CommandDescription { get; set; }
        [ConfigFilter]
        public virtual bool IsCommandExecuteDone => _commandDone;
        [ConfigFilter]
        public virtual bool IsCommandExecuting => _isCommandExecuting;

        public virtual double CommandExecuteTimeOut { get ; set ; }

        public Guid CommandGuid { get;  set; }
        public bool IsEnable { get ; set ; }
        public int Index { get ; set ; }
        public string CommandToken { get ; set ; }
        public string LinkParameterName { get; set ; }
        public object CommandParameterValue { get ; set ; }

        public SolenoidBaseCommand(string parameterName, Guid commandGuid)
        {
            
            CommandExecuteTimeOut = 120;
            CommandGuid = commandGuid == Guid.Empty ? Guid.NewGuid() : commandGuid;
            IsEnable = true;
            LinkParameterName = parameterName;
        }
        public virtual void ExecuteCommand()
        {
            
        }
        protected virtual void WaitCommandDone()
        {
            
        }
        public virtual Task ExecuteCommandAsync()
        {
            return Task.CompletedTask;
        }
    }
}
