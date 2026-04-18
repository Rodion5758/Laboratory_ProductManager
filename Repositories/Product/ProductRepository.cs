using Laboratory_ProductManager.Common;
using Laboratory_ProductManager.Repositories.Storage;

namespace Laboratory_ProductManager.Repositories.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly JsonStorage _storage;

        public ProductRepository(JsonStorage storage)
        {
            _storage = storage;
        }

        public async Task<ProductDBModel> GetProductByIdAsync(Guid productId)
        {
            var data = await _storage.LoadAsync();
            var dbmodel = data.Products.FirstOrDefault(product => product.ID == productId);

            if (dbmodel == null) throw new ArgumentNullException(nameof(productId));

            return dbmodel;
        }

        public async Task<List<ProductDBModel>> GetProductsByWarehouseIdAsync(Guid warehouseId)
        {
            var data = await _storage.LoadAsync();
            return data.Products.Where(product => product.WareHouseID == warehouseId).ToList();
        }

        public async Task<ProductDBModel> CreateProductAsync(Guid warehouseId, string name, decimal price, int quantity, ProductCategory category, string description)
        {
            var data = await _storage.LoadAsync();
            var newProduct = new ProductDBModel(
                wareHouseID: warehouseId,
                name: name,
                price: price,
                quantity: quantity,
                category: category,
                description: description
            );
            data.Products.Add(newProduct);
            await _storage.SaveAsync(data);
            return newProduct;
        }

        public async Task UpdateProductAsync(Guid productId, string name, decimal price, int quantity, ProductCategory category, string description)
        {
            var data = await _storage.LoadAsync();
            var product = data.Products.FirstOrDefault(product => product.ID == productId);

            if (product == null) throw new ArgumentNullException(nameof(productId));
            
            product.Name = name;
            product.Price = price;
            product.Quantity = quantity;
            product.Category = category;
            product.Description = description;
            await _storage.SaveAsync(data);
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            var data = await _storage.LoadAsync();
            var toDelete = data.Products.FirstOrDefault(product => product.ID == productId);

            if (toDelete == null) throw new ArgumentNullException(nameof(productId));

            data.Products.Remove(toDelete);
            await _storage.SaveAsync(data);
        }

        public ProductDBModel GetProductById(Guid productId)
        {
            return GetProductByIdAsync(productId).GetAwaiter().GetResult();
        }

        public List<ProductDBModel> GetProductsByWarehouseId(Guid warehouseId)
        {
            return GetProductsByWarehouseIdAsync(warehouseId).GetAwaiter().GetResult();
        }

        public ProductDBModel CreateProduct(Guid warehouseId, string name, decimal price, int quantity, ProductCategory category, string description)
        {
            return CreateProductAsync(warehouseId, name, price, quantity, category, description).GetAwaiter().GetResult();
        }

        public void UpdateProduct(Guid productId, string name, decimal price, int quantity, ProductCategory category, string description)
        {
            UpdateProductAsync(productId, name, price, quantity, category, description).GetAwaiter().GetResult();
        }

        public void DeleteProduct(Guid productId)
        {
            DeleteProductAsync(productId).GetAwaiter().GetResult();
        }
    }
}
