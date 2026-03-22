
using Laboratory_ProductManager.UIModels.ProductUIModel;
using Laboratory_ProductManager.UIModels.WareHouseUIModel;
using Laboratory_ProductManager.Common.Enums;
using Laboratory_ProductManager.Services.Interfaces;

using System.Collections.ObjectModel;

namespace Laboratory_ProductManager.AppUI.ViewModels
{
    public class WarehouseProductsViewModel : BaseViewModel
    {
        private readonly IWarehouseService _warehouseService;
        private readonly MainViewModel _mainViewModel;
        private RelayCommand _backCommand;

        private WarehouseView _warehouse;
        public WarehouseView Warehouse
        {
            get => _warehouse;
            set => SetProperty(ref _warehouse, value);
        }

        private ObservableCollection<ProductView> _products;
        public ObservableCollection<ProductView> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        private ProductView _selectedProduct;
        public ProductView SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                if (SetProperty(ref _selectedProduct, value) && value != null)
                {
                    _mainViewModel.NavigateToProductDetail(value.ID);
                }
            }
        }

        public RelayCommand BackCommand =>
            _backCommand ??= new RelayCommand(o => GoBack());

        public WarehouseProductsViewModel(IWarehouseService warehouseService, Guid warehouseId, MainViewModel mainViewModel)
        {
            _warehouseService = warehouseService;
            _mainViewModel = mainViewModel;
            LoadWarehouse(warehouseId);
        }

        private void LoadWarehouse(Guid warehouseId)
        {
            var dbWarehouse = _warehouseService.GetWarehouseById(warehouseId);

            var location = Enum.Parse<WareHouseLocation>(dbWarehouse.Location);
            Warehouse = new WarehouseView(dbWarehouse.Id, dbWarehouse.Name, location);


            Products = new ObservableCollection<ProductView>();
            foreach (var product in dbWarehouse.Products)
            {
                Products.Add(new ProductView(
                    product.Id,
                    dbWarehouse.Id,
                    product.Name,
                    product.Price,
                    product.Quantity,
                    product.Category,
                    "Product"
                ));
            }
        }

        private void GoBack()
        {
            _mainViewModel.NavigateToWarehouses();
        }
    }
}
