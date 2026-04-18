using System;
using System.Collections.Generic;
using Laboratory_ProductManager.Services.DTO;

namespace Laboratory_ProductManager.Services.Interfaces
{
    public interface IWarehouseService
    {
        Task<List<WarehouseListDto>> GetAllWarehousesAsync();
        Task<WarehouseDetailDto> GetWarehouseByIdAsync(Guid warehouseId);
        Task<WarehouseDetailDto> CreateWarehouseAsync(string name, string location);
        Task UpdateWarehouseAsync(Guid warehouseId, string name, string location);
        Task DeleteWarehouseAsync(Guid warehouseId);

        List<WarehouseListDto> GetAllWarehouses();
        WarehouseDetailDto GetWarehouseById(Guid warehouseId);
    }
}
