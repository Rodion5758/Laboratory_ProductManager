using Laboratory_ProductManager.DBModels;
using Laboratory_ProductManager.Services.WarehouseServices.Common;
using Laboratory_ProductManager.UIModels.ProductUIModel;
using Laboratory_ProductManager.UIModels.WareHouseUIModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory_ProductManager.Services.WarehouseServices
{
    public interface IWarehouseRead
    {
        public List<WarehouseView> GetAllWarehouses();

        public WarehouseView GetById(Guid warehouseId);

        public List<ProductView> GetProductsByWarehouseId(Guid warehouseId);
    }
}
