using Laboratory_ProductManager.Services.WarehouseServices;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace AppUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    

    // тут реалізовується Inversion of control
    public partial class App : Application
    {


        // Це фактично керівник залежностями
        private ServiceProvider _container;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _container = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {   
            // Збираємо всіх підлеглих цьому контейнеру
            // Вказуємо за що він відповідає
            services.AddSingleton<IWarehouseRead, WarehouseRead>();
            services.AddSingleton<MainWindow>();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // На запуску програмии просимо в цього контейнера головне вікно
            // І він автоматично за попердніми інструкціями створює новий сервіс для складу
            var mainWindow = _container.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }

}
