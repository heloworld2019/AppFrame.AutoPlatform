using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.WpfClient.Models
{
    public class AppProductVo : BindableBase
    {
        private string _today;
        private string _productModel;
        private string _orderNumber;
        private int _orderPlan;
        private int _orderProgress;
        private int _monthlyPlan;
        private int _monthlyProgress;
        private int _dailyPlan;
        private int _dailyProgress;
        private int _okNums;
        private int _ngNums;

        public AppProductVo(string today, 
                            string productModel, 
                            string orderNumber, 
                            int orderPlan, 
                            int orderProgress, 
                            int monthlyPlan, 
                            int monthlyProgress, 
                            int dailyPlan, 
                            int dailyProgress, 
                            int okNums, 
                            int ngNums)
        {
            Today = today;
            ProductModel = productModel;
            OrderNumber = orderNumber;
            OrderPlan = orderPlan;
            OrderProgress = orderProgress;
            MonthlyPlan = monthlyPlan;
            MonthlyProgress = monthlyProgress;
            DailyPlan = dailyPlan;
            DailyProgress = dailyProgress;
            OkNums = okNums;
            NgNums = ngNums;
        }

        public string Today { get => _today; set { _today = value; RaisePropertyChanged(); } }
        public string ProductModel { get => _productModel; set { _productModel = value; RaisePropertyChanged(); } }
        public string OrderNumber { get => _orderNumber; set { _orderNumber = value; RaisePropertyChanged(); } }
        public int OrderPlan { get => _orderPlan; set { _orderPlan = value; RaisePropertyChanged(); } }
        public int OrderProgress { get => _orderProgress; set { _orderProgress = value; RaisePropertyChanged(); } }
        public int MonthlyPlan { get => _monthlyPlan; set { _monthlyPlan = value; RaisePropertyChanged(); } }
        public int MonthlyProgress { get => _monthlyProgress; set { _monthlyProgress = value; RaisePropertyChanged(); } }
        public int DailyPlan { get => _dailyPlan; set { _dailyPlan = value; RaisePropertyChanged(); } }
        public int DailyProgress { get => _dailyProgress; set { _dailyProgress = value; RaisePropertyChanged(); } }
        public int OkNums { get => _okNums; set { _okNums = value; RaisePropertyChanged(); } }
        public int NgNums { get => _ngNums; set { _ngNums = value; RaisePropertyChanged(); } }
    }
}
