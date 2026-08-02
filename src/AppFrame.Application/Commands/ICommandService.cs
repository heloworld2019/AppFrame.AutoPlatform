using AppFrame.Common.AppResult;
using AppFrame.Core.AppFrameCommand.Interfaces;
using AppFrame.Core.AppFrameCommand.Manager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Application.Commands
{
    public interface ICommandService
    {
        /// <summary>
        /// 获取所有任务
        /// </summary>
        /// <returns></returns>
        DataResult<Dictionary<string, ObservableCollection<IAppCommand>>> GetAllTasks();
        /// <summary>
        /// 获取指定任务
        /// </summary>
        /// <param name="progressName"></param>
        /// <returns></returns>
        DataResult<ObservableCollection<IAppCommand>> GetTasks(string progressName);
        /// <summary>
        /// 注册一个执行流程
        /// </summary>
        void RegisterExecutionProgress(string progressName);
        /// <summary>
        /// 取消注册执行流程
        /// </summary>
        void UnregisterExecutionProgress(string progressName);
        /// <summary>
        /// 重命名执行流程
        /// </summary>
        void RenameExecutionProgress(string oldName, string newName);
        /// <summary>
        /// 执行一个流程
        /// </summary>
        /// <param name="progressName"></param>
        Task<DataResult<string>> ExecuteProgressAsync(string progressName);
        /// <summary>
        /// 移除一个命令
        /// </summary>
        /// <param name="progressName"></param>
        /// <param name="commandGuid"></param>
        void RemoveCommand(string progressName, Guid commandGuid);
        /// <summary>
        /// 移除一个命令
        /// </summary>
        /// <param name="progressName"></param>
        /// <param name="index"></param>
        void RemoveCommand(string progressName, int index);
        /// <summary>
        /// 更新命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        DataResult<IAppCommand> UpdateCommand(string progressName, IAppCommand command);
        /// <summary>
        /// 注册回零命令
        /// </summary>
        /// <param name="progressName"></param>
        /// <param name="parameterName"></param>
        /// <param name="commandGuid"></param>
        DataResult<IAppCommand> RegisterAxisHomeCommand(string progressName, string parameterName, Guid commandGuid, int index = -1);
        /// <summary>
        /// 注册绝对定位命令
        /// </summary>
        /// <param name="progressName"></param>
        /// <param name="parameterName"></param>
        /// <param name="position"></param>
        /// <param name="commandGuid"></param>
        DataResult<IAppCommand> RegisterAxisAbsCommand(string progressName, string parameterName, double position, Guid commandGuid, int index = -1);
        /// <summary>
        /// 注册相对定位命令
        /// </summary>
        /// <param name="progressName"></param>
        /// <param name="parameterName"></param>
        /// <param name="distance"></param>
        /// <param name="commandGuid"></param>
        DataResult<IAppCommand> RegisterAxisRelCommand(string progressName, string parameterName, double distance, Guid commandGuid, int index = -1);
        /// <summary>
        /// 注册阀到动点命令
        /// </summary>
        /// <param name="progressName"></param>
        /// <param name="parameterName"></param>
        /// <param name="commandGuid"></param>
        DataResult<IAppCommand> RegisterSolenoidGoCommand(string progressName, string parameterName, Guid commandGuid, int index = -1);
        /// <summary>
        /// 注册阀到原点命令
        /// </summary>
        /// <param name="progressName"></param>
        /// <param name="parameterName"></param>
        /// <param name="commandGuid"></param>
        DataResult<IAppCommand> RegisterSolenoidBackCommand(string progressName, string parameterName, Guid commandGuid, int index = -1);
        /// <summary>
        /// 注册延时命令
        /// </summary>
        /// <param name="progressName"></param>
        /// <param name="delayTime"></param>
        /// <param name="commandGuid"></param>
        DataResult<IAppCommand> RegisterDelayCommand(string progressName, double delayTime, Guid commandGuid, int index = -1);
        /// <summary>
        /// 注册信号等待命令
        /// </summary>
        /// <param name="progressName"></param>
        /// <param name="parameterName"></param>
        /// <param name="timeout"></param>
        /// <param name="commandGuid"></param>
        DataResult<IAppCommand> RegisterWaitInputCommand(string progressName, string parameterName, double timeout, bool condition, Guid commandGuid, int index = -1);
        /// <summary>
        /// 注册更新输出命令
        /// </summary>
        /// <param name="progressName"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <param name="timeout"></param>
        /// <param name="commandGuid"></param>
        DataResult<IAppCommand> RegisterWriteOutputCommand(string progressName, string parameterName, bool value, double timeout, Guid commandGuid, int index = -1);
    }
}
