using Laboratory_ProductManager.UIModels.ProductUIModel;

namespace Laboratory_ProductManager.Services.ProductServices
{

    public class ProductCreate
    {       

        public void Create(ProductCreation creationModel)
        {
            FakeStorage.Products.Add(creationModel.SaveChanges());
        }
    }
}