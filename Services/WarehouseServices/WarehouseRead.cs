using Laboratory_ProductManager.DBModels;
using Laboratory_ProductManager.Services.WarehouseServices.Common;
using Laboratory_ProductManager.UIModels.ProductUIModel;
using Laboratory_ProductManager.UIModels.WareHouseUIModel;

namespace Laboratory_ProductManager.Services.WarehouseServices
{
    public class WarehouseRead : IWarehouseRead
    {
        public List<WarehouseView> GetAllWarehouses()
        {
            List<WarehouseView> finalList = new List<WarehouseView>();

            foreach (var warehouse in FakeStorage.Warehouses)
            {
                // Буде ліниве завантаження
                var warehouseView = new  WarehouseView(warehouse);
                finalList.Add(warehouseView);
            }

            return finalList;
        }

        public WarehouseView GetById(Guid warehouseId)
        {
            WareHouseDBModel? dbmodel = WarehouseRetrieve.GetWarehouseDbModelById(warehouseId);

            if (dbmodel == null) throw new ArgumentNullException();

            var warehouseView = new WarehouseView(dbmodel);

            List<ProductView> products = GetProductsByWarehouseId(warehouseId);

            foreach (var product in products)
            {
                warehouseView.Products.Add(product);
            }

            return warehouseView;
        }

        public List<ProductView> GetProductsByWarehouseId(Guid warehouseId)
        {
            List<ProductView> products = new List<ProductView>();

            foreach (var product in FakeStorage.Products)
            {
                if (product.WareHouseID == warehouseId)
                {
                    products.Add(new ProductView(product));
                }
            }

            return products;
        }
    }
}