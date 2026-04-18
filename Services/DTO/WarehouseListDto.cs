namespace Laboratory_ProductManager.Services.DTO
{
    public class WarehouseListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Location})";
        }
    }
}