using Laboratory_ProductManager.AppUI.ViewModels;
using Laboratory_ProductManager.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Laboratory_ProductManager.AppUI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            _navigationService.CurrentViewChanged += () => CurrentViewModel = (BaseViewModel)_navigationService.CurrentView;

            _navigationService.NavigateTo<WarehousesViewModel>();
        }
    }
}
