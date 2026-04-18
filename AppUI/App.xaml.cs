using Laboratory_ProductManager.AppUI.Services;
using Laboratory_ProductManager.AppUI.ViewModels;
using Laboratory_ProductManager.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Laboratory_ProductManager.AppUI
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
            services.AddProductManagerServices();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddTransient<WarehousesPAge>();
            services.AddTransient<WarehouseProductsPage>();
            services.AddTransient<ProductDetailsPage>();

            services.AddTransient<WarehousesViewModel>();
            services.AddTransient<WarehouseProductsViewModel>();
            services.AddTransient<ProductDetailViewModel>();
            services.AddSingleton<MainViewModel>();

            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = _container.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
