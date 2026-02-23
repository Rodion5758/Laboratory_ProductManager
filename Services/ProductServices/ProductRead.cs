
using Laboratory_ProductManager.DBModels;
using Laboratory_ProductManager.Services.ProductServices.Common;
using Laboratory_ProductManager.UIModels.ProductUIModel;

namespace Laboratory_ProductManager.Services.ProductServices
{
    public class ProductRead
    {

        public ProductView GetById(Guid productId)
        {
            ProductDBModel? dbmodel = ProductRetrieve.FindProductDBModelById(productId);

            if (dbmodel == null) throw new ArgumentNullException();

            return new ProductView(dbmodel);
        }

        public List<ProductView> GetByWarehouseId(Guid warehouseId)
        {
            List<ProductView> finalList = new List<ProductView>();

            foreach(var product in FakeStorage.Products)
            {
                if (product.WareHouseID == warehouseId)
                {
                    finalList.Add(new ProductView(product));
                }
            }

            return finalList;
        }
    }
}