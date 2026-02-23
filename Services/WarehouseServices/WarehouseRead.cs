using Laboratory_ProductManager.DBModels;
using Laboratory_ProductManager.Services.WarehouseServices.Common;
using Laboratory_ProductManager.UIModels.ProductUIModel;
using Laboratory_ProductManager.UIModels.WareHouseUIModel;

namespace Laboratory_ProductManager.Services.WarehouseServices
{
    public class WarehouseRead
    {
        public List<WarehouseView> GetAllWarehouses()
        {
            List<WarehouseView> finalList = new List<WarehouseView>();

            foreach (var warehouse in FakeStorage.Warehouses)
            {
                List<ProductDBModel> products = GetProductsByWarehouseId(warehouse.ID);
                var warehouseView = new  WarehouseView(warehouse);

                foreach (var product in products)
                {
                    warehouseView.Products.Add(new ProductView(product));
                }

                finalList.Add(warehouseView);
            }

            return finalList;
        }

        public WarehouseView GetById(Guid warehouseId)
        {
            WareHouseDBModel? dbmodel = WarehouseRetrieve.GetWarehouseDbModelById(warehouseId);

            if (dbmodel == null) throw new ArgumentNullException();

            List<ProductDBModel> products = GetProductsByWarehouseId(warehouseId);

            var warehouseView = new WarehouseView(dbmodel);

            foreach (var product in products)
            {
                warehouseView.Products.Add(new ProductView(product));
            }

            return warehouseView;
        }

        public List<ProductDBModel> GetProductsByWarehouseId(Guid warehouseId)
        {
            List<ProductDBModel> products = new List<ProductDBModel>();

            foreach (var product in FakeStorage.Products)
            {
                if (product.WareHouseID == warehouseId)
                {
                    products.Add(product);
                }
            }

            return products;
        }
    }
}