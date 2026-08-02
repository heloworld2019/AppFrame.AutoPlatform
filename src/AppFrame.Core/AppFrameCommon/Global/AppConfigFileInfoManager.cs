using AppFrame.Core.AppFrameElement.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommon.Global
{
    public class AppConfigFileInfoManager
    {
        private static readonly object _lock = new object();
        private static AppConfigFileInfoManager _instance;
        private AppConfigFileInfoManager()
        {

        }
        public static AppConfigFileInfoManager Instance => Create();
        private static AppConfigFileInfoManager Create()
        {
            lock (_lock)
            {
                return _instance ?? (_instance = new AppConfigFileInfoManager());
            }
        }
        //配置文件夹路径
        public readonly string AppConfigFloderPath = Environment.CurrentDirectory + "\\AppConfig";
        public readonly string AppConfigInfoPath = Environment.CurrentDirectory + "\\AppConfig" + "\\AppConfigInfos.xml";
        public readonly string AppProductFloderPath = Environment.CurrentDirectory + "\\AppProduct";
        public readonly string AppProductFileName = "AppProducts.xml";
        public readonly string AppVendorFloderPath = Environment.CurrentDirectory + "\\AppVendor";
        public readonly string AppVendorFileName = "AppVendors.xml";
        public readonly string AppTaskFloderPath = Environment.CurrentDirectory + "\\AppTask";
        //rootNode
        public readonly string ConfigRootNodeName = "Configs";
        //categoryNode
        public readonly string AppElementCategoryName = "AppElements";
        public readonly string AppCommunicationCategoryName = "AppCommunication";
        public readonly string AppAxisCategoryName = "AppAxises";
        public readonly string AppSolenoidCategoryName = "AppSolenoids";
        public readonly string AppConfigInfoCategoryName = "AppConfigInfos";
        public readonly string AppProductCategoryName = "AppProducts";
        public readonly string AppVendorCategoryName = "AppVendors";
        public readonly string AppTaskCategoryName = "AppTasks";
        //groupNode
        public readonly string AppInputGroupName = "Input";
        public readonly string AppOutputGroupName = "Output";
        public readonly string AppAlarmGroupName = "Alarm";
        public readonly string AppDataGroupName = "Data";
        
        public readonly string ModbusRtuClientGroupName = "ModbuRtuClient";
        public readonly string ModbusTcpClientGroupName = "ModbuTcpClient";
        public readonly string ModbusTcpServerGroupName = "ModbuTcpServer";
        public readonly string NetClientGroupName = "NetClient";
        public readonly string NetServerGroupName = "NetServer";

        public readonly string AxisGroupName = "Axis";
        public readonly string SolenoidGroupName = "Solenoid";
        public readonly string AppConfigInfoGroupName = "Info";

        public readonly string AppProductGroupName = "ProductData";
        public readonly string AppVendorGroupName = "Vendor";

        public readonly string AppTaskGroupName = "Task";
    }
}
