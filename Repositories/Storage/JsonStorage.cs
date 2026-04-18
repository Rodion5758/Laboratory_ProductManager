using System.Text.Json;
using Laboratory_ProductManager.Common;
using Laboratory_ProductManager.Repositories.Product;
using Laboratory_ProductManager.Repositories.Warehouse;

namespace Laboratory_ProductManager.Repositories.Storage
{
    public class JsonStorage
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { WriteIndented = true };

        public JsonStorage()
        {
            var folderPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Laboratory_ProductManager");

            Directory.CreateDirectory(folderPath);
            _filePath = Path.Combine(folderPath, "storage.json");
        }

        public async Task<StorageData> LoadAsync()
        {
            if (!File.Exists(_filePath))
            {
                var initialData = CreateInitialData();
                await SaveAsync(initialData);
                return initialData;
            }

            await using var stream = File.OpenRead(_filePath);
            var data = await JsonSerializer.DeserializeAsync<StorageData>(stream, _jsonOptions);
            return data ?? new StorageData();
        }

        public async Task SaveAsync(StorageData data)
        {
            await using var stream = File.Create(_filePath);
            await JsonSerializer.SerializeAsync(stream, data, _jsonOptions);
        }

        public StorageData Load()
        {
            return LoadAsync().GetAwaiter().GetResult();
        }

        public void Save(StorageData data)
        {
            SaveAsync(data).GetAwaiter().GetResult();
        }

        private static StorageData CreateInitialData()
        {
            var data = new StorageData();

            var mainWarehouse = new WareHouseDBModel("Kyiv Warehouse", WareHouseLocation.Kyiv);
            var reserveWarehouse = new WareHouseDBModel("Lviv Warehouse", WareHouseLocation.Lviv);
            var emptyWarehouse = new WareHouseDBModel("Mankivka Warehouse", WareHouseLocation.Mankivka);

            data.Warehouses.Add(mainWarehouse);
            data.Warehouses.Add(reserveWarehouse);
            data.Warehouses.Add(emptyWarehouse);

            data.Products.Add(CreateProduct(mainWarehouse.ID, "Ноутбук ASUS ROG", 45000m, ProductCategory.Electronics));
            data.Products.Add(CreateProduct(mainWarehouse.ID, "Миша Logitech G Pro", 3500m, ProductCategory.Electronics));
            data.Products.Add(CreateProduct(mainWarehouse.ID, "Клавіатура Keychron K2", 4200m, ProductCategory.Electronics));
            data.Products.Add(CreateProduct(mainWarehouse.ID, "Монітор Dell UltraSharp", 15000m, ProductCategory.Electronics));
            data.Products.Add(CreateProduct(mainWarehouse.ID, "Кава в зернах Lavazza 1кг", 600m, ProductCategory.Food));
            data.Products.Add(CreateProduct(mainWarehouse.ID, "Чай Greenfield 100 пак", 150m, ProductCategory.Food));
            data.Products.Add(CreateProduct(mainWarehouse.ID, "Печиво Oreo", 80m, ProductCategory.Food));
            data.Products.Add(CreateProduct(mainWarehouse.ID, "Молоко Яготинське 2.6%", 45m, ProductCategory.Food));
            data.Products.Add(CreateProduct(reserveWarehouse.ID, "Генератор EcoFlow Delta", 40000m, ProductCategory.Electronics));
            data.Products.Add(CreateProduct(reserveWarehouse.ID, "Павербанк Xiaomi 20000mAh", 1200m, ProductCategory.Electronics));

            return data;
        }

        private static ProductDBModel CreateProduct(Guid warehouseId, string name, decimal price, ProductCategory category)
        {
            return new ProductDBModel(
                wareHouseID: warehouseId,
                name: name,
                price: price,
                quantity: 1,
                category: category,
                description: "Стандартний опис для " + name
            );
        }
    }
}
