using Laboratory_ProductManager.Common.Enums;
using Laboratory_ProductManager.DBModels;

namespace Laboratory_ProductManager.UIModels.ProductUIModel
{
    // Seperate class for editing the entity via ui
    public class ProductEditing
    {
        private ProductDBModel _dbModel;

        public string Name
        {
            get => _dbModel.Name;
            set => _dbModel.Name = value;
        }
        public decimal Price
        {
            get => _dbModel.Price;
            set => _dbModel.Price = value;
        }
        public ProductCategory Category
        {
            get => _dbModel.Category;
            set => _dbModel.Category = value;
        }
        public string Description
        {
            get => _dbModel.Description;
            set => _dbModel.Description = value;
        }

        public ProductEditing(ProductDBModel dbModel)
        {
            _dbModel = dbModel;
        }

        // No neccesity to save changes, since we modify them in the DBMODEL
    }
}
