using Laboratory_ProductManager.Common.Enums;


namespace Repositories.Product
{
    public class ProductDBModel
    {   
        // ID is generated only once in the constructor and can't be changed
        public Guid ID { get; }

        // WareHouseID is assigned only once in the constructor and doesn't change
        public Guid WareHouseID { get; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ProductCategory Category { get; set; }
        public string Description { get; set; }


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
