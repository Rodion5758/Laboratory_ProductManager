using Laboratory_ProductManager.Common.Enums;
using Laboratory_ProductManager.DBModels;
using Laboratory_ProductManager.UIModels.ProductUIModel;

namespace Laboratory_ProductManager.UIModels.WareHouseUIModel
{
    public class WarehouseEditing
    {
        private WareHouseDBModel _dbModel;
        
        public string Name
        { 
            get => _dbModel.Name;
            set => _dbModel.Name = value;
        }
        
        public WareHouseLocation Location
        { 
            get => _dbModel.Location;
            set => _dbModel.Location = value;
        }

        public List<ProductView> Products { get; private set; } = new List<ProductView>();

        public WarehouseEditing(WareHouseDBModel dbModel) 
        {
            _dbModel = dbModel;
        }

       // No need to save changes, the changes are done directly to the db model
    }
}






