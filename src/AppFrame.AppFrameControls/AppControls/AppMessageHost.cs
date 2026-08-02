using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AppFrame.AppFrameControls.AppControls
{
    /// <summary>
    /// 消息窗口的容器
    /// </summary>
    public class AppMessageHost : ItemsControl
    {
        /// <summary>
        /// 判断指定项是否可以作为此容器的子项
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return base.IsItemItsOwnContainerOverride(item);
        }
        /// <summary>
        /// 创建指定用于显示在容器的项
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new AppMessagePopup();
        }
    }
}
