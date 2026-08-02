using AppFrame.Common.AppAttribute.Filter;
using AppFrame.Common.AppFiles;
using AppFrame.Common.AppList;
using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameConfig.Interfaces;
using AppFrame.Core.AppFrameConfig.Models;
using AppFrame.Core.AppFrameElement.Models;
using AppFrame.Core.AppFrameException.AppConfigs;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppFrame.Infrastructrue.AppFrameConfig
{
    public class AppConfiguration : GlobalObjectManager, IAppConfiguration
    {
        public void LoadConfig(string configName = "")
        {
            CurrencyFileHelper.DirectoryExist(AppFileInfo.AppConfigFloderPath);
            bool isFirstLoad = false;
            if (string.IsNullOrWhiteSpace(configName))
            {
                if (File.Exists(AppFileInfo.AppConfigInfoPath))
                    Load(AppFileInfo.AppConfigInfoPath, AppFileInfo.AppConfigInfoGroupName, AppConfigs.AppConfigInfo);
                if (AppConfigs.AppConfigInfo?.Count <= 0)
                {
                    GeneralDefaultData(); //生成默认的数据
                    isFirstLoad = true;
                }
                var model = AppConfigs.AppConfigInfo.FirstOrDefault(p => p.IsCurrent);
                if (model == null)
                {
                    throw new ConfigInfoNotExistException(ConfigInfoNotExistString);
                }
                configName = model.ConfigName;
            }
            if (isFirstLoad) return;
            string fileName = configName + ".xml";
            string path = Path.Combine(AppFileInfo.AppConfigFloderPath, fileName);
            Load(path, AppFileInfo.AppInputGroupName, AppConfigs.AppInput);
            Load(path, AppFileInfo.AppOutputGroupName, AppConfigs.AppOutput);
            Load(path, AppFileInfo.AppAlarmGroupName, AppConfigs.AppAlarm);
            Load(path, AppFileInfo.AppDataGroupName, AppConfigs.AppData);
            Load(path, AppFileInfo.ModbusRtuClientGroupName, AppConfigs.AppModbusRtu);
            Load(path, AppFileInfo.ModbusTcpClientGroupName, AppConfigs.AppModbusTcpClient);
            Load(path, AppFileInfo.ModbusTcpServerGroupName, AppConfigs.AppModbusTcpServer);
            Load(path, AppFileInfo.NetClientGroupName, AppConfigs.AppNetClient);
            Load(path, AppFileInfo.NetServerGroupName, AppConfigs.AppNetServer);
            Load(path, AppFileInfo.AxisGroupName, AppConfigs.AppAxisConfigs);
            Load(path, AppFileInfo.SolenoidGroupName, AppConfigs.AppSolenoidConfigs);
            //加载生产数据
            CurrencyFileHelper.DirectoryExist(AppFileInfo.AppProductFloderPath);
            path = Path.Combine(AppFileInfo.AppProductFloderPath, AppFileInfo.AppProductFileName);
            Load(path, AppFileInfo.AppProductGroupName, AppConfigs.AppProducts);
            //加载厂商信息
            CurrencyFileHelper.DirectoryExist(AppFileInfo.AppVendorFloderPath);
            path = Path.Combine(AppFileInfo.AppVendorFloderPath, AppFileInfo.AppVendorFileName);
            Load(path, AppFileInfo.AppVendorGroupName, AppConfigs.AppVendors);
        }
        public void SaveConfig()
        {
            //保存生产数据
            CurrencyFileHelper.DirectoryExist(AppFileInfo.AppProductFloderPath);
            string productPath = Path.Combine(AppFileInfo.AppProductFloderPath, AppFileInfo.AppProductFileName);
            Save(productPath, AppConfigs.AppProducts);
            //保存厂商信息
            CurrencyFileHelper.DirectoryExist(AppFileInfo.AppVendorFloderPath);
            string vendorPath = Path.Combine(AppFileInfo.AppVendorFloderPath, AppFileInfo.AppVendorFileName);
            Save(vendorPath, AppConfigs.AppVendors);
            //保存系统配置
            CurrencyFileHelper.DirectoryExist(AppFileInfo.AppConfigFloderPath);
            var model = AppConfigs.AppConfigInfo.FirstOrDefault(p => p.IsCurrent);
            if (model == null)
            {
                throw new ConfigInfoNotExistException(ConfigInfoSaveErrorString);
            }
            string configName = model.ConfigName;
            string fileName = configName + ".xml";
            string path = Path.Combine(AppFileInfo.AppConfigFloderPath, fileName);
            Save(AppFileInfo.AppConfigInfoPath, AppConfigs.AppConfigInfo);
            Save(path, AppConfigs.AppInput);
            Save(path, AppConfigs.AppOutput);
            Save(path, AppConfigs.AppAlarm);
            Save(path, AppConfigs.AppData);
            Save(path, AppConfigs.AppModbusRtu);
            Save(path, AppConfigs.AppModbusTcpClient);
            Save(path, AppConfigs.AppModbusTcpServer);
            Save(path, AppConfigs.AppNetClient);
            Save(path, AppConfigs.AppNetServer);
            Save(path, AppConfigs.AppAxisConfigs);
            Save(path, AppConfigs.AppSolenoidConfigs);
        }
        private void Save<T>(string path, IAppList<T> configs) where T : AppConfigParameter
        {
            foreach (var item in configs)
            {
                var properties = item.GetType().GetProperties();
                var elements = new List<XElement>();
                foreach (var property in properties)
                {
                    var objects = property.GetCustomAttributes(typeof(ConfigFilterAttribute), false);
                    if (objects.Length > 0) continue;
                    elements.Add(new XElement(property.Name, property.GetValue(item)));
                }
                XmlFileHelper.CreateXml(path, elements, AppFileInfo.ConfigRootNodeName, item.ParameterCategoryName, item.ParameterGroupName, item.ParameterName);
            }
        }
        private void Load<T>(string path, string groupNodeName, IAppList<T> configs) where T : AppConfigParameter, new()
        {
            var querys = XmlFileHelper.LoadXml(path, groupNodeName);
            if (querys == null) return;
            foreach (var item in querys)
            {
                var subElements = item.Elements();
                if (subElements == null) continue;
                var parameter = new T();
                var properties = parameter.GetType().GetProperties();
                foreach (var subitem in subElements)
                {
                    var property = properties.FirstOrDefault(x => x.Name.Equals(subitem.Name.LocalName));
                    if (property == null) continue;
                    var typeName = property.PropertyType.Name;
                    dynamic value = subitem.Value; //动态类型
                    switch (typeName)
                    {
                        case "Int64":
                            long.TryParse(subitem.Value, out long result);
                            value = result;
                            break;
                        case "UInt64":
                            ulong.TryParse(subitem.Value, out ulong result0);
                            value = result0;
                            break;
                        case "Int32":
                            int.TryParse(subitem.Value, out int result1);
                            value = result1;
                            break;
                        case "UInt32":
                            uint.TryParse(subitem.Value, out uint result2);
                            value = result2;
                            break;
                        case "Single":
                            float.TryParse(subitem.Value, out float result3);
                            value = result3;
                            break;
                        case "Double":
                            double.TryParse(subitem.Value, out double result4);
                            value = result4;
                            break;
                        case "Int16":
                            short.TryParse(subitem.Value, out short result5);
                            value = result5;
                            break;
                        case "UInt16":
                            ushort.TryParse(subitem.Value, out ushort result6);
                            value = result6;
                            break;
                        case "Boolean":
                            bool.TryParse(subitem.Value, out bool result7);
                            value = result7;
                            break;
                        case "Parity":
                            value = (Parity)Enum.Parse(typeof(Parity), subitem.Value);
                            break;
                        case "StopBits":
                            value = (StopBits)Enum.Parse(typeof(StopBits), subitem.Value);
                            break;
                        default:
                            break;
                    }
                    property.SetValue(parameter, value);
                }
                configs.Add(parameter);
            }
        }
        /// <summary>
        /// 生成默认的数据
        /// </summary>
        private void GeneralDefaultData()
        {
            for (int i = 0; i < 32; i++)
            {
                AppConfigs.AppInput.Add(new AppInputElement
                                        (i, i, $"In_{i}", AppFileInfo.AppInputGroupName, $"默认输入参数{i}", AppFileInfo.AppElementCategoryName, true,$"输入参数{i}"));
                AppConfigs.AppOutput.Add(new AppOutputElement
                                        (i, i, $"Out_{i}", AppFileInfo.AppOutputGroupName, $"默认输出参数{i}", AppFileInfo.AppElementCategoryName, true,$"输出参数{i}"));
            }
            for (int i = 0; i < 200; i++)
            {
                AppConfigs.AppAlarm.Add(new AppAlarmElement
                                        (i, i, $"Alarm_{i}", AppFileInfo.AppAlarmGroupName, $"默认报警参数{i}", AppFileInfo.AppElementCategoryName, true,$"报警参数{i}"));
                AppConfigs.AppData.Add(new AppDataElement
                                        (i, i, $"Data_{i}", AppFileInfo.AppDataGroupName, $"默认其他参数{i}", AppFileInfo.AppElementCategoryName, true, $"其他参数{i}","null"));
            }
            AppConfigs.AppModbusRtu.Add(new AppModbusRtuElement(0, 0, "ModbusRtu配置", AppFileInfo.ModbusRtuClientGroupName, "默认ModbusRtu参数", AppFileInfo.AppCommunicationCategoryName,
                                                                true, "COM5", 9600, Parity.None, 8, StopBits.One));
            AppConfigs.AppModbusTcpClient.Add(new AppModbusTcpClientElement(0, 0, "ModbusTcpClient配置", AppFileInfo.ModbusTcpClientGroupName, "默认ModbusTcpClient参数",
                                                AppFileInfo.AppCommunicationCategoryName, true, "192.168.1.10", 502));
            AppConfigs.AppModbusTcpServer.Add(new AppModbusTcpServerElement(0, 0, "ModbusTcpServer配置", AppFileInfo.ModbusTcpServerGroupName, "默认ModbusTcpServer参数",
                                                AppFileInfo.AppCommunicationCategoryName, true, "192.168.1.11", 502));
            AppConfigs.AppNetClient.Add(new AppNetClientElement(0, 0, "NetClient配置", AppFileInfo.NetClientGroupName, "默认NetClient参数",
                                                AppFileInfo.AppCommunicationCategoryName, true, "192.168.2.10", 8002));
            AppConfigs.AppNetServer.Add(new AppNetServerElement(0, 0, "NetServer配置", AppFileInfo.NetServerGroupName, "默认NetServer参数",
                                                AppFileInfo.AppCommunicationCategoryName, true, "192.168.2.11", 8002));
            for (int i = 0; i < 10; i++)
            {
                AppConfigs.AppAxisConfigs.Add(new AppAxisElement(i, i, $"Axis{i}", AppFileInfo.AxisGroupName, $"默认Axis参数{i}", AppFileInfo.AppAxisCategoryName,
                                                true, 0, (ushort)i, $"Axis{i}", "mm"));
                AppConfigs.AppSolenoidConfigs.Add(new AppSolenoidElement(i, i, $"Solenoid{i}", AppFileInfo.SolenoidGroupName, $"默认Solenoid参数{i}", AppFileInfo.AppSolenoidCategoryName,
                                                true, $"Solenoid{i}", i, i + 10, i, i + 10, i, i + 10, false, true));
            }
            AppConfigs.AppConfigInfo.Add(new AppConfigInfoElement(0, 0, "J00-521", AppFileInfo.AppConfigInfoGroupName, "默认配置信息", AppFileInfo.AppConfigInfoCategoryName,
                                         true, "J00-521", DateTime.Now.ToString("G"), DateTime.Now.ToString("G"), true));

            var date = DateTime.UtcNow.ToString(ProductDateFormat);
            AppConfigs.AppProducts.Add(new AppProductElement(0, 0, $"_{date}", AppFileInfo.AppProductGroupName, $"{date}的生产数据", AppFileInfo.AppProductCategoryName,
                                      true, date,"J00-521", "2022120100012J00521", 100000, 63841, 10000, 1752, 350, 121, 120, 1));

            AppConfigs.AppVendors.Add(new AppVendorElement(0, 0,"厂商信息", AppFileInfo.AppVendorGroupName, "系统所属厂商的信息", AppFileInfo.AppVendorCategoryName,
                                     true, "厂商名称：先导科技集团","办公地址：广州市珠江新城环球都会广场47楼", "生产基地：先导微电子（衢州）有限公司",
                                     "厂商电话：13122286685", "厂商网址：www.vital.com", "销售专线：400-888-888", "技术专线：13122286685", true,"AppFrame-AutoPlatform"));
        }

        public void CreateConfig(string configName)
        {
            if (string.IsNullOrWhiteSpace(configName))
            {
                throw new ArgumentException($"“{nameof(configName)}”不能为 null 或空白。", nameof(configName));
            }
            var result = AppConfigs.AppConfigInfo.FirstOrDefault(p => p.ConfigName.Equals(configName));
            if (result != null)
            {
                throw new ConfigTipException("该配置已存在!");
            }
            int id = AppConfigs.AppConfigInfo.Count;
            string date = DateTime.UtcNow.ToString("G");
            var newConfig = new AppConfigInfoElement(id, id, configName, AppFileInfo.AppConfigInfoGroupName, $"配置方案{configName}", AppFileInfo.AppConfigInfoCategoryName,
                                                    true, configName, date, date, true);
            var model = AppConfigs.AppConfigInfo.Single(p => p.IsCurrent);
            if (model != null)
                model.IsCurrent = false;
            AppConfigs.AppConfigInfo.Add(newConfig);
            SaveConfig();
        }

        public void RemoveConfig(string configName)
        {
            if (string.IsNullOrWhiteSpace(configName))
            {
                throw new ArgumentException($"“{nameof(configName)}”不能为 null 或空白。", nameof(configName));
            }
            var model = AppConfigs.AppConfigInfo.Single(p => p.ConfigName.Equals(configName));
            if(model!=null && model.IsCurrent)
            {
                throw new ConfigTipException("当前系统正在使用此配置,无法移除!");
            }
            if(model != null)
            {
                AppConfigs.AppConfigInfo.Remove(model);
                XmlFileHelper.RemoveNode(AppFileInfo.AppConfigInfoPath, model.ParameterGroupName, model.ParameterName);
                //移除本地配置文件
                string path = Path.Combine(AppFileInfo.AppConfigFloderPath, $"{configName}.xml");
                var file = Directory.GetFiles(AppFileInfo.AppConfigFloderPath).FirstOrDefault(p => p.ToLower().Equals(path.ToLower()));
                if (string.IsNullOrWhiteSpace(file)) return;
                File.Delete(file);
            }
        }

        public void RenameConfig(string oldName, string newName)
        {
            if (string.IsNullOrWhiteSpace(oldName))
            {
                throw new ArgumentException($"“{nameof(oldName)}”不能为 null 或空白。", nameof(oldName));
            }
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException($"“{nameof(newName)}”不能为 null 或空白。", nameof(newName));
            }
            var model = AppConfigs.AppConfigInfo.FirstOrDefault(p => p.ConfigName.Equals(newName));
            if (model != null)
            {
                throw new ConfigTipException("该配置已存在,请重新命名!");
            }
            model = AppConfigs.AppConfigInfo.FirstOrDefault(p => p.ConfigName.Equals(oldName));
            if (model == null)
            {
                throw new ConfigTipException("旧的配置不存在,请确认!");
            }
            model.ConfigName = model.ParameterName = newName;
            model.UpdateTime = DateTime.UtcNow.ToString("G");
            XmlFileHelper.RemoveNode(AppFileInfo.AppConfigInfoPath, model.ParameterGroupName, oldName);
            string oldPath = Path.Combine(AppFileInfo.AppConfigFloderPath, $"{oldName}.xml");
            string newPath = Path.Combine(AppFileInfo.AppConfigFloderPath, $"{newName}.xml");
            if (!string.IsNullOrWhiteSpace(oldPath) && !string.IsNullOrWhiteSpace(newName))
            {
                var fi = new FileInfo(oldPath);
                fi.MoveTo(newPath);
            }
            SaveConfig();
        }
    }
}
