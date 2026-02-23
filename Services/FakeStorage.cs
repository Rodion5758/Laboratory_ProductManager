
using Laboratory_ProductManager.Common.Enums;
using Laboratory_ProductManager.DBModels;

namespace Laboratory_ProductManager.Services
{
    internal class FakeStorage
    {   
        public static List<WareHouseDBModel> Warehouses { get; } = new List<WareHouseDBModel>();
        public static List<ProductDBModel> Products { get; } = new List<ProductDBModel>();


        static FakeStorage()
        {
            GenerateData();
        }

        // Used llm to generate the examples))
        private static void GenerateData()
        {
            var mainWarehouse = new WareHouseDBModel { Name = "Kyiv Warehous", Location = WareHouseLocation.Kyiv };
            var reserveWarehouse = new WareHouseDBModel { Name = "Lviv Warehouse", Location = WareHouseLocation.Lviv };
            var emptyWarehouse = new WareHouseDBModel { Name = "Mankivka Warehouse", Location = WareHouseLocation.Mankivka };

            Warehouses.Add(mainWarehouse);
            Warehouses.Add(reserveWarehouse);
            Warehouses.Add(emptyWarehouse);

            Products.Add(CreateProduct(mainWarehouse.ID, "Ноутбук ASUS ROG", 45000m, ProductCategory.Electronics));
            Products.Add(CreateProduct(mainWarehouse.ID, "Миша Logitech G Pro", 3500m, ProductCategory.Electronics));
            Products.Add(CreateProduct(mainWarehouse.ID, "Клавіатура Keychron K2", 4200m, ProductCategory.Electronics));
            Products.Add(CreateProduct(mainWarehouse.ID, "Монітор Dell UltraSharp", 15000m, ProductCategory.Electronics));
            Products.Add(CreateProduct(mainWarehouse.ID, "Кава в зернах Lavazza 1кг", 600m, ProductCategory.Food));
            Products.Add(CreateProduct(mainWarehouse.ID, "Чай Greenfield 100 пак", 150m, ProductCategory.Food));
            Products.Add(CreateProduct(mainWarehouse.ID, "Печиво Oreo", 80m, ProductCategory.Food));
            Products.Add(CreateProduct(mainWarehouse.ID, "Молоко Яготинське 2.6%", 45m, ProductCategory.Food));

            Products.Add(CreateProduct(reserveWarehouse.ID, "Генератор EcoFlow Delta", 40000m, ProductCategory.Electronics));
            Products.Add(CreateProduct(reserveWarehouse.ID, "Павербанк Xiaomi 20000mAh", 1200m, ProductCategory.Electronics));

        }

        private static ProductDBModel CreateProduct(Guid warehouseId, string name, decimal price, ProductCategory category)
        {
            return new ProductDBModel(
                wareHouseID: warehouseId,
                name: name,
                price: price,
                category: category,
                description: "Стандартний опис для " + name
            );
        }
    }
}