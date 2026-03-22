using Laboratory_ProductManager.AppUI.ViewModels;
using Laboratory_ProductManager.Repositories;
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
    public partial class WarehousesPAge : Page
    {
        public WarehousesPAge(WarehousesViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
