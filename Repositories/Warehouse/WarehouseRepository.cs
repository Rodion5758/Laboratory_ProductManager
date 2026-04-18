using Laboratory_ProductManager.Common;
using Laboratory_ProductManager.Repositories.Product;
using Laboratory_ProductManager.Repositories.Storage;

namespace Laboratory_ProductManager.Repositories.Warehouse
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly JsonStorage _storage;

        public WarehouseRepository(JsonStorage storage)
        {
            _storage = storage;
        }

        public async Task<List<WareHouseDBModel>> GetAllWarehousesAsync()
        {
            var data = await _storage.LoadAsync();
            return data.Warehouses.ToList();
        }

        public async Task<WareHouseDBModel> GetWarehouseByIdAsync(Guid warehouseId)
        {
            var data = await _storage.LoadAsync();
            var dbmodel = data.Warehouses.FirstOrDefault(warehouse => warehouse.ID == warehouseId);

            if (dbmodel == null) throw new ArgumentNullException(nameof(warehouseId));

            return dbmodel;
        }

        public async Task<List<ProductDBModel>> GetProductsByWarehouseIdAsync(Guid warehouseId)
        {
            var data = await _storage.LoadAsync();
            return data.Products.Where(product => product.WareHouseID == warehouseId).ToList();
        }

        public async Task<WareHouseDBModel> CreateWarehouseAsync(string name, WareHouseLocation location)
        {
            var data = await _storage.LoadAsync();
            var newWarehouse = new WareHouseDBModel(name: name, location: location);
            data.Warehouses.Add(newWarehouse);
            await _storage.SaveAsync(data);
            return newWarehouse;
        }

        public async Task UpdateWarehouseAsync(Guid warehouseId, string name, WareHouseLocation location)
        {
            var data = await _storage.LoadAsync();
            var warehouse = data.Warehouses.FirstOrDefault(warehouse => warehouse.ID == warehouseId);

            if (warehouse == null) throw new ArgumentNullException(nameof(warehouseId));
            
            warehouse.Name = name;
            warehouse.Location = location;
            await _storage.SaveAsync(data);
        }

        public async Task DeleteWarehouseAsync(Guid warehouseId)
        {
            var data = await _storage.LoadAsync();
            var dbModel = data.Warehouses.FirstOrDefault(warehouse => warehouse.ID == warehouseId);

            if (dbModel == null) throw new ArgumentException(nameof(warehouseId));

            data.Warehouses.Remove(dbModel);
            data.Products.RemoveAll(product => product.WareHouseID == warehouseId);
            await _storage.SaveAsync(data);
        }

        public List<WareHouseDBModel> GetAllWarehouses()
        {
            return GetAllWarehousesAsync().GetAwaiter().GetResult();
        }

        public WareHouseDBModel GetWarehouseById(Guid warehouseId)
        {
            return GetWarehouseByIdAsync(warehouseId).GetAwaiter().GetResult();
        }

        public List<ProductDBModel> GetProductsByWarehouseId(Guid warehouseId)
        {
            return GetProductsByWarehouseIdAsync(warehouseId).GetAwaiter().GetResult();
        }

        public WareHouseDBModel CreateWarehouse(string name, WareHouseLocation location)
        {
            return CreateWarehouseAsync(name, location).GetAwaiter().GetResult();
        }

        public void UpdateWarehouse(Guid warehouseId, string name, WareHouseLocation location)
        {
            UpdateWarehouseAsync(warehouseId, name, location).GetAwaiter().GetResult();
        }

        public void DeleteWarehouse(Guid warehouseId)
        {
            DeleteWarehouseAsync(warehouseId).GetAwaiter().GetResult();
        }
    }
}
