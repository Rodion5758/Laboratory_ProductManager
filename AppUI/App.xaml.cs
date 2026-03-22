using Laboratory_ProductManager.Repositories;
using Laboratory_ProductManager.Services;
using Laboratory_ProductManager.AppUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Laboratory_ProductManager.Services.Interfaces;

namespace AppUI
{
    public partial class App : Application
    {
        public ServiceProvider ServiceProvider { get; private set; }
        private ServiceProvider _container;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _container = services.BuildServiceProvider();
            ServiceProvider = _container;
        }

        private void ConfigureServices(ServiceCollection services)
        {   
            // Repositories
            services.AddSingleton<IWarehouseRepository, WarehouseRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();

            // Services
            services.AddTransient<IWarehouseService, WarehouseService>();
            services.AddTransient<IProductService, ProductService>();

            services.AddTransient<WarehousesPAge>();
            services.AddTransient<WarehouseProductsPage>();
            services.AddTransient<ProductDetailsPage>();

            services.AddTransient<WarehousesViewModel>();
            services.AddTransient<WarehouseProductsViewModel>();
            services.AddTransient<ProductDetailViewModel>();
            services.AddSingleton<MainViewModel>();

            // MainWindow
            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = _container.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
