using Laboratory_ProductManager.UIModels.WareHouseUIModel;
using Laboratory_ProductManager.Common.Enums;
using Laboratory_ProductManager.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Laboratory_ProductManager.AppUI.ViewModels
{
    public class WarehousesViewModel : BaseViewModel
    {
        private readonly IWarehouseService _service;
        private readonly MainViewModel _mainViewModel;
        private RelayCommand _selectWarehouseCommand;

        private ObservableCollection<WarehouseView> _warehouses;
        public ObservableCollection<WarehouseView> Warehouses
        {
            get => _warehouses;
            set => SetProperty(ref _warehouses, value);
        }

        private WarehouseView _selectedWarehouse;
        public WarehouseView SelectedWarehouse
        {
            get => _selectedWarehouse;
            set
            {
                if (SetProperty(ref _selectedWarehouse, value) && value != null)
                {
                    _mainViewModel.NavigateToWarehouseDetail(value.ID);
                }
            }
        }

        public RelayCommand SelectWarehouseCommand =>
            _selectWarehouseCommand ??= new RelayCommand(o => SelectWarehouse());

        public WarehousesViewModel(IWarehouseService service, MainViewModel mainViewModel)
        {
            _service = service;
            _mainViewModel = mainViewModel;
            LoadWarehouses();
        }

        private void LoadWarehouses()
        {
            var dbWarehouses = _service.GetAllWarehouses();
            Warehouses = new ObservableCollection<WarehouseView>();

            foreach (var warehouse in dbWarehouses)
            {
                var location = Enum.Parse<WareHouseLocation>(warehouse.Location);
                Warehouses.Add(new WarehouseView(warehouse.Id, warehouse.Name, location));
            }
        }

        private void SelectWarehouse()
        {
            if (SelectedWarehouse != null)
            {
                _mainViewModel.NavigateToWarehouseDetail(SelectedWarehouse.ID);
            }
        }
    }
}
