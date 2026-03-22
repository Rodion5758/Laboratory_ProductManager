using Laboratory_ProductManager.Repositories;
using Laboratory_ProductManager.Services.DTOs;
using Laboratory_ProductManager.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Laboratory_ProductManager.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public ProductDetailDto GetProductById(Guid productId)
        {
            var dbProduct = _repository.GetProductById(productId);

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
        public List<ProductListDto> GetProductsByWarehouseId(Guid warehouseId)
        {
            var dbProducts = _repository.GetProductsByWarehouseId(warehouseId);
            var result = new List<ProductListDto>();

            foreach (var product in dbProducts)
            {
                result.Add(new ProductListDto
                {
                    Id = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity
                });
            }

            return result;
        }

        public ProductDetailDto CreateProduct(Guid warehouseId, string name, decimal price, int quantity, string category, string description)
        {
            var dbProduct = _repository.CreateProduct(warehouseId, name, price, quantity, 
                Enum.Parse<Laboratory_ProductManager.Common.Enums.ProductCategory>(category), 
                description);

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

        public void UpdateProduct(Guid productId, string name, decimal price, int quantity, string category, string description)
        {
            _repository.UpdateProduct(productId, name, price, quantity,
                Enum.Parse<Laboratory_ProductManager.Common.Enums.ProductCategory>(category),
                description);
        }

        public void DeleteProduct(Guid productId)
        {
            _repository.DeleteProduct(productId);
        }
    }
}
