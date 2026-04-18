using Laboratory_ProductManager.Common;

namespace Laboratory_ProductManager.Services.DTO
{
    public class ProductListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalCost => Price * Quantity;
        public ProductCategory Category { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Price:F2} hrn (Qty: {Quantity})";
        }
    }
}
