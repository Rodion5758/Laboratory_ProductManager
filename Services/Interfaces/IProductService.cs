using System;
using System.Collections.Generic;
using Laboratory_ProductManager.Services.DTO;

namespace Laboratory_ProductManager.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDetailDto> GetProductByIdAsync(Guid productId);
        Task<List<ProductListDto>> GetProductsByWarehouseIdAsync(Guid warehouseId);
        Task<ProductDetailDto> CreateProductAsync(Guid warehouseId, string name, decimal price, int quantity, string category, string description);
        Task UpdateProductAsync(Guid productId, string name, decimal price, int quantity, string category, string description);
        Task DeleteProductAsync(Guid productId);

        ProductDetailDto GetProductById(Guid productId);
        List<ProductListDto> GetProductsByWarehouseId(Guid warehouseId);
    }
}
