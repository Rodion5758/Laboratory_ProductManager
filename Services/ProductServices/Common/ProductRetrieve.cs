using Laboratory_ProductManager.DBModels;

namespace Laboratory_ProductManager.Services.ProductServices.Common
{
    public class ProductRetrieve
    {
        public static ProductDBModel? FindProductDBModelById(Guid id)
        {
            ProductDBModel? finalProduct = null;
            foreach (var product in FakeStorage.Products)
            {
                if (product.ID == id)
                {
                    finalProduct = product; break;
                }
            }

            return finalProduct;
        }
    }
}
