using AppFrame.Common.AppAttribute.Filter;
using AppFrame.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommand.Interfaces
{
    /// <summary>
    /// 命令接口
    /// </summary>
    public interface IAppCommand
    {
        
        /// <summary>
        /// 命令索引
        /// </summary>
        int Index { get; set; }
        /// <summary>
        /// 命令标识符
        /// </summary>
        Guid CommandGuid { get; set; }
        /// <summary>
        /// 命令名称
        /// </summary>
        string CommandName { get; set; }
        /// <summary>
        /// 命令描述
        /// </summary>        
        string CommandDescription { get; set; }
        /// <summary>
        /// 命令令牌
        /// </summary>
        string CommandToken { get; set; }
        /// <summary>
        /// 命令执行超时时间
        /// </summary>
        double CommandExecuteTimeOut { get; set; }
        /// <summary>
        /// 命令是否启用
        /// </summary>
        bool IsEnable { get; set; }
        /// <summary>
        /// 关联的命令参数值
        /// </summary>
        object CommandParameterValue { get; set; }
        /// <summary>
        /// 关联的参数名称
        /// </summary>
        string LinkParameterName { get; set; }
        /// <summary>
        /// 指示命令是否执行完成
        /// </summary>
        bool IsCommandExecuteDone { get; }
        /// <summary>
        /// 指示命令是否正在执行
        /// </summary>
        bool IsCommandExecuting { get; }     
        /// <summary>
        /// 执行命令
        /// </summary>
        void ExecuteCommand();
        /// <summary>
        /// 异步执行命令
        /// </summary>
        /// <returns></returns>
        Task ExecuteCommandAsync();
    }
}
