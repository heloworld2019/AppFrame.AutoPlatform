using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommon.Global
{
    /// <summary>
    /// 系统本地化字符串资源
    /// </summary>
    public class AppStringsResource
    {
        public const string ProductDateFormat = "yyyy-MM-dd";
        public const string IpAddressErrorString = "IP地址格式不正确";
        public const string ModbusTcpServerSubscribeAddressErrorString = "订阅的地址格式不正确,请检查!";
        public const string NetClientNoConnectedString = "客户端未连接,无法发送数据!";
        public const string ConfigInfoNotExistString = "配置信息文件参数错误,请检查!";
        public const string ConfigInfoSaveErrorString = "当前要保存的配置信息参数错误,请检查!";

        public const string IpAddressMatchString = "@\"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$\"";
    }
}
