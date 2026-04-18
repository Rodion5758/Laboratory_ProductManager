using System;
using System.Collections.Generic;
using Laboratory_ProductManager.Common;
using Laboratory_ProductManager.Repositories.Product;

namespace Laboratory_ProductManager.Repositories
{
    public interface IProductRepository
    {
        Task<ProductDBModel> GetProductByIdAsync(Guid productId);
        Task<List<ProductDBModel>> GetProductsByWarehouseIdAsync(Guid warehouseId);
        Task<ProductDBModel> CreateProductAsync(Guid warehouseId, string name, decimal price, int quantity, ProductCategory category, string description);
        Task UpdateProductAsync(Guid productId, string name, decimal price, int quantity, ProductCategory category, string description);
        Task DeleteProductAsync(Guid productId);

        ProductDBModel GetProductById(Guid productId);
        List<ProductDBModel> GetProductsByWarehouseId(Guid warehouseId);
        ProductDBModel CreateProduct(Guid warehouseId, string name, decimal price, int quantity, ProductCategory category, string description);
        void UpdateProduct(Guid productId, string name, decimal price, int quantity, ProductCategory category, string description);
        void DeleteProduct(Guid productId);
    }
}
