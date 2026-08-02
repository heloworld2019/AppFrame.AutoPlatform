using AppFrame.Entity.Enum;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.WpfClient.Models
{
    public class DeviceStationInfo : BindableBase
    {
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
