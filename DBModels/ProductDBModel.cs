
using Laboratory_ProductManager.Common.Enums;


namespace Laboratory_ProductManager.DBModels
{
    public class ProductDBModel
    {   
        // ID is generated only once in the constructor and can't be changed
        public Guid ID { get; }

        // WareHouseID is assigned only once in the constructor and doesn't change
        public Guid WareHouseID { get; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public string Description { get; set; }


        public ProductDBModel() { }

        public ProductDBModel(
            Guid wareHouseID,
            string name,
            decimal price,
            ProductCategory category,
            string description
            )
        {
            ID = Guid.NewGuid();
            WareHouseID = wareHouseID;
            Name = name;
            Price = price;
            Category = category;
            Description = description;
        }
    }
}
