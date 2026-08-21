using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using Ecom.infrastructure.Repositiries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure
{
    public static class InfrastructureRegisteration
    {
        public static IServiceCollection InfrastructureConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));
            //services.AddScoped < ICategoryRepositry,CategoryRepositry>();
            //services.AddScoped <IProductRepository, ProductRepositry>();
            //services.AddScoped <IPhotoRepositry, PhotoRepositry>();
            services.AddScoped <IUnitOfWork, UnitOfWork>();
            services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("EcomDatabase"));
            });


            return services;
        }
    }
}
