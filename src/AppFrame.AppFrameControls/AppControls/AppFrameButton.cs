using AppFrame.AppFrameControls.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppFrame.AppFrameControls.AppControls
{
    public class AppFrameButton : Button
    {
        /// <summary>
        /// 按钮类型
        /// </summary>
        [Bindable(true)]
        public ButtonStyleEnum Type
        {
            get { return (ButtonStyleEnum)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(ButtonStyleEnum), typeof(AppFrameButton), new PropertyMetadata(ButtonStyleEnum.Default));

        /// <summary>
        /// 加载时显示的内容
        /// </summary>
        [Bindable(true)]
        public object LoadingContent
        {
            get { return (object)GetValue(LoadingContentProperty); }
            set { SetValue(LoadingContentProperty, value); }
        }
        // Using a DependencyProperty as the backing store for _loadingContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadingContentProperty =
            DependencyProperty.Register("LoadingContent", typeof(object), typeof(AppFrameButton));

        /// <summary>
        /// 指示按钮是否是加载状态
        /// </summary>
        [Bindable(true)]
        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }
        // Using a DependencyProperty as the backing store for IsLoading.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(AppFrameButton), new PropertyMetadata(false));

        /// <summary>
        /// 按钮圆角
        /// </summary>
        [Bindable(true)]
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(AppFrameButton));

        [Bindable(true)]
        public Brush HoverBackground
        {
            get { return (Brush)GetValue(HoverBackgroundProperty); }
            set { SetValue(HoverBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoverBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register("HoverBackground", typeof(Brush), typeof(AppFrameButton), new PropertyMetadata(Brushes.Transparent));

        [Bindable(true)]
        public Brush HoverBorderBrush
        {
            get { return (Brush)GetValue(HoverBorderBrushProperty); }
            set { SetValue(HoverBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoverBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.Register("HoverBorderBrush", typeof(Brush), typeof(AppFrameButton), new PropertyMetadata(Brushes.Transparent));
    }
}
