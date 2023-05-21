using LabApp.Entities;
using System.Text.Json;

namespace LabApp.DataGenerator
{
    public abstract class DataGenerator : IDataGenerator
    {
        public abstract void AddClients();

        public abstract void AddProducts();

        public abstract void AddTests();

        public List<Product> GetProductsFromJsonFile()
        {
            var products = new List<Product>();

            var productFile = File.ReadAllLines("products.json");
            if (productFile.Length > 0)
            {
                foreach (var line in productFile)
                {
                    Product product = JsonSerializer.Deserialize<Product>(line);
                    products.Add(new Product
                    {
                        Id = product.Id,
                        ProductName = product.ProductName,
                        ProductType = product.ProductType,
                        ArrivalTemperature = product.ArrivalTemperature,
                        StorageTemperature = product.StorageTemperature,
                        Container = product.Container,
                        ArrivalDate = product.ArrivalDate,
                        RegistrationDate = product.RegistrationDate
                    });
                }
            }

            return products;
        }

        public List<Test> GetTestsFromJsonFile()
        {
            var tests = new List<Test>();

            var testFile = File.ReadAllLines("tests.json");
            if (testFile.Length > 0)
            {
                foreach (var line in testFile)
                {
                    Test test = JsonSerializer.Deserialize<Test>(line);
                    tests.Add(new Test
                    {
                        Id = test.Id,
                        TestName = test.TestName,
                        TestType = test.TestType,
                        Price = test.Price
                    });
                }
            }

            return tests;
        }

        public List<Client> GetClientsFromJsonFile()
        {
            var clients = new List<Client>();

            var clientFile = File.ReadAllLines("clients.json");
            if (clientFile.Length > 0)
            {
                foreach (var line in clientFile)
                {
                    Client client = JsonSerializer.Deserialize<Client>(line);
                    clients.Add(new Client
                    {
                        Id = client.Id,
                        ClientName = client.ClientName,
                        ClientAddressStreet = client.ClientAddressStreet,
                        ClientAddressNumber = client.ClientAddressNumber,
                        ClientAddressCity = client.ClientAddressCity,
                        ClientAddressPostalCode = client.ClientAddressPostalCode
                    });
                }
            }

            return clients;
        }
    }
}
