using Laboratory_ProductManager.Common.Enums;
using Laboratory_ProductManager.DBModels;
using Laboratory_ProductManager.UIModels.ProductUIModel;

namespace Laboratory_ProductManager.UIModels.WareHouseUIModel
{
    public class WarehouseView
    {
        private WareHouseDBModel _dbModel;
        
        public Guid ID => _dbModel.ID;
        public string Name => _dbModel.Name;
        public WareHouseLocation Location => _dbModel.Location;
        public List<ProductView> Products { get; } = new List<ProductView>();

        public WarehouseView(WareHouseDBModel dbModel)
        {
            _dbModel = dbModel;
        }

        public override string ToString()
        {
            return $"Warehouse: {Name} ; Location: {Location} ;";
        }
    }
}
