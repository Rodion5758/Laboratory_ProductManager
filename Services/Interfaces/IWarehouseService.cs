using Laboratory_ProductManager.Services.DTOs;
using System;
using System.Collections.Generic;

namespace Laboratory_ProductManager.Services.Interfaces
{
    public interface IWarehouseService
    {
        List<WarehouseListDto> GetAllWarehouses();
        WarehouseDetailDto GetWarehouseById(Guid warehouseId);
    }
}