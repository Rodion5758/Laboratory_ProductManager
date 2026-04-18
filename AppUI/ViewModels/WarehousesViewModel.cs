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
    public class WarehousesViewModel : BaseViewModel
    {
        private readonly IWarehouseService _service;
        private readonly INavigationService _navigation;
        private readonly List<WarehouseListDto> _allWarehouses = new List<WarehouseListDto>();
        private AsyncRelayCommand _openWarehouseCommand;
        private AsyncRelayCommand _addWarehouseCommand;
        private AsyncRelayCommand _deleteWarehouseCommand;
        private string _searchText;
        private string _sortOption = "Name";
        private string _newWarehouseName;
        private string _newWarehouseLocation = "Kyiv";

        private ObservableCollection<WarehouseListDto> _warehouses;
        public ObservableCollection<WarehouseListDto> Warehouses
        {
            get => _warehouses;
            set => SetProperty(ref _warehouses, value);
        }

        private WarehouseListDto _selectedWarehouse;
        public WarehouseListDto SelectedWarehouse
        {
            get => _selectedWarehouse;
            set
            {
                SetProperty(ref _selectedWarehouse, value);
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    ApplyFilterAndSort();
                }
            }
        }

        public string SortOption
        {
            get => _sortOption;
            set
            {
                if (SetProperty(ref _sortOption, value))
                {
                    ApplyFilterAndSort();
                }
            }
        }

        public string NewWarehouseName
        {
            get => _newWarehouseName;
            set => SetProperty(ref _newWarehouseName, value);
        }

        public string NewWarehouseLocation
        {
            get => _newWarehouseLocation;
            set => SetProperty(ref _newWarehouseLocation, value);
        }

        public ObservableCollection<string> LocationOptions { get; } =
            new ObservableCollection<string> { "Kyiv", "Lviv", "Mankivka", "Uman", "Yahotyn", "Byku", "Talne" };

        public ObservableCollection<string> SortOptions { get; } =
            new ObservableCollection<string> { "Name", "Location" };

        public AsyncRelayCommand OpenWarehouseCommand =>
            _openWarehouseCommand ??= new AsyncRelayCommand(o => OpenWarehouseAsync(), o => IsNotBusy && SelectedWarehouse != null);

        public AsyncRelayCommand AddWarehouseCommand =>
            _addWarehouseCommand ??= new AsyncRelayCommand(o => AddWarehouseAsync(), o => IsNotBusy);

        public AsyncRelayCommand DeleteWarehouseCommand =>
            _deleteWarehouseCommand ??= new AsyncRelayCommand(o => DeleteWarehouseAsync(), o => IsNotBusy && SelectedWarehouse != null);

        public WarehousesViewModel(IWarehouseService service, INavigationService navigationService)
        {
            _navigation = navigationService;
            _service = service;
            _ = LoadWarehousesAsync();
        }

        private async Task LoadWarehousesAsync()
        {
            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;
                var warehouses = await _service.GetAllWarehousesAsync();
                _allWarehouses.Clear();
                _allWarehouses.AddRange(warehouses);
                ApplyFilterAndSort();
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

        private void ApplyFilterAndSort()
        {
            IEnumerable<WarehouseListDto> result = _allWarehouses;

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                result = result.Where(warehouse =>
                    warehouse.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    warehouse.Location.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            }

            result = SortOption == "Location"
                ? result.OrderBy(warehouse => warehouse.Location).ThenBy(warehouse => warehouse.Name)
                : result.OrderBy(warehouse => warehouse.Name);

            Warehouses = new ObservableCollection<WarehouseListDto>(result);
        }

        private Task OpenWarehouseAsync()
        {
            if (SelectedWarehouse != null)
            {
                _navigation.NavigateTo<WarehouseProductsViewModel>(vm => vm.Initialize(SelectedWarehouse.Id));
            }

            return Task.CompletedTask;
        }

        private async Task AddWarehouseAsync()
        {
            if (string.IsNullOrWhiteSpace(NewWarehouseName))
            {
                ErrorMessage = "Warehouse name is required.";
                return;
            }

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;
                await _service.CreateWarehouseAsync(NewWarehouseName.Trim(), NewWarehouseLocation ?? "Kyiv");
                NewWarehouseName = string.Empty;
                await LoadWarehousesAsync();
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

        private async Task DeleteWarehouseAsync()
        {
            if (SelectedWarehouse == null)
            {
                return;
            }

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;
                await _service.DeleteWarehouseAsync(SelectedWarehouse.Id);
                SelectedWarehouse = null;
                await LoadWarehousesAsync();
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
    }
}
