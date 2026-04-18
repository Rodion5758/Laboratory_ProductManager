using Laboratory_ProductManager.Repositories;
using Laboratory_ProductManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using Laboratory_ProductManager.Common;
using Laboratory_ProductManager.Services.DTO;

namespace Laboratory_ProductManager.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDetailDto> GetProductByIdAsync(Guid productId)
        {
            var dbProduct = await _repository.GetProductByIdAsync(productId);

            return new ProductDetailDto
            {
                Id = dbProduct.ID,
                Name = dbProduct.Name,
                Price = dbProduct.Price,
                Quantity = dbProduct.Quantity,
                Category = dbProduct.Category.ToString(),
                Description = dbProduct.Description,
                TotalCost = dbProduct.Price * dbProduct.Quantity
            };
        }

        public async Task<List<ProductListDto>> GetProductsByWarehouseIdAsync(Guid warehouseId)
        {
            var dbProducts = await _repository.GetProductsByWarehouseIdAsync(warehouseId);
            var result = new List<ProductListDto>();

            foreach (var product in dbProducts)
            {
                result.Add(new ProductListDto
                {
                    Id = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Category = product.Category
                });
            }

            return result;
        }

        public async Task<ProductDetailDto> CreateProductAsync(Guid warehouseId, string name, decimal price, int quantity, string category, string description)
        {
            ValidateProduct(name, price, quantity);
            var parsedCategory = ParseCategory(category);

            var dbProduct = await _repository.CreateProductAsync(warehouseId, name.Trim(), price, quantity, 
                parsedCategory, 
                description ?? string.Empty);

            return new ProductDetailDto
            {
                Id = dbProduct.ID,
                Name = dbProduct.Name,
                Price = dbProduct.Price,
                Quantity = dbProduct.Quantity,
                Category = dbProduct.Category.ToString(),
                Description = dbProduct.Description,
                TotalCost = dbProduct.Price * dbProduct.Quantity
            };
        }

        public async Task UpdateProductAsync(Guid productId, string name, decimal price, int quantity, string category, string description)
        {
            ValidateProduct(name, price, quantity);
            var parsedCategory = ParseCategory(category);

            await _repository.UpdateProductAsync(productId, name.Trim(), price, quantity,
                parsedCategory,
                description ?? string.Empty);
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            await _repository.DeleteProductAsync(productId);
        }

        public ProductDetailDto GetProductById(Guid productId)
        {
            return GetProductByIdAsync(productId).GetAwaiter().GetResult();
        }

        public List<ProductListDto> GetProductsByWarehouseId(Guid warehouseId)
        {
            return GetProductsByWarehouseIdAsync(warehouseId).GetAwaiter().GetResult();
        }

        private static void ValidateProduct(string name, decimal price, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Product name is required.");
            }

            if (price < 0)
            {
                throw new ArgumentException("Product price must be non-negative.");
            }

            if (quantity < 0)
            {
                throw new ArgumentException("Product quantity must be non-negative.");
            }
        }

        private static ProductCategory ParseCategory(string category)
        {
            if (!Enum.TryParse<ProductCategory>(category, true, out var parsedCategory))
            {
                throw new ArgumentException("Product category is invalid.");
            }

            return parsedCategory;
        }
    }
}
