using System;
using System.Collections.Generic;
using System.Text;
using Laboratory_ProductManager.Common.Enums;

namespace Repositories.Warehouse
{
    public class WareHouseDBModel
    {
           
        // Same as Product, generated only once and can't be changed
        public Guid ID { get; }
        public string Name { get; set; }
        // Warehouses can be moved to another place, not physically
        public WareHouseLocation Location { get; set; }

        public WareHouseDBModel() { }
        public WareHouseDBModel(string name, WareHouseLocation location)
        {
            ID = Guid.NewGuid();
            Name = name;
            Location = location;
        }
    }
}
