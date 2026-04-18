namespace Laboratory_ProductManager.Services.DTO
{
    public class ProductDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal TotalCost { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Price:F2} hrn (Qty: {Quantity}) - Total: {TotalCost:F2} hrn";
        }
    }
}