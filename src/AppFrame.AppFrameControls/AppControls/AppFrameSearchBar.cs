using AppFrame.AppFrameControls.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppFrame.AppFrameControls.AppControls
{
    /// <summary>
    /// 搜索框
    /// </summary>
    public class AppFrameSearchBar : ContentControl
    {
        /// <summary>
        /// 主题
        /// </summary>
        public SearchBarEnum ThemeType
        {
            get { return (SearchBarEnum)GetValue(ThemeTypeProperty); }
            set { SetValue(ThemeTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThemeTypeProperty =
            DependencyProperty.Register("ThemeType", typeof(SearchBarEnum), typeof(AppFrameSearchBar), new PropertyMetadata(SearchBarEnum.Dark));

        /// <summary>
        /// 水印文字
        /// </summary>
        public string WaterText
        {
            get { return (string)GetValue(WaterTextProperty); }
            set { SetValue(WaterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WaterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WaterTextProperty =
            DependencyProperty.Register("WaterText", typeof(string), typeof(AppFrameSearchBar), new PropertyMetadata("搜你想搜索的"));


        /// <summary>
        /// 搜索命令
        /// </summary>
        public ICommand SearchCommand
        {
            get { return (ICommand)GetValue(SearchCommandProperty); }
            set { SetValue(SearchCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchCommandProperty =
            DependencyProperty.Register("SearchCommand", typeof(ICommand), typeof(AppFrameSearchBar));
        /// <summary>
        /// 搜索命令参数
        /// </summary>
        public object SearchCommandParameter
        {
            get { return (object)GetValue(SearchCommandParameterProperty); }
            set { SetValue(SearchCommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchCommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchCommandParameterProperty =
            DependencyProperty.Register("SearchCommandParameter", typeof(object), typeof(AppFrameSearchBar));
    }
}
