using AppFrame.Core.AppFrameRepository;
using AppFrame.Entity.EntityBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Infrastructrue.AppFrameRepository
{
    public static class UnitOfWorkExtension
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
        public static IServiceCollection AddCustomRepository<TEntity, TRepisotory>(this IServiceCollection services)
                      where TEntity : AppEntityBase, new()
                      where TRepisotory : class, IAppRepository<TEntity>
        {
            services.AddScoped<IAppRepository<TEntity>, TRepisotory>();
            return services;
        }
    }
}
