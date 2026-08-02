using AppFrame.AppFrameControls.AppControls;
using AppFrame.AppFrameControls.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AppFrame.AppFrameControls.Common.Global
{
    /// <summary>
    /// 管理系统消息弹窗程序
    /// </summary>
    public class AppMessage
    {
        /// <summary>
        /// 全局消息窗口容器的唯一标识
        /// </summary>
        public const string GlobalMessageHostToken = "GlaobalMessage";
        /// <summary>
        /// 管理消息容器组件
        /// </summary>
        internal static readonly Dictionary<string, AppMessageHost> _messageHosts = new Dictionary<string, AppMessageHost>();

        /// <summary>
        /// 根据依赖对象 获取依赖属性的值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetHostToken(DependencyObject obj)
        {
            return (string)obj.GetValue(HostTokenProperty);
        }
        /// <summary>
        /// 根据依赖对象 设置依赖属性的值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetHostToken(DependencyObject obj, string value)
        {
            obj.SetValue(HostTokenProperty, value);
        }
        // Using a DependencyProperty as the backing store for HostToken.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HostTokenProperty =
            DependencyProperty.RegisterAttached("HostToken", typeof(string), typeof(AppMessage), new PropertyMetadata(OnTokenChanged)); //注册附加属性

        private static void OnTokenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (!(d is AppMessageHost host)) return;
                var token = GetHostToken(host);
                if (_messageHosts.ContainsKey(token)) _messageHosts.Remove(token);
                _messageHosts.Add(token, host);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void DefaultTpis(string message, string token = "", double delayTime = 3) => Show(message, token, delayTime, MessageStyleEnum.Default);
        public static void SucccessTpis(string message, string token = "", double delayTime = 3) => Show(message, token, delayTime, MessageStyleEnum.Success);
        public static void WarningTpis(string message, string token = "", double delayTime = 3) => Show(message, token, delayTime, MessageStyleEnum.Warning);
        public static void ErrorTpis(string message, string token = "", double delayTime = 3) => Show(message, token, delayTime, MessageStyleEnum.Error);
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="token"></param>
        /// <param name="delayTime"></param>
        /// <param name="type"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static void Show(string message, string token, double delayTime, MessageStyleEnum type)
        {
            if (string.IsNullOrWhiteSpace(token)) token = GlobalMessageHostToken;
            if (!_messageHosts.ContainsKey(token)) return;
            var host = _messageHosts.FirstOrDefault(p => p.Key.Equals(token)).Value;
            if (host == null) return;
            //此处使用委托跨线程调用控件
            host.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
            {
                host.Items.Add(new AppMessagePopup() { MessageType = type, Message = message, CloseDelayTime = delayTime, Uid = Guid.NewGuid().ToString() });
            }));
        }
    }
}
