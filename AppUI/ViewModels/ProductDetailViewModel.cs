using Laboratory_ProductManager.Services.Interfaces;
using Laboratory_ProductManager.UIModels.ProductUIModel;
using Laboratory_ProductManager.Common.Enums;
using System;

namespace Laboratory_ProductManager.AppUI.ViewModels
{
    public class ProductDetailViewModel : BaseViewModel
    {
        private readonly IProductService _service;
        private readonly MainViewModel _mainViewModel;
        private RelayCommand _backCommand;

        private ProductView _product;
        public ProductView Product
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }

        public RelayCommand BackCommand =>
            _backCommand ??= new RelayCommand(o => GoBack());

        public ProductDetailViewModel(IProductService service, Guid productId, MainViewModel mainViewModel)
        {
            _service = service;
            _mainViewModel = mainViewModel;
            LoadProductDetail(productId);
        }

        private void LoadProductDetail(Guid productId)
        {
            var dbProduct = _service.GetProductById(productId);
            var category = Enum.Parse<ProductCategory>(dbProduct.Category ?? "Electronics");
            Product = new ProductView(
                dbProduct.Id,
                dbProduct.Id,
                dbProduct.Name,
                dbProduct.Price,
                dbProduct.Quantity,
                category,
                dbProduct.Description
            );
        }

        private void GoBack()
        {
            _mainViewModel.NavigateToWarehouses();
        }
    }
}