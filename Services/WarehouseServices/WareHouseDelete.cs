
using Laboratory_ProductManager.DBModels;
using Laboratory_ProductManager.Services.WarehouseServices.Common;

namespace Laboratory_ProductManager.Services.WarehouseServices
{
    public class WarehouseDelete
    {

        public void Delete(Guid warehouseId)
        {
            WareHouseDBModel? dBModel = WarehouseRetrieve.GetWarehouseDbModelById(warehouseId);

            if (dBModel == null) throw new ArgumentException();

            FakeStorage.Warehouses.Remove(dBModel);

            // Also removes all the products in the warehouse. Like cascade in SQL
            foreach(var product in FakeStorage.Products)
            {
                if (product.WareHouseID == warehouseId)
                {
                    FakeStorage.Products.Remove(product);
                }
            }
        }
    }
}