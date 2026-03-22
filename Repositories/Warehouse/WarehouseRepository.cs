using Laboratory_ProductManager.Common.Enums;
using Repositories.Product;
using Repositories.Warehouse;
using System;
using System.Collections.Generic;

namespace Laboratory_ProductManager.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        // Read operations
        public List<WareHouseDBModel> GetAllWarehouses()
        {
            List<WareHouseDBModel> finalList = new List<WareHouseDBModel>();

            foreach (var warehouse in FakeStorage.Warehouses)
            {
                finalList.Add(warehouse);
            }

            return finalList;
        }

        public WareHouseDBModel GetWarehouseById(Guid warehouseId)
        {
            WareHouseDBModel? dbmodel = null;
            foreach (var warehouse in FakeStorage.Warehouses)
            {
                if (warehouse.ID == warehouseId)
                {
                    dbmodel = warehouse;
                    break;
                }
            }

            if (dbmodel == null) throw new ArgumentNullException(nameof(warehouseId));

            return dbmodel;
        }

        public List<ProductDBModel> GetProductsByWarehouseId(Guid warehouseId)
        {
            List<ProductDBModel> products = new List<ProductDBModel>();

            foreach (var product in FakeStorage.Products)
            {
                if (product.WareHouseID == warehouseId)
                {
                    products.Add(product);
                }
            }

            return products;
        }

        // Create operations
        public WareHouseDBModel CreateWarehouse(string name, WareHouseLocation location)
        {
            var newWarehouse = new WareHouseDBModel(name: name, location: location);
            FakeStorage.Warehouses.Add(newWarehouse);
            return newWarehouse;
        }

        // Update operations
        public void UpdateWarehouse(Guid warehouseId, string name, WareHouseLocation location)
        {
            var warehouse = GetWarehouseById(warehouseId);
            
            warehouse.Name = name;
            warehouse.Location = location;
        }

        // Delete operations
        public void DeleteWarehouse(Guid warehouseId)
        {
            WareHouseDBModel? dBModel = null;
            foreach (var warehouse in FakeStorage.Warehouses)
            {
                if (warehouse.ID == warehouseId)
                {
                    dBModel = warehouse;
                    break;
                }
            }

            if (dBModel == null) throw new ArgumentException();

            FakeStorage.Warehouses.Remove(dBModel);

            // Also removes all the products in the warehouse. Like cascade in SQL
            foreach (var product in FakeStorage.Products)
            {
                if (product.WareHouseID == warehouseId)
                {
                    FakeStorage.Products.Remove(product);
                }
            }
        }
    }
}
