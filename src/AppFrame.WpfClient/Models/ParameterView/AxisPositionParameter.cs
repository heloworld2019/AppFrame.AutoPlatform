using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.WpfClient.Models.ParameterView
{
    public class AxisPositionParameter : BindableBase
    {
        private double _position;
        private double _currentPosition;

        public AxisPositionParameter(string positionName, double position, double currentPosition)
        {
            PositionName = positionName;
            Position = position;
            CurrentPosition = currentPosition;
        }

        public string PositionName { get; set; }
        public double Position
        {
            get { return _position; }
            set { _position = value; RaisePropertyChanged(); }
        }
        public double CurrentPosition
        {
            get { return _currentPosition; }
            set { _currentPosition = value; RaisePropertyChanged(); }
        }

    }
}
