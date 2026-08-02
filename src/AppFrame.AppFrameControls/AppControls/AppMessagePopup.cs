using AppFrame.AppFrameControls.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AppFrame.AppFrameControls.AppControls
{
    /// <summary>
    /// 消息窗口控件
    /// </summary>
    public class AppMessagePopup : ContentControl
    {
        public AppMessagePopup()
        {
            CloseDelayTime = 3; //给定一个默认值
            Loaded += AppMessagePopup_Loaded;
        }

        private async void AppMessagePopup_Loaded(object sender, RoutedEventArgs e)
        {
            if(Parent is AppMessageHost host)
            {
                await Task.Delay(TimeSpan.FromSeconds(CloseDelayTime));
                host.Items.Remove(this);
            }
        }

        /// <summary>
        /// 消息类型
        /// </summary>
        [Bindable(true)]
        public MessageStyleEnum MessageType
        {
            get { return (MessageStyleEnum)GetValue(MessageTypeProperty); }
            set { SetValue(MessageTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageTypeProperty =
            DependencyProperty.Register("MessageType", typeof(MessageStyleEnum), typeof(AppMessagePopup), new PropertyMetadata(MessageStyleEnum.Success));

        /// <summary>
        /// 消息内容
        /// </summary>
        [Bindable(true)]
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Message.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(AppMessagePopup));


        /// <summary>
        /// 关闭消息窗口延时 [单位：秒]
        /// </summary>
        public double CloseDelayTime { get; set; }
    }
}
