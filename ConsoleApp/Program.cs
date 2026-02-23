using System;
using System.Collections.Generic;
using Laboratory_ProductManager.Services.WarehouseServices;
using Laboratory_ProductManager.UIModels.ProductUIModel;
using Laboratory_ProductManager.UIModels.WareHouseUIModel;

namespace Laboratory_ProductManager.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            WarehouseRead warehouseService = new WarehouseRead();

            List<WarehouseView> warehouses = warehouseService.GetAllWarehouses();

            while (true)
            {
                Console.WriteLine("WAREHOUSES");
                for (int i = 0; i < warehouses.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {warehouses[i].Name} ({warehouses[i].Location})");
                }
                Console.WriteLine("Enter 0 to exit");

                Console.Write("Enter warehouse number: ");
                string input = Console.ReadLine();

                if (input == "0") break;

                if (int.TryParse(input, out int index) && index > 0 && index <= warehouses.Count)
                {
                    var selectedWarehouse = warehouses[index - 1];

                    Console.WriteLine($"About the warehouse: ");
                    Console.WriteLine($"Name: {selectedWarehouse.Name}");
                    Console.WriteLine($"ID: {selectedWarehouse.ID}");

                    if (selectedWarehouse.Products.Count == 0)
                    {
                        var loadedProducts = warehouseService.GetProductsByWarehouseId(selectedWarehouse.ID);

                        selectedWarehouse.Products.AddRange((IEnumerable<ProductView>)loadedProducts);
                    }

                    Console.WriteLine("Products in the warehouse");
                    if (selectedWarehouse.Products.Count == 0)
                    {
                        Console.WriteLine("Empty");
                    }
                    else
                    {
                        for (int p = 0; p < selectedWarehouse.Products.Count; p++)
                        {
                            var prod = selectedWarehouse.Products[p];
                            Console.WriteLine($"{p + 1}. {prod.Name} - {prod.Price} грн");
                        }

                        Console.Write("Enter product number to view details: ");
                        string prodInput = Console.ReadLine();

                        if (int.TryParse(prodInput, out int prodIndex) && prodIndex > 0 && prodIndex <= selectedWarehouse.Products.Count)
                        {
                            var selectedProduct = selectedWarehouse.Products[prodIndex - 1];
                            Console.WriteLine("Product info");
                            Console.WriteLine(selectedProduct.ToString());
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No such warehosue!");
                }
            }
        }
    }
}