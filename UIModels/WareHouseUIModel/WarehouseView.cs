using Laboratory_ProductManager.Common.Enums;
using Laboratory_ProductManager.UIModels.ProductUIModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Laboratory_ProductManager.UIModels.WareHouseUIModel
{
    /// <summary>
    /// WarehouseView - DTO без залежностей від DB моделей
    /// </summary>
    public class WarehouseView
    {        
        public Guid ID { get; set; }
        public string Name { get; set; }
        public WareHouseLocation Location { get; set; }
        public List<ProductView> Products { get; set; } = new List<ProductView>();

        public int TotalProducts => Products.Count;
        public decimal TotalValue => Products.Sum(p => p.TotalCost);

        public WarehouseView()
        {
        }

        public WarehouseView(Guid id, string name, WareHouseLocation location)
        {
            ID = id;
            Name = name;
            Location = location;
        }

        public override string ToString()
        {
            return $"Warehouse: {Name} ; Location: {Location} ; Total Products: {TotalProducts} ; Total Value: {TotalValue:F2} hrn";
        }
    }
}
