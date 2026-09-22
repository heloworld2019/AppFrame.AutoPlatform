using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Entity.Dto
{
    public class AppProductDto
    {
        public string Today           { get; set; }
        public string ProductModel    { get; set; }
        public string OrderNumber     { get; set; }
        public int    OrderPlan       { get; set; }
        public int    OrderProgress   { get; set; }
        public int    MonthlyPlan     { get; set; }
        public int    MonthlyProgress { get; set; }
        public int    DailyPlan       { get; set; }
        public int    DailyProgress   { get; set; }
        public int    OkNums          { get; set; }
        public int    NgNums          { get; set; }
    }
}
