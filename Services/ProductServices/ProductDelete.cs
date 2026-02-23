
using Laboratory_ProductManager.DBModels;
using Laboratory_ProductManager.Services.ProductServices.Common;

namespace Laboratory_ProductManager.Services.ProductServices
{
    public class ProductDelete
    {
        public void Delete(Guid productId)
        {
            ProductDBModel? toDelete = ProductRetrieve.FindProductDBModelById(productId);

            if (toDelete == null) throw new ArgumentNullException();
            
            FakeStorage.Products.Remove(toDelete);
        }
    }
}