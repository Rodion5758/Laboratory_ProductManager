using Laboratory_ProductManager.Repositories;
using Laboratory_ProductManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using Laboratory_ProductManager.Common;
using Laboratory_ProductManager.Services.DTO;

namespace Laboratory_ProductManager.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _repository;

        public WarehouseService(IWarehouseRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<WarehouseListDto>> GetAllWarehousesAsync()
        {
            var dbWarehouses = await _repository.GetAllWarehousesAsync();
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

        public async Task<WarehouseDetailDto> GetWarehouseByIdAsync(Guid warehouseId)
        {
            var dbWarehouse = await _repository.GetWarehouseByIdAsync(warehouseId);
            var dbProducts = await _repository.GetProductsByWarehouseIdAsync(warehouseId);

            var productDtos = new List<ProductListDto>();
            decimal totalValue = 0;

            foreach (var product in dbProducts)
            {
                var productDto = new ProductListDto
                {
                    Id = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Category = product.Category
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

        public async Task<WarehouseDetailDto> CreateWarehouseAsync(string name, string location)
        {
            ValidateName(name);
            var parsedLocation = ParseLocation(location);

            var warehouse = await _repository.CreateWarehouseAsync(name.Trim(), parsedLocation);
            return await GetWarehouseByIdAsync(warehouse.ID);
        }

        public async Task UpdateWarehouseAsync(Guid warehouseId, string name, string location)
        {
            ValidateName(name);
            var parsedLocation = ParseLocation(location);

            await _repository.UpdateWarehouseAsync(warehouseId, name.Trim(), parsedLocation);
        }

        public async Task DeleteWarehouseAsync(Guid warehouseId)
        {
            await _repository.DeleteWarehouseAsync(warehouseId);
        }

        public List<WarehouseListDto> GetAllWarehouses()
        {
            return GetAllWarehousesAsync().GetAwaiter().GetResult();
        }

        public WarehouseDetailDto GetWarehouseById(Guid warehouseId)
        {
            return GetWarehouseByIdAsync(warehouseId).GetAwaiter().GetResult();
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Warehouse name is required.");
            }
        }

        private static WareHouseLocation ParseLocation(string location)
        {
            if (!Enum.TryParse<WareHouseLocation>(location, true, out var parsedLocation))
            {
                throw new ArgumentException("Warehouse location is invalid.");
            }

            return parsedLocation;
        }
    }
}
