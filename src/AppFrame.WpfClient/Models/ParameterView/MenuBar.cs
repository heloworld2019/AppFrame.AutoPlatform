using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.WpfClient.Models
{
    public class MenuBar : BindableBase
    {
        public MenuBar(string icon, string title, string navigateViewName,string titleColor)
        {
            Icon = icon;
            Title = title;
            NavigateViewName = navigateViewName;
            TitleColor = titleColor;
        }

        public string Icon { get; set; }
        public string Title { get; set; }
        public string NavigateViewName { get; set; }
        private string _titleColor;

        public string TitleColor
        {
            get { return _titleColor; }
            set { _titleColor = value; RaisePropertyChanged(); }
        }

    }
}
