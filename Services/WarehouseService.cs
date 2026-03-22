using Laboratory_ProductManager.Repositories;
using Laboratory_ProductManager.Services.DTOs;
using Laboratory_ProductManager.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Laboratory_ProductManager.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _repository;

        public WarehouseService(IWarehouseRepository repository)
        {
            _repository = repository;
        }

        public List<WarehouseListDto> GetAllWarehouses()
        {
            var dbWarehouses = _repository.GetAllWarehouses();
            var result = new List<WarehouseListDto>();

            foreach (var warehouse in dbWarehouses)
            {
                result.Add(new WarehouseListDto
                {
                    Id = warehouse.ID,
                    Name = warehouse.Name,
                    Location = warehouse.Location.ToString()
                });
            }

            return result;
        }

        public WarehouseDetailDto GetWarehouseById(Guid warehouseId)
        {
            var dbWarehouse = _repository.GetWarehouseById(warehouseId);
            var dbProducts = _repository.GetProductsByWarehouseId(warehouseId);

            var productDtos = new List<ProductListDto>();
            decimal totalValue = 0;

            foreach (var product in dbProducts)
            {
                var productDto = new ProductListDto
                {
                    Id = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity
                };
                productDtos.Add(productDto);
                totalValue += product.Price * product.Quantity;
            }

            return new WarehouseDetailDto
            {
                Id = dbWarehouse.ID,
                Name = dbWarehouse.Name,
                Location = dbWarehouse.Location.ToString(),
                TotalProducts = dbProducts.Count,
                TotalValue = totalValue,
                Products = productDtos
            };
        }
    }
}