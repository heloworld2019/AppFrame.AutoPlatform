using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppFrame.WpfClient
{
    internal class RecipeEditViewModel
    {




        public ObservableCollection<MfcSettingDto> MfcSettingList { get; set; }









}



  




   public class MfcSettingDto : INotifyPropertyChanged
    {
        private int _no;
        public int No
        {
            get => _no;
            set { _no = value; OnPropertyChanged(); }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        private double _setpoint;
        public double Setpoint
        {
            get => _setpoint;
            set { _setpoint = value; OnPropertyChanged(); }
        }

        private string _unit;
        public string Unit
        {
            get => _unit;
            set { _unit = value; OnPropertyChanged(); }
        }

        private double _high;
        public double High
        {
            get => _high;
            set { _high = value; OnPropertyChanged(); }
        }

        private double _low;
        public double Low
        {
            get => _low;
            set { _low = value; OnPropertyChanged(); }
        }

        private bool _isCheck;
        public bool IsCheck
        {
            get => _isCheck;
            set { _isCheck = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

















}