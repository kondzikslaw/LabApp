﻿using LabApp.DataProviders;
using LabApp.Entities;
using LabApp.Repositories;
using System.Globalization;

namespace LabApp.Components.Menu
{
    public class ProductMenuSql : Menu<Product>, IMenu<Product>
    {
        private readonly IRepository<Product> _productRepository;

        private readonly IProductsProvider _productsProvider;
        
        public ProductMenuSql(IRepository<Product> productRepository, IProductsProvider productsProvider) : base(productRepository)
        {
            _productRepository = productRepository;
            _productsProvider = productsProvider;
        }
        
        public new void MenuActions()
        {
            while (true)
            {
                base.MenuActions();
                var input = Console.ReadLine().ToUpper();

                if (input == "1")
                {
                    try
                    {
                        ReadAllItems();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (input == "2")
                {
                    AddNewItem();
                }
                else if (input == "3")
                {
                    RemoveItem();
                }
                else if (input == "4")
                {
                    FilterMenu();
                }
                else if (input == "B")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong input. Please try again.");
                }
            }
        }

        protected override void AddNewItem()
        {
            Console.WriteLine("Enter Product Name: ");
            var name = Console.ReadLine();
            Console.WriteLine("Enter Product Type: ");
            var type = Console.ReadLine();
            Console.WriteLine("Enter Arrival Temperature: ");
            var arrivalTemperature = decimal.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.WriteLine("Enter Storage Temperature: ");
            var storageTemperature = decimal.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.WriteLine("Enter Container: ");
            var container = Console.ReadLine();

            _productRepository.Add(new Product { ProductName = name, ProductType = type, ArrivalTemperature = arrivalTemperature, StorageTemperature = storageTemperature, Container = container, ArrivalDate = DateTime.Now.ToShortDateString(), RegistrationDate = DateTime.Now.ToString() });
            _productRepository.Save();
        }

        protected override void RemoveItem()
        {
            ReadAllItems();
            Console.WriteLine("Choose Id of Product you would like to remove: ");
            var input = int.Parse(Console.ReadLine());
            var product = _productRepository.GetById(input);

            _productRepository.Remove(product);
            _productRepository.Save();
        }
        
        protected override void FilterMenu()
        {
            while (true)
            {
                Console.WriteLine("Choose what filter you want to use:\n" +
                    "1 - Show First Product with chosen container\n" +
                    "2 - Get Frozen Products\n" +
                    "3 - Show products which temperatures difference is too high\n" +
                    "4 - Show Product Types\n" +
                    "B - Go back to previous menu.");

                var input = Console.ReadLine().ToUpper();

                if (input == "1")
                {
                    Console.WriteLine("Please enter container: ");
                    var container = Console.ReadLine();
                    _productsProvider.FirstOrDefaultByContainer(container);
                }
                else if (input == "2")
                {
                    _productsProvider.GetFrozenProducts();
                }
                else if (input == "3")
                {
                    _productsProvider.GetProductsWithTooHighDifferenceTemperatures();
                }
                else if (input == "4")
                {
                    _productsProvider.GetUniqueProductTypes();
                }
                else if (input == "B")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong input. Please try again.");
                }
            }
        }
    }
}
