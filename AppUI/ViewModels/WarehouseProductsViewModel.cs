using Laboratory_ProductManager.Services.DTO;
using Laboratory_ProductManager.Services.Interfaces;
using System.Collections.ObjectModel;
using Laboratory_ProductManager.AppUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Laboratory_ProductManager.AppUI.ViewModels
{
    public class WarehouseProductsViewModel : BaseViewModel
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IProductService _productService;
        private readonly INavigationService _navigation;
        private readonly List<ProductListDto> _allProducts = new List<ProductListDto>();
        private AsyncRelayCommand _backCommand;
        private AsyncRelayCommand _editWarehouseCommand;
        private AsyncRelayCommand _saveWarehouseCommand;
        private AsyncRelayCommand _cancelEditCommand;
        private AsyncRelayCommand _openProductCommand;
        private AsyncRelayCommand _addProductCommand;
        private AsyncRelayCommand _deleteProductCommand;
        private Guid _warehouseId;
        private bool _isEditing;
        private string _editName;
        private string _editLocation;
        private string _productSearchText;
        private string _productSortOption = "Name";
        private string _newProductName;
        private string _newProductPrice;
        private string _newProductQuantity = "1";
        private string _newProductCategory = "Electronics";
        private string _newProductDescription;

        private WarehouseDetailDto _warehouse;
        public WarehouseDetailDto Warehouse
        {
            get => _warehouse;
            set
            {
                if (SetProperty(ref _warehouse, value) && value != null)
                {
                    EditName = value.Name;
                    EditLocation = value.Location;
                }
            }
        }

        private ObservableCollection<ProductListDto> _products;
        public ObservableCollection<ProductListDto> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        public string ProductSearchText
        {
            get => _productSearchText;
            set
            {
                if (SetProperty(ref _productSearchText, value))
                {
                    ApplyProductFilterAndSort();
                }
            }
        }

        public string ProductSortOption
        {
            get => _productSortOption;
            set
            {
                if (SetProperty(ref _productSortOption, value))
                {
                    ApplyProductFilterAndSort();
                }
            }
        }

        public string NewProductName
        {
            get => _newProductName;
            set => SetProperty(ref _newProductName, value);
        }

        public string NewProductPrice
        {
            get => _newProductPrice;
            set => SetProperty(ref _newProductPrice, value);
        }

        public string NewProductQuantity
        {
            get => _newProductQuantity;
            set => SetProperty(ref _newProductQuantity, value);
        }

        public string NewProductCategory
        {
            get => _newProductCategory;
            set => SetProperty(ref _newProductCategory, value);
        }

        public string NewProductDescription
        {
            get => _newProductDescription;
            set => SetProperty(ref _newProductDescription, value);
        }

        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                if (SetProperty(ref _isEditing, value))
                {
                    OnPropertyChanged(nameof(IsNotEditing));
                }
            }
        }

        public bool IsNotEditing => !IsEditing;

        public string EditName
        {
            get => _editName;
            set => SetProperty(ref _editName, value);
        }

        public string EditLocation
        {
            get => _editLocation;
            set => SetProperty(ref _editLocation, value);
        }

        public ObservableCollection<string> LocationOptions { get; } =
            new ObservableCollection<string> { "Kyiv", "Lviv", "Mankivka", "Uman", "Yahotyn", "Byku", "Talne" };

        public ObservableCollection<string> ProductSortOptions { get; } =
            new ObservableCollection<string> { "Name", "Category", "Price", "Quantity" };

        public ObservableCollection<string> CategoryOptions { get; } =
            new ObservableCollection<string> { "Food", "Electronics", "Clothing", "Medicine", "Souvenirs" };

        private ProductListDto _selectedProduct;
        public ProductListDto SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                SetProperty(ref _selectedProduct, value);
            }
        }

        public AsyncRelayCommand OpenProductCommand =>
            _openProductCommand ??= new AsyncRelayCommand(o => OpenProductAsync(), o => IsNotBusy && SelectedProduct != null && Warehouse != null);

        public AsyncRelayCommand AddProductCommand =>
            _addProductCommand ??= new AsyncRelayCommand(o => AddProductAsync(), o => IsNotBusy && Warehouse != null);

        public AsyncRelayCommand DeleteProductCommand =>
            _deleteProductCommand ??= new AsyncRelayCommand(o => DeleteProductAsync(), o => IsNotBusy && SelectedProduct != null);

        public AsyncRelayCommand BackCommand =>
            _backCommand ??= new AsyncRelayCommand(o => GoBackAsync(), o => IsNotBusy);

        public AsyncRelayCommand EditWarehouseCommand =>
            _editWarehouseCommand ??= new AsyncRelayCommand(o => EditWarehouseAsync(), o => IsNotBusy && IsNotEditing && Warehouse != null);

        public AsyncRelayCommand SaveWarehouseCommand =>
            _saveWarehouseCommand ??= new AsyncRelayCommand(o => SaveWarehouseAsync(), o => IsNotBusy && IsEditing);

        public AsyncRelayCommand CancelEditCommand =>
            _cancelEditCommand ??= new AsyncRelayCommand(o => CancelEditAsync(), o => IsNotBusy && IsEditing);

        public WarehouseProductsViewModel(IWarehouseService warehouseService, IProductService productService, INavigationService navigation)
        {
            _warehouseService = warehouseService;
            _productService = productService;
            _navigation = navigation;
        }

        public void Initialize(Guid warehouseId)
        {
            _warehouseId = warehouseId;
            _ = LoadWarehouseAsync(warehouseId);
        }

        private async Task LoadWarehouseAsync(Guid warehouseId)
        {
            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;
                Warehouse = await _warehouseService.GetWarehouseByIdAsync(warehouseId);
                _allProducts.Clear();
                _allProducts.AddRange(Warehouse.Products);
                ApplyProductFilterAndSort();
                IsEditing = false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private Task OpenProductAsync()
        {
            if (SelectedProduct != null && Warehouse != null)
            {
                _navigation.NavigateTo<ProductDetailViewModel>(vm => vm.Initialize(SelectedProduct.Id, Warehouse.Id));
            }

            return Task.CompletedTask;
        }

        private void ApplyProductFilterAndSort()
        {
            IEnumerable<ProductListDto> result = _allProducts;

            if (!string.IsNullOrWhiteSpace(ProductSearchText))
            {
                result = result.Where(product =>
                    product.Name.Contains(ProductSearchText, StringComparison.OrdinalIgnoreCase) ||
                    product.Category.ToString().Contains(ProductSearchText, StringComparison.OrdinalIgnoreCase));
            }

            result = ProductSortOption switch
            {
                "Category" => result.OrderBy(product => product.Category).ThenBy(product => product.Name),
                "Price" => result.OrderBy(product => product.Price).ThenBy(product => product.Name),
                "Quantity" => result.OrderBy(product => product.Quantity).ThenBy(product => product.Name),
                _ => result.OrderBy(product => product.Name)
            };

            Products = new ObservableCollection<ProductListDto>(result);
        }

        private async Task AddProductAsync()
        {
            if (string.IsNullOrWhiteSpace(NewProductName))
            {
                ErrorMessage = "Product name is required.";
                return;
            }

            if (!decimal.TryParse(NewProductPrice, out var price) || price < 0)
            {
                ErrorMessage = "Product price must be a valid non-negative number.";
                return;
            }

            if (!int.TryParse(NewProductQuantity, out var quantity) || quantity < 0)
            {
                ErrorMessage = "Product quantity must be a valid non-negative number.";
                return;
            }

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;
                await _productService.CreateProductAsync(
                    _warehouseId,
                    NewProductName.Trim(),
                    price,
                    quantity,
                    NewProductCategory ?? "Electronics",
                    NewProductDescription ?? string.Empty);

                NewProductName = string.Empty;
                NewProductPrice = string.Empty;
                NewProductQuantity = "1";
                NewProductDescription = string.Empty;
                await LoadWarehouseAsync(_warehouseId);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DeleteProductAsync()
        {
            if (SelectedProduct == null)
            {
                return;
            }

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;
                await _productService.DeleteProductAsync(SelectedProduct.Id);
                SelectedProduct = null;
                await LoadWarehouseAsync(_warehouseId);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private Task EditWarehouseAsync()
        {
            if (Warehouse != null)
            {
                EditName = Warehouse.Name;
                EditLocation = Warehouse.Location;
                IsEditing = true;
            }

            return Task.CompletedTask;
        }

        private async Task SaveWarehouseAsync()
        {
            if (string.IsNullOrWhiteSpace(EditName))
            {
                ErrorMessage = "Warehouse name is required.";
                return;
            }

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;
                await _warehouseService.UpdateWarehouseAsync(_warehouseId, EditName.Trim(), EditLocation ?? Warehouse.Location);
                await LoadWarehouseAsync(_warehouseId);
                IsEditing = false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private Task CancelEditAsync()
        {
            if (Warehouse != null)
            {
                EditName = Warehouse.Name;
                EditLocation = Warehouse.Location;
            }

            IsEditing = false;
            ErrorMessage = string.Empty;
            return Task.CompletedTask;
        }

        private Task GoBackAsync()
        {
            _navigation.NavigateTo<WarehousesViewModel>();
            return Task.CompletedTask;
        }
    }
}
