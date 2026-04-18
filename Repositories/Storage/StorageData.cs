using Laboratory_ProductManager.Repositories.Product;
using Laboratory_ProductManager.Repositories.Warehouse;

namespace Laboratory_ProductManager.Repositories.Storage
{
    public class StorageData
    {
        public List<WareHouseDBModel> Warehouses { get; set; } = new List<WareHouseDBModel>();
        public List<ProductDBModel> Products { get; set; } = new List<ProductDBModel>();
    }
}
