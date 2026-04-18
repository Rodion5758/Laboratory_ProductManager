using Laboratory_ProductManager.Services.Interfaces;
using Laboratory_ProductManager.Services.DTO;
using System;
using Laboratory_ProductManager.AppUI.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Laboratory_ProductManager.AppUI.ViewModels
{
    public class ProductDetailViewModel : BaseViewModel
    {
        private readonly IProductService _service;
        private readonly INavigationService _navigation;
        private AsyncRelayCommand _backCommand;
        private AsyncRelayCommand _editProductCommand;
        private AsyncRelayCommand _saveProductCommand;
        private AsyncRelayCommand _cancelEditCommand;
        private Guid _warehouseId;
        private Guid _productId;
        private bool _isEditing;
        private string _editName;
        private string _editPrice;
        private string _editQuantity;
        private string _editCategory;
        private string _editDescription;
        private ProductDetailDto _product;
        public ProductDetailDto Product
        {
            get => _product;
            set
            {
                if (SetProperty(ref _product, value) && value != null)
                {
                    SetEditFields(value);
                }
            }
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

        public string EditPrice
        {
            get => _editPrice;
            set => SetProperty(ref _editPrice, value);
        }

        public string EditQuantity
        {
            get => _editQuantity;
            set => SetProperty(ref _editQuantity, value);
        }

        public string EditCategory
        {
            get => _editCategory;
            set => SetProperty(ref _editCategory, value);
        }

        public string EditDescription
        {
            get => _editDescription;
            set => SetProperty(ref _editDescription, value);
        }

        public ObservableCollection<string> CategoryOptions { get; } =
            new ObservableCollection<string> { "Food", "Electronics", "Clothing", "Medicine", "Souvenirs" };

        public AsyncRelayCommand BackCommand =>
            _backCommand ??= new AsyncRelayCommand(o => GoBackAsync(), o => IsNotBusy);

        public AsyncRelayCommand EditProductCommand =>
            _editProductCommand ??= new AsyncRelayCommand(o => EditProductAsync(), o => IsNotBusy && IsNotEditing && Product != null);

        public AsyncRelayCommand SaveProductCommand =>
            _saveProductCommand ??= new AsyncRelayCommand(o => SaveProductAsync(), o => IsNotBusy && IsEditing);

        public AsyncRelayCommand CancelEditCommand =>
            _cancelEditCommand ??= new AsyncRelayCommand(o => CancelEditAsync(), o => IsNotBusy && IsEditing);

        public ProductDetailViewModel(IProductService service, INavigationService navigation)
        {
            _service = service;
            _navigation = navigation;
        }

        public void Initialize(Guid productId, Guid warehouseId)
        {
            _productId = productId;
            _warehouseId = warehouseId;
            _ = LoadProductDetailAsync(productId);
        }

        private async Task LoadProductDetailAsync(Guid productId)
        {
            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;
                Product = await _service.GetProductByIdAsync(productId);
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

        private void SetEditFields(ProductDetailDto product)
        {
            EditName = product.Name;
            EditPrice = product.Price.ToString();
            EditQuantity = product.Quantity.ToString();
            EditCategory = product.Category;
            EditDescription = product.Description;
        }

        private Task EditProductAsync()
        {
            if (Product != null)
            {
                SetEditFields(Product);
                IsEditing = true;
            }

            return Task.CompletedTask;
        }

        private async Task SaveProductAsync()
        {
            if (string.IsNullOrWhiteSpace(EditName))
            {
                ErrorMessage = "Product name is required.";
                return;
            }

            if (!decimal.TryParse(EditPrice, out var price) || price < 0)
            {
                ErrorMessage = "Product price must be a valid non-negative number.";
                return;
            }

            if (!int.TryParse(EditQuantity, out var quantity) || quantity < 0)
            {
                ErrorMessage = "Product quantity must be a valid non-negative number.";
                return;
            }

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;
                await _service.UpdateProductAsync(
                    _productId,
                    EditName.Trim(),
                    price,
                    quantity,
                    EditCategory ?? "Electronics",
                    EditDescription ?? string.Empty);
                await LoadProductDetailAsync(_productId);
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
            if (Product != null)
            {
                SetEditFields(Product);
            }

            IsEditing = false;
            ErrorMessage = string.Empty;
            return Task.CompletedTask;
        }

        private Task GoBackAsync()
        {
            _navigation.NavigateTo<WarehouseProductsViewModel>(vm => vm.Initialize(_warehouseId));
            return Task.CompletedTask;
        }
    }
}
