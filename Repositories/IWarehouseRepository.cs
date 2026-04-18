using System;
using System.Collections.Generic;
using Laboratory_ProductManager.Common;
using Laboratory_ProductManager.Repositories.Product;
using Laboratory_ProductManager.Repositories.Warehouse;

namespace Laboratory_ProductManager.Repositories
{
    public interface IWarehouseRepository
    {
        Task<List<WareHouseDBModel>> GetAllWarehousesAsync();
        Task<WareHouseDBModel> GetWarehouseByIdAsync(Guid warehouseId);
        Task<List<ProductDBModel>> GetProductsByWarehouseIdAsync(Guid warehouseId);
        Task<WareHouseDBModel> CreateWarehouseAsync(string name, WareHouseLocation location);
        Task UpdateWarehouseAsync(Guid warehouseId, string name, WareHouseLocation location);
        Task DeleteWarehouseAsync(Guid warehouseId);

        List<WareHouseDBModel> GetAllWarehouses();
        WareHouseDBModel GetWarehouseById(Guid warehouseId);
        List<ProductDBModel> GetProductsByWarehouseId(Guid warehouseId);
        WareHouseDBModel CreateWarehouse(string name, WareHouseLocation location);
        void UpdateWarehouse(Guid warehouseId, string name, WareHouseLocation location);
        void DeleteWarehouse(Guid warehouseId);
    }
}
