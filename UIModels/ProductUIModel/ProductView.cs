using Laboratory_ProductManager.Common.Enums;
using System;

namespace Laboratory_ProductManager.UIModels.ProductUIModel
{
    /// <summary>
    /// ProductView - DTO без залежностей від DB моделей
    /// </summary>
    public class ProductView
    {
        public Guid ID { get; set; }
        public Guid WareHouseId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalCost => Price * Quantity;
        public ProductCategory Category { get; set; }
        public string Description { get; set; }

        public ProductView()
        {
        }

        public ProductView(Guid id, Guid warehouseId, string name, decimal price, int quantity, ProductCategory category, string description)
        {
            ID = id;
            WareHouseId = warehouseId;
            Name = name;
            Price = price;
            Quantity = quantity;
            Category = category;
            Description = description;
        }

        public override string ToString()
        {
            return $"Product: {Name} ; Category: {Category} ; Price: {Price:F2} hrn ; Quantity: {Quantity} ; Total Cost: {TotalCost:F2} hrn";
        }
    }
}
