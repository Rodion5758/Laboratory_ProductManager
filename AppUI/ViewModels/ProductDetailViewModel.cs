using Laboratory_ProductManager.Services.Interfaces;
using Laboratory_ProductManager.UIModels.ProductUIModel;
using Laboratory_ProductManager.Common.Enums;
using System;

namespace Laboratory_ProductManager.AppUI.ViewModels
{
    public class ProductDetailViewModel : BaseViewModel
    {
        private readonly IProductService _service;
        private readonly INavigationService _navigation;
        private RelayCommand _backCommand;
        private Guid _warehouseId;
        private ProductView _product;
        public ProductView Product
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }

        public RelayCommand BackCommand =>
            _backCommand ??= new RelayCommand(o => GoBack());

        public ProductDetailViewModel(IProductService service, INavigationService navigation)
        {
            _service = service;
            _navigation = navigation;
        }

        public void Initialize(Guid productId, Guid warehouseId)
        {
            _warehouseId = warehouseId;
            LoadProductDetail(productId);
        }

        private void LoadProductDetail(Guid productId)
        {
            var dbProduct = _service.GetProductById(productId);
            var category = Enum.Parse<ProductCategory>(dbProduct.Category ?? "Electronics");

            Product = new ProductView(
                dbProduct.Id,
                Guid.Empty,
                dbProduct.Name,
                dbProduct.Price,
                dbProduct.Quantity,
                category,
                dbProduct.Description
            );
        }

        private void GoBack()
        {
            _navigation.NavigateTo<WarehouseProductsViewModel>(vm => vm.Initialize(_warehouseId));
        }
    }
}