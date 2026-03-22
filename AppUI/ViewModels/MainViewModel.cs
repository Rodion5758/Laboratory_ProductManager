using Laboratory_ProductManager.AppUI.ViewModels;
using Laboratory_ProductManager.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Laboratory_ProductManager.AppUI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IServiceProvider _serviceProvidr;
        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public MainViewModel(IServiceProvider provider)
        {
            _serviceProvidr = provider;
            NavigateToWarehouses();
        }

        public void NavigateToWarehouses()
        {
            CurrentViewModel = _serviceProvidr.GetRequiredService<WarehousesViewModel>();
        }

        public void NavigateToWarehouseDetail(Guid warehouseId)
        {
            var vm = _serviceProvidr.GetRequiredService<WarehouseProductsViewModel>();
            CurrentViewModel = vm;
        }

        public void NavigateToProductDetail(Guid produtctId)
        {
            var vm = _serviceProvidr.GetRequiredService<ProductDetailViewModel>();
            CurrentViewModel = vm;
        }
    }
}
