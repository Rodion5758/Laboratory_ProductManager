namespace Laboratory_ProductManager.Services.DTO
{
    public class WarehouseDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int TotalProducts { get; set; }
        public decimal TotalValue { get; set; }
        public List<ProductListDto> Products { get; set; } = new List<ProductListDto>();

        public override string ToString()
        {
            return $"{Name} ({Location}) - Products: {TotalProducts}, Value: {TotalValue:F2} hrn";
        }
    }
}