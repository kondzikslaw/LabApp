using LabApp.DataAccess.Data.Entities;
using LabApp.DataAccess.Data.Repositories;

namespace LabApp.ApplicationServices.Components.DataGenerator
{
    internal class DataGeneratorList : DataGenerator, IDataGenerator
    {
        private readonly IRepository<Product> _productRepository;

        private readonly IRepository<Test> _testRepository;

        private readonly IRepository<Client> _clientRepository;

        public DataGeneratorList(IRepository<Product> productRepository, IRepository<Test> testRepository, IRepository<Client> clientRepository)
        {
            _productRepository = productRepository;
            _testRepository = testRepository;
            _clientRepository = clientRepository;
        }

        public override void AddClients()
        {

            var clients = GetClientsFromJsonFile();
            foreach (var client in clients)
            {
                _clientRepository.Add(client);
            }
            _clientRepository.Save();
        }

        public override void AddProducts()
        {

            var products = GetProductsFromJsonFile();
            foreach (var product in products)
            {
                _productRepository.Add(product);
            }
            _productRepository.Save();

        }

        public override void AddTests()
        {
            var tests = GetTestsFromJsonFile();
            foreach (var test in tests)
            {
                _testRepository.Add(test);
            }
            _clientRepository.Save();
        }
    }
}
