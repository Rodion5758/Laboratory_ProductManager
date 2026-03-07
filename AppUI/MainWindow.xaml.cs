using Laboratory_ProductManager.Services.WarehouseServices;
using Laboratory_ProductManager.UIModels.WareHouseUIModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IWarehouseRead _warehouseService;
        public MainWindow(IWarehouseRead warehouseService)
        {
            InitializeComponent();
            _warehouseService = warehouseService;
            var warehouses = _warehouseService.GetAllWarehouses();
            FrameManager.Navigate(new WarehousesPAge(_warehouseService));
 
        }
    }
}