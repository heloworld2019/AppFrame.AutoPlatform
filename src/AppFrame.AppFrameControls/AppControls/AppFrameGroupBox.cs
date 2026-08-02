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
    public class AppFrameGroupBox : GroupBox
    {
        /// <summary>
        /// 头部位置类型
        /// </summary>
        [Bindable(true)]
        public GroupBoxHeaderStyleEnum HeaderType
        {
            get { return (GroupBoxHeaderStyleEnum)GetValue(HeaderTypeProperty); }
            set { SetValue(HeaderTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderTypeProperty =
            DependencyProperty.Register("HeaderType", typeof(GroupBoxHeaderStyleEnum), typeof(AppFrameGroupBox));

        [Bindable(true)]
        public GridLength DistanceToLeft
        {
            get { return (GridLength)GetValue(DistanceToLeftProperty); }
            set { SetValue(DistanceToLeftProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DistanceToLeft.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DistanceToLeftProperty =
            DependencyProperty.Register("DistanceToLeft", typeof(GridLength), typeof(AppFrameGroupBox));
        [Bindable(true)]
        public GridLength DistanceToRight
        {
            get { return (GridLength)GetValue(DistanceToRightProperty); }
            set { SetValue(DistanceToRightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DistanceToLeft.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DistanceToRightProperty =
            DependencyProperty.Register("DistanceToRight", typeof(GridLength), typeof(AppFrameGroupBox));
    }
}
