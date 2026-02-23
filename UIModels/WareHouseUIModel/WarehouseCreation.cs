
using Laboratory_ProductManager.Common.Enums;
using Laboratory_ProductManager.DBModels;
using Laboratory_ProductManager.UIModels.ProductUIModel;

namespace Laboratory_ProductManager.UIModels.WareHouseUIModel
{
    public class WarehouseCreation
    {
        public string Name { get; set; }
        public WareHouseLocation Location { get; set; }

        public List<ProductView> Products { get; set; } = new List<ProductView>();

        public WarehouseCreation() { }

        public WareHouseDBModel SaveChanges()
        {
            return new WareHouseDBModel(
                name: Name,
                location: Location
            );
        }

    }
}
