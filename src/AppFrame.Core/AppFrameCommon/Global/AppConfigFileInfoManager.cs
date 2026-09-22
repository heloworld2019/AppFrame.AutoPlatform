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
        public readonly string AppConfigFloderPath  = Environment.CurrentDirectory + "\\AppConfig";                       
        public readonly string AppConfigInfoPath    = Environment.CurrentDirectory + "\\AppConfig" + "\\AppConfigInfos.xml";
        public readonly string AppProductFloderPath = Environment.CurrentDirectory + "\\AppProduct";
        public readonly string AppProductFileName   = "AppProducts.xml";
        public readonly string AppVendorFloderPath  = Environment.CurrentDirectory + "\\AppVendor";
        public readonly string AppVendorFileName    = "AppVendors.xml";
        public readonly string AppTaskFloderPath    = Environment.CurrentDirectory + "\\AppTask";
        //rootNode
        public readonly string ConfigRootNodeName           = "Configs";    //根节点   
        //categoryNode
        public readonly string AppElementCategoryName       = "AppElements";      //元素类别节点
        public readonly string AppCommunicationCategoryName = "AppCommunication"; //通信类别节点
        public readonly string AppAxisCategoryName          = "AppAxises";        //轴类别节点
        public readonly string AppSolenoidCategoryName      = "AppSolenoids";     //电磁阀类别节点
        public readonly string AppConfigInfoCategoryName    = "AppConfigInfos";  //配置信息类别节点
        public readonly string AppProductCategoryName       = "AppProducts";     //产品类别节点
        public readonly string AppVendorCategoryName        = "AppVendors";     //厂商类别节点
        public readonly string AppTaskCategoryName          = "AppTasks";       // 任务类别节点
        //groupNode
        public readonly string AppInputGroupName            = "Input";    //输入组节点
        public readonly string AppOutputGroupName           = "Output";  //输出组节点
        public readonly string AppAlarmGroupName            = "Alarm";   // 报警组节点
        public readonly string AppDataGroupName             = "Data";   // 数据组节点

        public readonly string ModbusRtuClientGroupName     = "ModbusRtuClient";   
        public readonly string ModbusTcpClientGroupName     = "ModbusTcpClient";
        public readonly string ModbusTcpServerGroupName     = "ModbusTcpServer";
        public readonly string NetClientGroupName           = "NetClient";
        public readonly string NetServerGroupName           = "NetServer";

        public readonly string AxisGroupName                = "Axis";
        public readonly string SolenoidGroupName            = "Solenoid";
        public readonly string AppConfigInfoGroupName       = "Info";

        public readonly string AppProductGroupName          = "ProductData";
        public readonly string AppVendorGroupName           = "Vendor";

        public readonly string AppTaskGroupName             = "Task";
    }
}



//主配置目录 程序运行目录\AppConfig
//AppConfigInfoPath	系统主配置 xml 完整路径 AppConfig\AppConfigInfos.xml
//AppProductFloderPath	产品配方文件夹
//AppProductFileName	产品配置文件名 AppProducts.xml
//AppVendorFloderPath	厂商设备配置文件夹
//AppVendorFileName	厂商配置文件
//AppTaskFloderPath	工艺任务 / 脚本任务文件夹