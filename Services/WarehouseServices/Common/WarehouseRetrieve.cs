
using Laboratory_ProductManager.DBModels;

namespace Laboratory_ProductManager.Services.WarehouseServices.Common
{
    public class WarehouseRetrieve
    {
        public static WareHouseDBModel? GetWarehouseDbModelById(Guid id)
        {
            foreach(var warehosue in FakeStorage.Warehouses)
            {
                if (warehosue.ID == id)
                {
                    return warehosue;
                }
            }

            return null;
        }
    }
}
