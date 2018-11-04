using HLTAT.Business.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace HLTAT.Business.Service.DI
{
    public static class IServicesExtension
    {
        public static IServiceCollection RegisterProjectServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductFactory, ProductFactory>();

            return services;
        }
    }
}
