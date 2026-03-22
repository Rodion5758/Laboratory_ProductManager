using Laboratory_ProductManager.Services.DTOs;
using System;
using System.Collections.Generic;

namespace Laboratory_ProductManager.Services.Interfaces
{
    public interface IProductService
    {
        ProductDetailDto GetProductById(Guid productId);
        List<ProductListDto> GetProductsByWarehouseId(Guid warehouseId);
    }
}