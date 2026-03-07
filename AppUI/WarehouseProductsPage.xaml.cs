using Laboratory_ProductManager.Services.WarehouseServices;
using Laboratory_ProductManager.UIModels.ProductUIModel;
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
    public partial class WarehouseProductsPage : Page
    {

        private IWarehouseRead _service;
        private WarehouseView _view;
        public WarehouseProductsPage(IWarehouseRead service, WarehouseView view)
        {
            InitializeComponent();
            _service = service;
            _view = view;

            var products = _service.GetProductsByWarehouseId(view.ID);

            Products.ItemsSource = products;
        }

        private void OpenProductDetailsPage(object sender, MouseButtonEventArgs e)
        {
            var productn = this.Products.SelectedItem as ProductView;

            if (productn != null)
            {
                NavigationService.Navigate(new ProductDetailsPage(_service, productn));
            }
        }

        private void BackButton(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
