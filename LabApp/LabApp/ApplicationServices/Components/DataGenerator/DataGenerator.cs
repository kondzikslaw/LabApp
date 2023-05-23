using LabApp.DataAccess.Data.Entities;
using System.Text.Json;

namespace LabApp.ApplicationServices.Components.DataGenerator
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
                    products.Add(product);
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
                    tests.Add(test);
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
                    clients.Add(client);
                }
            }

            return clients;
        }
    }
}
