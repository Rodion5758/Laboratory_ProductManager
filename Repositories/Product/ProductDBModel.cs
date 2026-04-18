using Laboratory_ProductManager.Common;

namespace Laboratory_ProductManager.Repositories.Product
{
    public class ProductDBModel
    {
        public Guid ID { get; set; }
        public Guid WareHouseID { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ProductCategory Category { get; set; }
        public string Description { get; set; } = string.Empty;

        public ProductDBModel() { }

        public ProductDBModel(
            Guid wareHouseID,
            string name,
            decimal price,
            int quantity,
            ProductCategory category,
            string description
            )
        {
            ID = Guid.NewGuid();
            WareHouseID = wareHouseID;
            Name = name;
            Price = price;
            Quantity = quantity;
            Category = category;
            Description = description;
        }
    }
}
