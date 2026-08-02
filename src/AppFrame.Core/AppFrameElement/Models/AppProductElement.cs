using AppFrame.Core.AppFrameConfig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Models
{
    public class AppProductElement : AppConfigParameter
    {
        public AppProductElement()
        {

        }

        public AppProductElement(int index,
                                 int parameterId,
                                 string parameterName,
                                 string parameterGroupName,
                                 string parameterDescription,
                                 string parameterCategory,
                                 bool enable, 
                                 string today, 
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
            Index = index;
            ParameterId = parameterId;
            ParameterName = parameterName;
            ParameterGroupName = parameterGroupName;
            ParameterDescription = parameterDescription;
            ParameterCategoryName = parameterCategory;
            Enable = enable;
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

        public string Today { get; set; }
        public string ProductModel { get; set; }
        public string OrderNumber { get; set; }
        public int OrderPlan { get; set; }
        public int OrderProgress { get; set; } 
        public int MonthlyPlan { get; set; }
        public int MonthlyProgress { get; set; }
        public int DailyPlan { get; set; }
        public int DailyProgress { get; set; }
        public int OkNums { get; set; }
        public int NgNums { get; set; }
    }
}
