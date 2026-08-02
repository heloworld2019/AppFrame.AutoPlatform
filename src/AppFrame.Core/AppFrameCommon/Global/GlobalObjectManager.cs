using AppFrame.Core.AppFrameCommand.Manager;
using AppFrame.Core.AppFrameElement.Manager;
using AppFrame.Core.AppFrameEventBus;
using AppFrame.Core.AppFrameIndustrialControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommon.Global
{
    /// <summary>
    /// 全局对象管理器
    /// </summary>
    public class GlobalObjectManager : AppStringsResource
    {
        /// <summary>
        /// 管理系统文件信息
        /// </summary>
        public static AppConfigFileInfoManager AppFileInfo { get; set; } = AppConfigFileInfoManager.Instance;
        /// <summary>
        /// 事件总线
        /// </summary>
        public static AppEventBus EventBus { get; private set; } = AppEventBus.Instance;
        /// <summary>
        /// 管理系统配置
        /// </summary>
        protected static AppConfigManager AppConfigs { get; private set; } = AppConfigManager.Instance;
        /// <summary>
        /// 阀操作
        /// </summary>
        protected static AppSolenoidProvider AppSolenoids { get; private set; } = AppSolenoidProvider.Instance;
        /// <summary>
        /// 轴操作
        /// </summary>
        protected static AppAxisProvider AppAxises { get; private set; } = AppAxisProvider.Instance;
        /// <summary>
        /// 系统所有的可执行流程
        /// </summary>
        protected static Dictionary<string, AppCommandManager> AllCommands { get; private set; } = new Dictionary<string, AppCommandManager>();
    }
}
