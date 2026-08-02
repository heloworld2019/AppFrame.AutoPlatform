using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.WpfClient.Models.ParameterView
{
    public class SolenoidParameter : BindableBase
    {
        private bool _value;
        private double _doubleValue;

        public SolenoidParameter(string parameterName, string parameterDescription, bool value, double doubleValue, bool isBool, string icon)
        {
            ParameterName = parameterName;
            ParameterDescription = parameterDescription;
            BoolValue = value;
            DoubleValue = doubleValue;
            IsBool = isBool;
            IsDouble = !IsBool;
            Icon = icon;
        }

        public string ParameterName { get; set; }
        public string ParameterDescription { get; set; }
        public bool BoolValue { get => _value; set { _value = value; RaisePropertyChanged(); } }
        public double DoubleValue { get => _doubleValue; set { _doubleValue = value; RaisePropertyChanged(); } }
        public bool IsBool { get; set; }
        public bool IsDouble { get; set; }
        public string Icon { get; set; }
    }
}
