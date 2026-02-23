using Laboratory_ProductManager.DBModels;
using Laboratory_ProductManager.UIModels.WareHouseUIModel;

namespace Laboratory_ProductManager.Services.WarehouseServices
{
    public class WarehouseCreate
    {

        public void Create(WarehouseCreation creationModel)
        {
            FakeStorage.Warehouses.Add(creationModel.SaveChanges());
        }
    }
}