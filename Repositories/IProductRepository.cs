using Laboratory_ProductManager.Common.Enums;
using Repositories.Product;
using System;
using System.Collections.Generic;

namespace Laboratory_ProductManager.Repositories
{
    public interface IProductRepository
    {
        // Read operations
        ProductDBModel GetProductById(Guid productId);
        List<ProductDBModel> GetProductsByWarehouseId(Guid warehouseId);

        // Create operations
        ProductDBModel CreateProduct(Guid warehouseId, string name, decimal price, int quantity, ProductCategory category, string description);

        // Update operations
        void UpdateProduct(Guid productId, string name, decimal price, int quantity, ProductCategory category, string description);

        // Delete operations
        void DeleteProduct(Guid productId);
    }
}