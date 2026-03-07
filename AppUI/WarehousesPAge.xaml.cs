using Laboratory_ProductManager.Services.WarehouseServices;
using Laboratory_ProductManager.UIModels.WareHouseUIModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppUI
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
  

    // Сторінка відображає всі склади
    public partial class WarehousesPAge : Page
    {

        private IWarehouseRead _service;
        public WarehousesPAge(IWarehouseRead service)
        {
            InitializeComponent();
            _service = service;
            ShowWarehouses();
        }

        // Забираємо з xaml файлу ListBox і передаємо йому список, а xaml його вже форматує
        private void ShowWarehouses()
        {
            this.Warehouses.ItemsSource = _service.GetAllWarehouses();
        }

        private void OpenWarehousePage(object sender, MouseEventArgs e)
        {
            var warehouse = this.Warehouses.SelectedItem as WarehouseView;

            if (warehouse != null)
            {
                NavigationService.Navigate(new WarehouseProductsPage(_service, warehouse));
            }
        }
    }
}
