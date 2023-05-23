using LabApp.ApplicationServices.Components.DataProviders;
using LabApp.DataAccess.Data.Entities;
using LabApp.DataAccess.Data.Repositories;
using System.Globalization;

namespace LabApp.ApplicationServices.Components.Menu
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

        protected override void AddNewItem()
        {
            Console.WriteLine("Enter Product Name: ");
            var name = Console.ReadLine();
            Console.WriteLine("Enter Product Type: ");
            var type = Console.ReadLine();
            Console.WriteLine("Enter Arrival Temperature: ");
            var arrivalTemperatureString = Console.ReadLine();
            var arrivalTemperature = decimal.TryParse(arrivalTemperatureString, CultureInfo.InvariantCulture, out decimal result) ? result : throw new Exception("Wrong input. Please try again.");
            Console.WriteLine("Enter Storage Temperature: ");
            var storageTemperatureString = Console.ReadLine();
            var storageTemperature = decimal.TryParse(storageTemperatureString, CultureInfo.InvariantCulture, out result) ? result : throw new Exception("Wrong input. Please try again.");
            Console.WriteLine("Enter Container: ");
            var container = Console.ReadLine();

            _productRepository.Add(new Product { ProductName = name, ProductType = type, ArrivalTemperature = arrivalTemperature, StorageTemperature = storageTemperature, Container = container, ArrivalDate = DateTime.Now.ToShortDateString(), RegistrationDate = DateTime.Now.ToString() });
            _productRepository.Save();
        }

        protected override void RemoveItem()
        {
            base.RemoveItem();
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
