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
        /// 

       // void CancelCommand();//zyd add
         // 取消命令


        Task ExecuteCommandAsync(); // 异步线程



    }
}






//统一脚本 / 步骤命令抽象
//配方脚本、工艺步骤，每一步都是一个IAppCommand，命令管理器统一调度，不用关心具体是什么命令（轴移动、开关阀、读写寄存器）。
//统一状态管理
//所有命令统一拥有：执行中、执行完成状态，方便做命令队列、命令超时检测、UI 界面状态绑定。
//支持参数绑定
//LinkParameterName可以关联配方参数，实现脚本命令从配方读取参数值。
//兼容 WPF MVVM
//属性全部可以 get/set，可直接 XAML 绑定到界面，做命令配置页面。
//支持序列化保存
//可以把命令集合序列化保存到数据库 / Json，保存工艺配方。