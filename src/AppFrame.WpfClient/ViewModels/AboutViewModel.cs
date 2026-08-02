using AppFrame.Application.Vendors;
using AppFrame.Entity.Dto;
using AppFrame.Infrastructrue.DependencyInjection;
using AppFrame.WpfClient.Common.DependencyInjection;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppFrame.WpfClient.ViewModels
{
    public class AboutViewModel : BindableBase
    {
        private readonly AppServiceProvider _appIoc;
        private readonly IVendorService _vendorService;
        public AboutViewModel(AppServiceProvider appIoc)
        {
            _appIoc = appIoc;
            _vendorService = _appIoc.Resolve<IVendorService>();
            GetVendor();
        }
        private AppVendorDto _vendor;

        public AppVendorDto Vendor
        {
            get { return _vendor; }
            set { _vendor = value; RaisePropertyChanged(); }
        }
        private void GetVendor()
        {
            try
            {
                var result = _vendorService.GetAppVendor();
                if (result.Statu)
                    Vendor = result.Data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }         
        }
    }
}
