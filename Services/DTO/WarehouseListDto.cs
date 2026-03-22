using System;

namespace Laboratory_ProductManager.Services.DTOs
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