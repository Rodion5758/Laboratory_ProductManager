using System;
using System.ComponentModel;
using Laboratory_ProductManager.Common.Enums;

namespace Laboratory_ProductManager.Services.DTOs
{
    public class ProductListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ProductCategory Category { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Price:F2} hrn (Qty: {Quantity})";
        }
    }
}