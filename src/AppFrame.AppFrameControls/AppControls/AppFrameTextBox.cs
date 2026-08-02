using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AppFrame.AppFrameControls.AppControls
{
    public class AppFrameTextBox : TextBox
    {
        /// <summary>
        /// 输入框圆角
        /// </summary>
        [Bindable(true)]
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(AppFrameTextBox));
        /// <summary>
        /// 鼠标移入边框色
        /// </summary>
        [Bindable(true)]
        public Brush HoverBorderBrush
        {
            get { return (Brush)GetValue(HoverBorderBrushProperty); }
            set { SetValue(HoverBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoverBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.Register("HoverBorderBrush", typeof(Brush), typeof(AppFrameTextBox), new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// 这是水印
        /// </summary>
        [Bindable(true)]
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Watermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(AppFrameTextBox));

        /// <summary>
        /// 水印文字颜色
        /// </summary>
        [Bindable(true)]
        public Brush WatermarkColor
        {
            get { return (Brush)GetValue(WatermarkColorProperty); }
            set { SetValue(WatermarkColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FocusedBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkColorProperty =
            DependencyProperty.Register("WatermarkColor", typeof(Brush), typeof(AppFrameTextBox), new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// 正则匹配
        /// </summary>
        public string RegexStrings
        {
            get { return (string)GetValue(RegexStringsProperty); }
            set { SetValue(RegexStringsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RegexStrings.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegexStringsProperty =
            DependencyProperty.Register("RegexStrings", typeof(string), typeof(AppFrameTextBox));

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            //base.OnPreviewTextInput(e);
            if (string.IsNullOrWhiteSpace(RegexStrings)) 
            {
                return;
            } 
            e.Handled = new Regex(RegexStrings).IsMatch(e.Text);
        }
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = false;
            base.OnPreviewKeyDown(e);   
        }
    }
}
