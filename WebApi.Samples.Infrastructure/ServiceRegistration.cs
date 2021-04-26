using Microsoft.Extensions.DependencyInjection;
using WebApi.Samples.Application.Interfaces;
using WebApi.Samples.Infrastructure.Repositories;

namespace WebApi.Samples.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}