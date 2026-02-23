
using Laboratory_ProductManager.DBModels;
using Laboratory_ProductManager.Services.ProductServices.Common;
using Laboratory_ProductManager.UIModels.ProductUIModel;

namespace Laboratory_ProductManager.Services.ProductServices
{
    public class ProductUpdate
    {
        public ProductEditing GetForEditing(Guid productId)
        {
            ProductDBModel? dbmodel = ProductRetrieve.FindProductDBModelById(productId);

            if (dbmodel == null) throw new ArgumentNullException();

            return new ProductEditing(dbmodel);
        }

    }
}