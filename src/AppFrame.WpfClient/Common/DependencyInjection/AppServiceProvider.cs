using AppFrame.Application.AppCommunication;
using AppFrame.Application.AppMotion;
using AppFrame.Application.AppSolenoids;
using AppFrame.Application.Commands;
using AppFrame.Application.Configs;
using AppFrame.Application.Elements;
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
using AppFrame.WpfClient.Common.AutoMappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.WpfClient.Common.DependencyInjection
{
    public class AppServiceProvider  // 依赖注入服务提供者类，负责注册和解析应用程序中的服务和依赖项
    {
        public AppServiceProvider()
        {
            _services = new ServiceCollection();
            RegisterService(_services);
            _serviceProvider = _services.BuildServiceProvider();
        }
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceCollection _services;
        private void RegisterService(IServiceCollection services)
        {
            //仓储层
            services.AddUnitOfWork()
                    .AddCustomRepository<AppUser, UserRepository>()
                    .AddCustomRepository<AppRole, RoleRepository>()
                    .AddCustomRepository<UserRole, UserRoleRepository>()
                    .AddCustomRepository<RolePermission, RolePermissionRepository>()
                    .AddCustomRepository<AppPermission, PermissionRepository>();

            //基础层
            services.AddSingleton<IAppLogger, AppLogger>()
                    .AddSingleton<IAppConfiguration, AppConfiguration>()
                    .AddSingleton<IAppAxisBusFactory, AppAxisBusFactory>()
                    .AddSingleton<IAppCommandFactory, AppCommandFactory>()
                    .AddSingleton<IAppCommandConfiguration, AppCommandConfiguration>()
                    .AddScoped<IAppSolenoidFactory, AppSolenoidFactory>()
                    .AddAutoMapper(typeof(AutoMapperProfile));
            //服务层
            services.AddScoped<IAppConfigService, AppConfigService>()
                    .AddScoped<IVendorService, VendorService>()
                    .AddScoped<IProductService, ProductService>()
                    .AddScoped<IAppElementService, AppElementService>()
                    .AddScoped<IAppSolenoidService, AppSolenoidService>()
                    .AddScoped<IAppCommunicationService, AppCommunicationService>()
                    .AddScoped<IEtcBusService, EtcBusService>()
                    .AddScoped<IAppMotionService, AppMotionService>()
                    .AddScoped<ICommandService, CommandService>();
        }
        public T Resolve<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
