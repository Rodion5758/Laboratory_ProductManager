using Laboratory_ProductManager.Common;

namespace Laboratory_ProductManager.Repositories.Warehouse
{
    public class WareHouseDBModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public WareHouseLocation Location { get; set; }

        public WareHouseDBModel() { }
        public WareHouseDBModel(string name, WareHouseLocation location)
        {
            ID = Guid.NewGuid();
            Name = name;
            Location = location;
        }
    }
}
