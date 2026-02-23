
using Laboratory_ProductManager.Common.Enums;
using Laboratory_ProductManager.DBModels;

namespace Laboratory_ProductManager.UIModels.ProductUIModel
{
    // Seperate class for viewing the entity via ui
    public class ProductView
    {
        private ProductDBModel _dbModel;

        public Guid ID => _dbModel.ID;
        public Guid WareHouseId => _dbModel.WareHouseID;
        public string Name => _dbModel.Name;
        public decimal Price => _dbModel.Price;
        public ProductCategory Category => _dbModel.Category;
        public string Description => _dbModel.Description;

        public ProductView(ProductDBModel dbModel)
        {
            _dbModel = dbModel;
        }

        public override string ToString()
        {
            return $"Product: {Name} ; Category: {Category} ; Price: {Price:F2} hrn";
        }
    }
}
