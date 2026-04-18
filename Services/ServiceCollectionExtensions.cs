using Laboratory_ProductManager.Repositories;
using Laboratory_ProductManager.Repositories.Product;
using Laboratory_ProductManager.Repositories.Storage;
using Laboratory_ProductManager.Repositories.Warehouse;
using Laboratory_ProductManager.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Laboratory_ProductManager.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProductManagerServices(this IServiceCollection services)
        {
            services.AddSingleton<JsonStorage>();
            services.AddSingleton<IWarehouseRepository, WarehouseRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddTransient<IWarehouseService, WarehouseService>();
            services.AddTransient<IProductService, ProductService>();

            return services;
        }
    }
}
