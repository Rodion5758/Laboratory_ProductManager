using Laboratory_ProductManager.Common.Enums;
using Repositories.Product;
using Repositories.Warehouse;
using System;
using System.Collections.Generic;

namespace Laboratory_ProductManager.Repositories
{
    public interface IWarehouseRepository
    {
        // Read operations
        List<WareHouseDBModel> GetAllWarehouses();
        WareHouseDBModel GetWarehouseById(Guid warehouseId);
        List<ProductDBModel> GetProductsByWarehouseId(Guid warehouseId);

        // Create operations
        WareHouseDBModel CreateWarehouse(string name, WareHouseLocation location);

        // Update operations
        void UpdateWarehouse(Guid warehouseId, string name, WareHouseLocation location);

        // Delete operations
        void DeleteWarehouse(Guid warehouseId);
    }
}