using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameConfig.Interfaces;
using AppFrame.Core.AppFrameElement.Models;
using AppFrame.Infrastructrue.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.ConsoleTest
{
    internal class ConfigurationTest: GlobalObjectManager
    {
        private readonly IAppConfiguration _appConfiguration;
        public ConfigurationTest(AppIoc appIoc)
        {
            _appConfiguration = appIoc.Resolve<IAppConfiguration>();
        }
        private void GenerateDefaultData()
        {
            for (int i = 0; i < 16; i++)
            {
                AppConfigs.AppInput.Add(new AppInputElement
                                        (i, i, $"输入参数{i}", AppFileInfo.AppInputGroupName, $"测试输入参数{i}", AppFileInfo.AppElementCategoryName, true,$"输入参数{i}"));
                AppConfigs.AppOutput.Add(new AppOutputElement
                                        (i, i, $"输出参数{i}", AppFileInfo.AppOutputGroupName, $"测试输出参数{i}", AppFileInfo.AppElementCategoryName, true, $"输出参数{i}"));
            }
            for (int i = 0; i < 100; i++)
            {
                AppConfigs.AppAlarm.Add(new AppAlarmElement
                                        (i, i, $"报警参数{i}", AppFileInfo.AppAlarmGroupName, $"测试报警参数{i}", AppFileInfo.AppElementCategoryName, true, $"报警参数{i}"));
                AppConfigs.AppData.Add(new AppDataElement
                                        (i, i, $"其他参数{i}", AppFileInfo.AppDataGroupName, $"测试其他参数{i}", AppFileInfo.AppElementCategoryName, true, $"其他参数{i}", "null"));
            }
            AppConfigs.AppModbusRtu.Add(new AppModbusRtuElement(0, 0, "ModbusRtu配置参数0", AppFileInfo.ModbusRtuClientGroupName, "测试ModbusRtu参数", AppFileInfo.AppCommunicationCategoryName,
                                                                true, "COM5", 9600, Parity.None, 8, StopBits.One));
            AppConfigs.AppModbusTcpClient.Add(new AppModbusTcpClientElement(0, 0, "ModbusTcpClient配置参数0", AppFileInfo.ModbusTcpClientGroupName, "测试ModbusTcpClient参数",
                                                AppFileInfo.AppCommunicationCategoryName, true, "192.168.1.10", 502));
            AppConfigs.AppModbusTcpServer.Add(new AppModbusTcpServerElement(0, 0, "ModbusTcpServer配置参数0", AppFileInfo.ModbusTcpServerGroupName, "测试ModbusTcpServer参数",
                                                AppFileInfo.AppCommunicationCategoryName, true, "192.168.1.11", 502));
            AppConfigs.AppNetClient.Add(new AppNetClientElement(0, 0, "NetClient配置参数0", AppFileInfo.NetClientGroupName, "测试NetClient参数",
                                                AppFileInfo.AppCommunicationCategoryName, true, "192.168.2.10", 8002));
            AppConfigs.AppNetServer.Add(new AppNetServerElement(0, 0, "NetServer配置参数0", AppFileInfo.NetServerGroupName, "测试NetServer参数",
                                                AppFileInfo.AppCommunicationCategoryName, true, "192.168.2.11", 8002));
            for (int i = 0; i < 10; i++)
            {
                AppConfigs.AppAxisConfigs.Add(new AppAxisElement(i, i, $"Axis{i}", AppFileInfo.AxisGroupName, $"测试Axis参数{i}", AppFileInfo.AppAxisCategoryName,
                                                true, 0, (ushort)i, $"Axis{i}", "mm"));
                AppConfigs.AppSolenoidConfigs.Add(new AppSolenoidElement(i, i, $"Solenoid{i}", AppFileInfo.SolenoidGroupName, $"测试Solenoid参数{i}", AppFileInfo.AppSolenoidCategoryName,
                                                true, $"Solenoid{i}", i, i + 10, i, i + 10, i, i + 10, false, true));
            }
            AppConfigs.AppConfigInfo.Add(new AppConfigInfoElement(0, 0, "J00-521", AppFileInfo.AppConfigInfoGroupName, "测试的配置信息", AppFileInfo.AppConfigInfoCategoryName,
                                         true, "J00-521", DateTime.Now.ToString("G"), DateTime.Now.ToString("G"), true));
            AppConfigs.AppConfigInfo.Add(new AppConfigInfoElement(1, 1, "J00-522", AppFileInfo.AppConfigInfoGroupName, "测试的配置信息", AppFileInfo.AppConfigInfoCategoryName,
                                         true, "J00-522", DateTime.Now.ToString("G"), DateTime.Now.ToString("G"), false));
        }
        public void Save(bool isfirst)
        {
            if (isfirst)
                GenerateDefaultData();
            _appConfiguration.SaveConfig();
        }
        public void Load()
        {
            _appConfiguration.LoadConfig();
        }
        public void CreateConfig(string name)
        {
            _appConfiguration.CreateConfig(name);
        }
        public void RemoveConfig(string name)
        {
            _appConfiguration.RemoveConfig(name);
        }
        public void RenameConfig(string oldName,string newName)
        {
            _appConfiguration.RenameConfig(oldName, newName);
        }
    }
}
