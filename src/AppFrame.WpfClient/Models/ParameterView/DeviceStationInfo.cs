using AppFrame.Entity.Enum;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.WpfClient.Models

{

    //只保存数据，只负责发出属性变更通知，不负责 “什么时候变”。  Model 只是数据载体，不能依赖设备服务，会造成耦合严重。
    public class DeviceStationInfo : BindableBase
    {
        private string _stationName;
        private DeviceStationState _stationState;
        private string _runingInfo;
        private string _alarmInfo;

		public DeviceStationInfo(string stationName, DeviceStationState stationState, string runingInfo, string alarmInfo)
		{
			StationName = stationName;
			StationState = stationState;
			RuningInfo = runingInfo;
			AlarmInfo = alarmInfo;
		}



		public string StationName { get; set; }	
        public DeviceStationState StationState
		{
			get { return _stationState; }
			set { _stationState = value; RaisePropertyChanged(); }
		}		
        public string RuningInfo
		{
			get { return _runingInfo; }
			set { _runingInfo = value; RaisePropertyChanged(); }
		}	
		public string AlarmInfo
		{
			get { return _alarmInfo; }
			set { _alarmInfo = value; RaisePropertyChanged(); }
		}

	}
}
