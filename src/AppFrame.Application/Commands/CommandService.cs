using AppFrame.Common.AppResult;
using AppFrame.Core.AppFrameCommand.Interfaces;
using AppFrame.Core.AppFrameCommand.Manager;
using AppFrame.Core.AppFrameCommon.Global;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppFrame.Application.Commands
{
    public class CommandService : GlobalObjectManager, ICommandService
    {
        private readonly IAppCommandFactory _factory;
        private CancellationTokenSource _token;
        public CommandService(IAppCommandFactory factory)
        {
            _factory = factory;
        }
        /// <summary>
        /// 更改索引
        /// </summary>
        /// <param name="appCommands"></param>
        /// <param name="index"></param>
        private void ChangeIndex(AppCommandManager appCommands, IAppCommand command, int index, bool isInc)
        {
            if (appCommands == null) return;
            foreach (var item in appCommands)
            {
                if (item.Index >= index && item.CommandGuid != command.CommandGuid)
                    _ = isInc ? item.Index++ : item.Index--;
            }
        }
        public DataResult<IAppCommand> RegisterAxisAbsCommand(string progressName, string parameterName, double position, Guid commandGuid, int index = -1)
        {
            var model = AllCommands[progressName];
            if (model != null)
            {
                var command = _factory.CreateAxisAbsCommand(parameterName, position, commandGuid);
                if (index == -1)
                {
                    command.Index = model.Count;
                    model.Add(command);
                }
                else
                {
                    command.Index = index;
                    model.Insert(index, command);
                    ChangeIndex(model, command, index, true);
                }
                return new DataResult<IAppCommand>(command);
            }
            return new DataResult<IAppCommand>($"不存在的执行流程{progressName}");
        }

        public DataResult<IAppCommand> RegisterAxisHomeCommand(string progressName, string parameterName, Guid commandGuid, int index = -1)
        {
            var model = AllCommands[progressName];
            if (model != null)
            {
                var command = _factory.CreateAxisHomeCommand(parameterName, commandGuid);
                if (index == -1)
                {
                    command.Index = model.Count;
                    model.Add(command);
                }
                else
                {
                    command.Index = index;
                    model.Insert(index, command);
                    ChangeIndex(model, command, index, true);
                }
                return new DataResult<IAppCommand>(command);
            }
            return new DataResult<IAppCommand>($"不存在的执行流程{progressName}");
        }

        public DataResult<IAppCommand> RegisterAxisRelCommand(string progressName, string parameterName, double distance, Guid commandGuid, int index = -1)
        {
            var model = AllCommands[progressName];
            if (model != null)
            {
                var command = _factory.CreateAxisRelCommand(parameterName, distance, commandGuid);
                if (index == -1)
                {
                    command.Index = model.Count;
                    model.Add(command);
                }
                else
                {
                    command.Index = index;
                    model.Insert(index, command);
                    ChangeIndex(model, command, index, true);
                }
                return new DataResult<IAppCommand>(command);
            }
            return new DataResult<IAppCommand>($"不存在的执行流程{progressName}");
        }

        public DataResult<IAppCommand> RegisterDelayCommand(string progressName, double delayTime, Guid commandGuid, int index = -1)
        {
            var model = AllCommands[progressName];
            if (model != null)
            {
                var command = _factory.CreateDelayCommand(delayTime, commandGuid);
                if (index == -1)
                {
                    command.Index = model.Count;
                    model.Add(command);
                }
                else
                {
                    command.Index = index;
                    model.Insert(index, command);
                    ChangeIndex(model, command, index, true);
                }
                return new DataResult<IAppCommand>(command);
            }
            return new DataResult<IAppCommand>($"不存在的执行流程{progressName}");
        }

        public DataResult<IAppCommand> RegisterSolenoidBackCommand(string progressName, string parameterName, Guid commandGuid, int index = -1)
        {
            var model = AllCommands[progressName];
            if (model != null)
            {
                var command = _factory.CreateSolenoidBackCommand(parameterName, commandGuid);
                if (index == -1)
                {
                    command.Index = model.Count;
                    model.Add(command);
                }
                else
                {
                    command.Index = index;
                    model.Insert(index, command);
                    ChangeIndex(model, command, index, true);
                }
                return new DataResult<IAppCommand>(command);
            }
            return new DataResult<IAppCommand>($"不存在的执行流程{progressName}");
        }

        public DataResult<IAppCommand> RegisterSolenoidGoCommand(string progressName, string parameterName, Guid commandGuid, int index = -1)
        {
            var model = AllCommands[progressName];
            if (model != null)
            {
                var command = _factory.CreateSolenoidGoCommand(parameterName, commandGuid);
                if (index == -1)
                {
                    command.Index = model.Count;
                    model.Add(command);
                }
                else
                {
                    command.Index = index;
                    model.Insert(index, command);
                    ChangeIndex(model, command, index, true);
                }
                return new DataResult<IAppCommand>(command);
            }
            return new DataResult<IAppCommand>($"不存在的执行流程{progressName}");
        }

        public DataResult<IAppCommand> RegisterWaitInputCommand(string progressName, string parameterName, double timeout, bool condition, Guid commandGuid, int index = -1)
        {
            var model = AllCommands[progressName];
            if (model != null)
            {
                var command = _factory.CreateWaitInputCommand(parameterName, timeout, condition, commandGuid);
                if (index == -1)
                {
                    command.Index = model.Count;
                    model.Add(command);
                }
                else
                {
                    command.Index = index;
                    model.Insert(index, command);
                    ChangeIndex(model, command, index, true);
                }
                return new DataResult<IAppCommand>(command);
            }
            return new DataResult<IAppCommand>($"不存在的执行流程{progressName}");
        }

        public DataResult<IAppCommand> RegisterWriteOutputCommand(string progressName, string parameterName, bool value, double timeout, Guid commandGuid, int index = -1)
        {
            var model = AllCommands[progressName];
            if (model != null)
            {
                var command = _factory.CreateWriteOutputCommand(parameterName, value, timeout, commandGuid);
                if (index == -1)
                {
                    command.Index = model.Count;
                    model.Add(command);
                }
                else
                {
                    command.Index = index;
                    model.Insert(index, command);
                    ChangeIndex(model, command, index, true);
                }
                return new DataResult<IAppCommand>(command);
            }
            return new DataResult<IAppCommand>($"不存在的执行流程{progressName}");
        }

        public void RenameExecutionProgress(string oldName, string newName)
        {
            if (!AllCommands.ContainsKey(oldName))
            {
                throw new ArgumentException($"不存在的执行流程:{oldName}，请重试！");
            }
            if (AllCommands.ContainsKey(newName))
            {
                throw new ArgumentException($"已存在的执行流程:{newName}，请重试！");
            }
            var model = AllCommands[oldName];
            if (model != null)
            {
                AllCommands.Remove(oldName);
                AllCommands.Add(newName, model);
            }           
        }
        public void UnregisterExecutionProgress(string progressName)
        {
            if (AllCommands.ContainsKey(progressName))
            {
                AllCommands.Remove(progressName);
            }     
        }
        public void RegisterExecutionProgress(string progressName)
        {
            if (AllCommands.ContainsKey(progressName))
            {
                throw new ArgumentException($"已存在的执行流程:{progressName}，请重试！");
            }
            AllCommands.Add(progressName, new AppCommandManager());
        }
        public async Task<DataResult<string>> ExecuteProgressAsync(string progressName)
        {
            var models = AllCommands[progressName];
            if (models == null) return new DataResult<string>($"不存在的执行流程{progressName}，请重试");
            bool result = true;
            string message = "流程执行完成";
            _token = new CancellationTokenSource();
            await Task.Factory.StartNew(async () =>
            {
                while (!_token.IsCancellationRequested)
                {
                    foreach (var command in models)
                    {
                        if (command == null || !command.IsEnable) continue;
                        await command.ExecuteCommandAsync();
                        if(command.IsCommandExecuteDone && !command.IsCommandExecuting) //命令执行成功
                        {
                            continue;
                        }
                        //命令执行失败
                        result = false;
                        message = $"{command.CommandName}流程执行失败，失败命令描述:{command.CommandDescription}";
                        break;
                    }
                    _token.Cancel();
                }
            }, _token.Token);
            return new DataResult<string>(message, result);
        }
        public void RemoveCommand(string progressName, Guid commandGuid)
        {
            if (!AllCommands.ContainsKey(progressName))
            {
                throw new ArgumentException($"不存在的执行流程{progressName}");
            }
            var models = AllCommands[progressName];
            if (models != null)
            {
                var command = models.FirstOrDefault(p => p.CommandGuid.Equals(commandGuid));
                if(command != null)
                {
                    int removedIndex = command.Index;
                    models.Remove(command);
                    ChangeIndex(models, command, removedIndex, false);
                }
            }
        }
        public void RemoveCommand(string progressName, int index)
        {
            if (!AllCommands.ContainsKey(progressName))
            {
                throw new ArgumentException($"不存在的执行流程{progressName}");
            }
            var models = AllCommands[progressName];
            if (models != null)
            {
                var command = models.FirstOrDefault(p => p.Index.Equals(index));
                if (command != null)
                {
                    int removedIndex = command.Index;
                    models.Remove(command);
                    ChangeIndex(models, command, removedIndex, false);
                }
            }
        }
        public DataResult<Dictionary<string, ObservableCollection<IAppCommand>>> GetAllTasks()
        {
            if (AllCommands == null) return new DataResult<Dictionary<string, ObservableCollection<IAppCommand>>>("不存在任何执行流程");
            var dicCommands = new Dictionary<string, ObservableCollection<IAppCommand>>();
            foreach (var item in AllCommands)
            {
                if (item.Value == null) continue;
                var commands = new ObservableCollection<IAppCommand>();
                foreach (var subitem in item.Value)
                {
                    commands.Add(subitem);
                }
                dicCommands.Add(item.Key, commands);
            }
            return new DataResult<Dictionary<string, ObservableCollection<IAppCommand>>>(dicCommands);
        }
        public DataResult<ObservableCollection<IAppCommand>> GetTasks(string progressName)
        {
            if (AllCommands == null) return new DataResult<ObservableCollection<IAppCommand>>("不存在任何执行流程");
            var commands = new ObservableCollection<IAppCommand>();
            var models = AllCommands[progressName];
            if (models == null) return new DataResult<ObservableCollection<IAppCommand>>($"不存在的执行流程{progressName}");
            foreach (var item in models)
            {
                commands.Add(item);
            }
            return new DataResult<ObservableCollection<IAppCommand>>(commands);
        }
        public DataResult<IAppCommand> UpdateCommand(string progressName, IAppCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            if (!AllCommands.ContainsKey(progressName))
            {
                return new DataResult<IAppCommand>($"不存在的执行流程{progressName}");
            }
            var models = AllCommands[progressName];
            var model = models.FirstOrDefault(p => p.CommandGuid == command.CommandGuid);
            if (model == null)
            {
                return new DataResult<IAppCommand>($"不存在的命令{command.CommandName}");
            }
            model.CommandName = command.CommandName;
            model.CommandDescription = command.CommandDescription;
            model.LinkParameterName = command.LinkParameterName;
            model.CommandParameterValue = command.CommandParameterValue;
            model.CommandExecuteTimeOut = command.CommandExecuteTimeOut;
            model.IsEnable = command.IsEnable;
            return new DataResult<IAppCommand>(model);
        }
    }
}
