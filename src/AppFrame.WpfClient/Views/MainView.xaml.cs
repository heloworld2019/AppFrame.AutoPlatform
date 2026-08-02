using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AppFrame.WpfClient.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            header.MouseDown += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount >= 2)
                {
                    WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
                }
                if (e.LeftButton == MouseButtonState.Pressed)
                    DragMove();               
            };
            btMin.Click += (s, e) =>
            {
                WindowState = WindowState.Minimized;
            };
            btMax.Click += (s, e) =>
            {
                WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            };
            btClose.Click += (s, e) =>
            {
                Close();
            };
        }
    }
}
