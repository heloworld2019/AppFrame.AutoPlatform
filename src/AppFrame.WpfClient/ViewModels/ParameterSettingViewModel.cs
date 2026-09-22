using AppFrame.WpfClient.Models.ParameterView;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppFrame.WpfClient.ViewModels
{
    public class ParameterSettingViewModel : BindableBase
    {
        public ParameterSettingViewModel()
        {
            ParameterPageIndex = 0;
            GetParameter();
        }
        private int _parameterPageIndex;
        private ObservableCollection<SolenoidParameter> _solenoidParameters;
        private ObservableCollection<AxisPositionParameter> _axisPositionParameters;
        /// <summary>
        /// 参数页索引
        /// </summary>
        public int ParameterPageIndex
        {
            get { return _parameterPageIndex; }
            set { _parameterPageIndex = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 气阀参数
        /// </summary>
        public ObservableCollection<SolenoidParameter> SolenoidParameters
        {
            get { return _solenoidParameters; }
            set { _solenoidParameters = value; RaisePropertyChanged(); }
        }




        /// <summary>
        /// 轴位置参数
        /// </summary>
        public ObservableCollection<AxisPositionParameter> AxisPositionParameters
        {
            get { return _axisPositionParameters; }
            set { _axisPositionParameters = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 切换参数页命令
        /// </summary>
        public ICommand SwitchParameterPageCommand => new DelegateCommand<string>(SwitchParameterPage);

        private void SwitchParameterPage(string index)
        {
            if (!int.TryParse(index, out int result))
            {
                ParameterPageIndex = 0;
                return;
            }
            ParameterPageIndex = result;

        }



       
        private void GetParameter()
        {
            SolenoidParameters = new ObservableCollection<SolenoidParameter>()
            {
                new SolenoidParameter("动点感应","屏蔽动点感应器后，将通过动点动作时间来控制气缸到达动点位",false,0,true,"\ue63a"),
                new SolenoidParameter("原点感应","屏蔽原点感应器后，将通过原点动作时间来控制气缸到达原点位",false,0,true,"\ue63a"),
                new SolenoidParameter("动点报警延时","控制气缸到达动点位后，在设定时间内未到达将触发动点感应报警",false,3.0,false,"\ue650"),
                new SolenoidParameter("原点报警延时","控制气缸到达原点位后，在设定时间内未到达将触发原点感应报警",false,3.0,false,"\ue650"),
                new SolenoidParameter("动点到达延时","控制气缸到达动点位的延时时间",false,0.2,false,"\ue650"),
                new SolenoidParameter("原点到达延时","控制气缸到达原点位的延时时间",false,0.2,false,"\ue650"),
                new SolenoidParameter("动点动作时间","动点感应屏蔽后，通过此时间判断是否达到动点位",false,1.0,false, "\ue650"),
                new SolenoidParameter("原点动作时间","原点感应屏蔽后，通过此时间判断是否达到原点位",false,1.0,false, "\ue650"),
                new SolenoidParameter("zhang","xiadao",false,1.0,false, "\ue650"),
            };



            AxisPositionParameters = new ObservableCollection<AxisPositionParameter>()
            {
                new AxisPositionParameter("默认位置0",-10.15,0.00),
                new AxisPositionParameter("默认位置1",-10.15,0.00),
                new AxisPositionParameter("默认位置2",-10.15,0.00),
                new AxisPositionParameter("默认位置3",-10.15,0.00),
                new AxisPositionParameter("默认位置4",-10.15,0.00),
                new AxisPositionParameter("默认位置5",-10.15,0.00),
                new AxisPositionParameter("默认位置6",-10.15,0.00),
                new AxisPositionParameter("默认位置7",-10.15,0.00),
                new AxisPositionParameter("默认位置8",-10.15,0.00),
                new AxisPositionParameter("默认位置9",-10.15,0.00),
            };
        }
    }
}
