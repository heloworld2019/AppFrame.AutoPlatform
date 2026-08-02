using AppFrame.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AppFrame.WpfClient.Common.Converters
{

    //正常运行：#018E32（当前绿色）
    //待机就绪：#FFB300（黄色）
    //故障报警：#E53935（红色）
    //停机离线：#757575（灰色）





    internal class EnumToColors : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is DeviceStationState state)
            {
                switch (state)
                {
                    case DeviceStationState.Runing:
                        return "#018E32"; // 绿色
                    case DeviceStationState.Alarming:
                        return "#E74032"; //红色
                    default:
                        break;
                }
            }
            return "#DDDDDD";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
