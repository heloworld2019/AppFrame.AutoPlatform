using AppFrame.Application.Configs;
using AppFrame.Application.Products;
using AppFrame.Application.Vendors;
using AppFrame.Core.AppFrameCommand.Interfaces;
using AppFrame.Core.AppFrameConfig.Interfaces;
using AppFrame.Core.AppFrameIndustrialControl.Interfaces;
using AppFrame.Core.AppFrameLogger;
using AppFrame.Entity.AppPermission;
using AppFrame.Infrastructrue.AppFrameCommand;
using AppFrame.Infrastructrue.AppFrameConfig;
using AppFrame.Infrastructrue.AppFrameIndustrialControl;
using AppFrame.Infrastructrue.AppFrameLogger;
using AppFrame.Infrastructrue.AppFrameRepository;
using AppFrame.Infrastructrue.AppFrameRepository.Repository;
using AppFrame.Infrastructrue.AutoMappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Infrastructrue.DependencyInjection
{
    public sealed class AppIoc
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceCollection _services;
        public AppIoc()
        {
            _services = new ServiceCollection();
            RegisterService(_services);
            _serviceProvider = _services.BuildServiceProvider();
        }
        private void RegisterService(IServiceCollection services)
        {
            //仓储层
            services.AddUnitOfWork()
                    .AddCustomRepository<AppUser,UserRepository>()
                    .AddCustomRepository<AppRole,RoleRepository>()
                    .AddCustomRepository<UserRole,UserRoleRepository>()
                    .AddCustomRepository<RolePermission,RolePermissionRepository>()
                    .AddCustomRepository<AppPermission,PermissionRepository>();

            //基础层
            services.AddSingleton<IAppLogger, AppLogger>()
                    .AddSingleton<IAppConfiguration, AppConfiguration>()
                    .AddSingleton<IAppAxisBusFactory, AppAxisBusFactory>()
                    .AddSingleton<IAppCommandFactory, AppCommandFactory>()
                    .AddScoped<IAppCommandConfiguration, AppCommandConfiguration>()
                    .AddScoped<IAppSolenoidFactory, AppSolenoidFactory>()
                    .AddAutoMapper(typeof(AutoMapperProfile));
            //服务层
            services.AddScoped<IAppConfigService, AppConfigService>()
                    .AddScoped<IVendorService, VendorService>()
                    .AddScoped<IProductService, ProductService>();

        }
        public T Resolve<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
