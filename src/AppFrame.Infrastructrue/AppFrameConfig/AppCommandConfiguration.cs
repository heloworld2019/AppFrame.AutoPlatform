using AppFrame.Common.AppAttribute.Filter;
using AppFrame.Common.AppFiles;
using AppFrame.Common.AppList;
using AppFrame.Core.AppFrameCommand.Interfaces;
using AppFrame.Core.AppFrameCommand.Manager;
using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameConfig.Interfaces;
using AppFrame.Core.AppFrameConfig.Models;
using AppFrame.Core.AppFrameException.AppConfigs;
using AppFrame.Infrastructrue.AppFrameCommand;
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
    public class AppCommandConfiguration : GlobalObjectManager, IAppCommandConfiguration
    {
        public void LoadCommandConfig(string configName = "")
        {
            CurrencyFileHelper.DirectoryExist(AppFileInfo.AppTaskFloderPath);
            if (string.IsNullOrWhiteSpace(configName))
            {
                var model = AppConfigs.AppConfigInfo.FirstOrDefault(p => p.IsCurrent);
                if (model == null)
                {
                    throw new ConfigInfoNotExistException(ConfigInfoNotExistString);
                }
                configName = model.ConfigName;
            }
            string fileName = configName + "-Task" + ".xml";
            string path = Path.Combine(AppFileInfo.AppTaskFloderPath, fileName);
            AllCommands.Clear();
            Load(path, AllCommands);
        }

        public void SaveCommandConfig()
        {
            CurrencyFileHelper.DirectoryExist(AppFileInfo.AppTaskFloderPath);
            var model = AppConfigs.AppConfigInfo.FirstOrDefault(p => p.IsCurrent);
            if (model == null)
            {
                throw new ConfigInfoNotExistException(ConfigInfoSaveErrorString);
            }
            string configName = model.ConfigName;
            string fileName = configName + "-Task" + ".xml";
            string path = Path.Combine(AppFileInfo.AppTaskFloderPath, fileName);
            if (AllCommands == null || AllCommands.Count <= 0) return;
            foreach (var command in AllCommands)
            {
                if (command.Value == null) continue;
                Save(path, command.Value, command.Key);
            }
            
        }
        private void Save(string path, IAppList<IAppCommand> configs, string groupName) 
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
                XmlFileHelper.CreateXml(path, elements, AppFileInfo.ConfigRootNodeName, AppFileInfo.AppTaskCategoryName, groupName, item.CommandName + "-" + item.Index.ToString());
            }
        }
        private void Load(string path, Dictionary<string, AppCommandManager> configs)
        {
            var tasks = XmlFileHelper.LoadXml(path, AppFileInfo.AppTaskCategoryName);
            if (tasks == null) return;
            foreach (var task in tasks)
            {
                var commands = task.Elements();
                if (commands == null) continue;
                var manager = new AppCommandManager();
                foreach (var command in commands)
                {
                    var commandValues = command.Elements();
                    if (commandValues == null) continue;
                    IAppCommand parameter = null;
                    var parameterName = commandValues.FirstOrDefault(p => p.Name.LocalName.Equals(nameof(IAppCommand.LinkParameterName)));
                    var parameterValue = commandValues.FirstOrDefault(p => p.Name.LocalName.Equals(nameof(IAppCommand.CommandParameterValue)));
                    var name = commandValues.FirstOrDefault(p => p.Name.LocalName.Equals(nameof(IAppCommand.CommandName)));
                    var desc = commandValues.FirstOrDefault(p => p.Name.LocalName.Equals(nameof(IAppCommand.CommandDescription)));
                    var time = commandValues.FirstOrDefault(p => p.Name.LocalName.Equals(nameof(IAppCommand.CommandExecuteTimeOut)));
                    var guid = commandValues.FirstOrDefault(p => p.Name.LocalName.Equals(nameof(IAppCommand.CommandGuid)));
                    var enable = commandValues.FirstOrDefault(p => p.Name.LocalName.Equals(nameof(IAppCommand.IsEnable)));
                    var index = commandValues.FirstOrDefault(p => p.Name.LocalName.Equals(nameof(IAppCommand.Index)));
                    var token = commandValues.FirstOrDefault(p => p.Name.LocalName.Equals(nameof(IAppCommand.CommandToken)));
                    if (parameterName == null || parameterValue == null || name == null || desc == null
                        || time == null || guid == null || enable == null || index == null || token == null)
                        continue;
                    switch (token.Value)
                    {
                        case nameof(AxisAbsCommand):
                            parameter = new AxisAbsCommand(parameterName.Value, double.Parse(parameterValue.Value), new Guid(guid.Value))
                            {
                                CommandDescription = desc.Value,
                                CommandExecuteTimeOut = double.Parse(time.Value),
                                CommandParameterValue = double.Parse(parameterValue.Value),
                                CommandName = name.Value,
                                CommandToken = token.Value,
                                Index = int.Parse(index.Value),
                                IsEnable = bool.Parse(enable.Value),
                                LinkParameterName = parameterName.Value
                            };
                            break;
                        case nameof(AxisRelCommand):
                            parameter = new AxisRelCommand(parameterName.Value, double.Parse(parameterValue.Value), new Guid(guid.Value))
                            {
                                CommandDescription = desc.Value,
                                CommandExecuteTimeOut = double.Parse(time.Value),
                                CommandParameterValue = double.Parse(parameterValue.Value),
                                CommandName = name.Value,
                                CommandToken = token.Value,
                                Index = int.Parse(index.Value),
                                IsEnable = bool.Parse(enable.Value),
                                LinkParameterName = parameterName.Value
                            };
                            break;
                        case nameof(AxisHomeCommand):
                            parameter = new AxisHomeCommand(parameterName.Value, new Guid(guid.Value))
                            {
                                CommandDescription = desc.Value,
                                CommandExecuteTimeOut = double.Parse(time.Value),
                                CommandParameterValue = parameterValue.Value,
                                CommandName = name.Value,
                                CommandToken = token.Value,
                                Index = int.Parse(index.Value),
                                IsEnable = bool.Parse(enable.Value),
                                LinkParameterName = parameterName.Value,
                            };
                            break;
                        case nameof(DelayCommand):
                            parameter = new DelayCommand(double.Parse(parameterValue.Value), new Guid(guid.Value))
                            {
                                CommandDescription = desc.Value,
                                CommandExecuteTimeOut = double.Parse(time.Value),
                                CommandParameterValue = double.Parse(parameterValue.Value),
                                CommandName = name.Value,
                                CommandToken = token.Value,
                                Index = int.Parse(index.Value),
                                IsEnable = bool.Parse(enable.Value),
                                LinkParameterName = parameterName.Value,
                            };
                            break;
                        case nameof(SolenoidGoCommand):
                            parameter = new SolenoidGoCommand(parameterName.Value, new Guid(guid.Value))
                            {
                                CommandDescription = desc.Value,
                                CommandExecuteTimeOut = double.Parse(time.Value),
                                CommandParameterValue = parameterValue.Value,
                                CommandName = name.Value,
                                CommandToken = token.Value,
                                Index = int.Parse(index.Value),
                                IsEnable = bool.Parse(enable.Value),
                                LinkParameterName = parameterName.Value,
                            };
                            break;
                        case nameof(SolenoidBackCommand):
                            parameter = new SolenoidBackCommand(parameterName.Value, new Guid(guid.Value))
                            {
                                CommandDescription = desc.Value,
                                CommandExecuteTimeOut = double.Parse(time.Value),
                                CommandParameterValue = parameterValue.Value,
                                CommandName = name.Value,
                                CommandToken = token.Value,
                                Index = int.Parse(index.Value),
                                IsEnable = bool.Parse(enable.Value),
                                LinkParameterName = parameterName.Value,
                            };
                            break;
                        case nameof(WaitInputCommand):
                            parameter = new WaitInputCommand(parameterName.Value, double.Parse(time.Value), bool.Parse(parameterValue.Value),new Guid(guid.Value))
                            {
                                CommandDescription = desc.Value,
                                CommandExecuteTimeOut = double.Parse(time.Value),
                                CommandParameterValue = bool.Parse(parameterValue.Value),
                                CommandName = name.Value,
                                CommandToken = token.Value,
                                Index = int.Parse(index.Value),
                                IsEnable = bool.Parse(enable.Value),
                                LinkParameterName = parameterName.Value,
                            };
                            break;
                        case nameof(WriteOutputCommand):
                            parameter = new WriteOutputCommand(parameterName.Value, bool.Parse(parameterValue.Value), double.Parse(time.Value), new Guid(guid.Value))
                            {
                                CommandDescription = desc.Value,
                                CommandExecuteTimeOut = double.Parse(time.Value),
                                CommandParameterValue = bool.Parse(parameterValue.Value),
                                CommandName = name.Value,
                                CommandToken = token.Value,
                                Index = int.Parse(index.Value),
                                IsEnable = bool.Parse(enable.Value),
                                LinkParameterName = parameterName.Value,
                            };
                            break;
                        default:
                            break;
                    }
                    if (parameter != null)
                        manager.Add(parameter);
                }
                configs.Add(task.Name.LocalName, manager);
            }              
        }
    }
}
