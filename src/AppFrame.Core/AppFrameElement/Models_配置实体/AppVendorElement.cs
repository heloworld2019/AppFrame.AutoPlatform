using AppFrame.Core.AppFrameConfig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Models
{
    public class AppVendorElement : AppConfigParameter
    {
        public AppVendorElement()
        {

        }
        public AppVendorElement(int index,
                                int parameterId,
                                string parameterName,
                                string parameterGroupName,
                                string parameterDescription,
                                string parameterCategory,
                                bool enable,
                                string vendorName,
                                string officeAddress, 
                                string productionBase, 
                                string phone, 
                                string webSite, 
                                string salesPhone, 
                                string technicalPhone,
                                bool isCurrent,
                                string appName)
        {
            Index = index;
            ParameterId = parameterId;
            ParameterName = parameterName;
            ParameterGroupName = parameterGroupName;
            ParameterDescription = parameterDescription;
            ParameterCategoryName = parameterCategory;
            Enable = enable;
            VendorName = vendorName;
            OfficeAddress = officeAddress;
            ProductionBase = productionBase;
            Phone = phone;
            WebSite = webSite;
            SalesPhone = salesPhone;
            TechnicalPhone = technicalPhone;
            IsCurrent = isCurrent;
            AppName = appName;
        }
        public string VendorName { get; set; }
        public string OfficeAddress { get; set; }
        public string ProductionBase { get; set; }
        public string Phone { get; set; }
        public string WebSite { get; set; }
        public string SalesPhone { get; set; }
        public string TechnicalPhone { get; set; }
        public bool IsCurrent { get; set; }
        public string AppName { get; set; }
    }
}
