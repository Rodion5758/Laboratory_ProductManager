using Laboratory_ProductManager.Common.Enums;
using Repositories.Product;
using System;
using System.Collections.Generic;

namespace Laboratory_ProductManager.Repositories
{
    public class ProductRepository : IProductRepository
    {
        // Read operations
        public ProductDBModel GetProductById(Guid productId)
        {
            ProductDBModel? dbmodel = null;
            foreach (var product in FakeStorage.Products)
            {
                if (product.ID == productId)
                {
                    dbmodel = product;
                    break;
                }
            }

            if (dbmodel == null) throw new ArgumentNullException(nameof(productId));

            return dbmodel;
        }

        public List<ProductDBModel> GetProductsByWarehouseId(Guid warehouseId)
        {
            List<ProductDBModel> finalList = new List<ProductDBModel>();

            foreach (var product in FakeStorage.Products)
            {
                if (product.WareHouseID == warehouseId)
                {
                    finalList.Add(product);
                }
            }

            return finalList;
        }

        // Create operations
        public ProductDBModel CreateProduct(Guid warehouseId, string name, decimal price, int quantity, ProductCategory category, string description)
        {
            var newProduct = new ProductDBModel(
                wareHouseID: warehouseId,
                name: name,
                price: price,
                quantity: quantity,
                category: category,
                description: description
            );
            FakeStorage.Products.Add(newProduct);
            return newProduct;
        }

        // Update operations
        public void UpdateProduct(Guid productId, string name, decimal price, int quantity, ProductCategory category, string description)
        {
            var product = GetProductById(productId);
            
            product.Name = name;
            product.Price = price;
            product.Quantity = quantity;
            product.Category = category;
            product.Description = description;
        }

        // Delete operations
        public void DeleteProduct(Guid productId)
        {
            ProductDBModel? toDelete = null;
            foreach (var product in FakeStorage.Products)
            {
                if (product.ID == productId)
                {
                    toDelete = product;
                    break;
                }
            }

            if (toDelete == null) throw new ArgumentNullException(nameof(productId));

            FakeStorage.Products.Remove(toDelete);
        }
    }
}
