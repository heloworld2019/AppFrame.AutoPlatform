using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppFrame.AppFrameControls.AppControls
{
    public class AppLoading : ContentControl
    {
        /// <summary>
        /// 动画开关
        /// </summary>
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
        // Using a DependencyProperty as the backing store for IsActive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(AppLoading));
        /// <summary>
        /// 加载动画时的提示消息
        /// </summary>
        public string TipMessage
        {
            get { return (string)GetValue(TipMessageProperty); }
            set { SetValue(TipMessageProperty, value); }
        }
        // Using a DependencyProperty as the backing store for TipMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TipMessageProperty =
            DependencyProperty.Register("TipMessage", typeof(string), typeof(AppLoading),new PropertyMetadata("Loading"));
        /// <summary>
        /// 动画条背景
        /// </summary>
        public Brush BackBrush
        {
            get { return (Brush)GetValue(BackBrushProperty); }
            set { SetValue(BackBrushProperty, value); }
        }
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackBrushProperty =
            DependencyProperty.Register("BackBrush", typeof(Brush), typeof(AppLoading),new PropertyMetadata(new SolidColorBrush(Color.FromRgb(68, 92, 90))));

        /// <summary>
        /// 动画条
        /// </summary>
        public Brush ScrollBrush
        {
            get { return (Brush)GetValue(ScrollBrushProperty); }
            set { SetValue(ScrollBrushProperty, value); }
        }
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScrollBrushProperty =
            DependencyProperty.Register("ScrollBrush", typeof(Brush), typeof(AppLoading), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(51, 171, 160))));
    }
}
