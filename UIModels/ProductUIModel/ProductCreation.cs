
using Laboratory_ProductManager.Common.Enums;
using Laboratory_ProductManager.DBModels;

namespace Laboratory_ProductManager.UIModels.ProductUIModel
{
    // Seperate class for creating the entity via ui

    public class ProductCreation
    {
        // WareHouseID is assigned only once in the constructor and doesn't change
        // In my implementation the product has "шлюб до смерті" with the warehouse
        public Guid WareHouseID { get; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public string Description { get; set; }
        public ProductCreation(Guid departement) 
        {
            WareHouseID = departement ;
        }

        public ProductDBModel SaveChanges()
        {
            return new ProductDBModel(
                wareHouseID: WareHouseID,
                name: Name,
                price: Price,
                category: Category,
                description: Description
            );
        }
    }
}
