using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.WpfClient.Models
{
    public class DataBar : BindableBase
    {
        private string _content;

		public DataBar(string title, string content, string icon, string startBackColor, string endBackColor)
		{
			Title = title;
			Content = content;
			Icon = icon;
			StartBackColor = startBackColor;
			EndBackColor = endBackColor;
		}

		public string Title { get; set; }
		public string Content
		{
			get { return _content; }
			set { _content = value; RaisePropertyChanged(); }
		}
		public string Icon { get; set; }
		public string StartBackColor { get; set; }
		public string EndBackColor { get; set; }
	}
}
