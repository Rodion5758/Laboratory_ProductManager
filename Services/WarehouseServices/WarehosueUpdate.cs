
using Laboratory_ProductManager.DBModels;
using Laboratory_ProductManager.Services.WarehouseServices.Common;
using Laboratory_ProductManager.UIModels.WareHouseUIModel;

namespace Laboratory_ProductManager.Services.WarehouseServices
{
    public class WarehouseUpdate
    {

        public WarehouseEditing GetForEditing(Guid warehouseId)
        {
            WareHouseDBModel? dBModel = WarehouseRetrieve.GetWarehouseDbModelById(warehouseId);

            if (dBModel == null) throw new ArgumentException();

            return new WarehouseEditing(dBModel);
        }
    }
}