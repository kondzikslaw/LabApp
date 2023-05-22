using LabApp.Entities;
using LabApp.Repositories;

namespace LabApp.Events
{
    public class EventsHandler : IEventsHandler
    {
        private readonly IRepository<Product> _productRepository;

        private readonly IRepository<Test> _testRepository;

        private readonly IRepository<Client> _clientRepository;

        public EventsHandler(IRepository<Product> productRepository, IRepository<Test> testRepository, IRepository<Client> clientRepository)
        {
            _productRepository = productRepository;

            _testRepository = testRepository;

            _clientRepository = clientRepository;

        }

        public void AssignEvents()
        {
            _productRepository.ItemAdded += ProductAddedToRepository;
            _productRepository.ItemRemoved += ProductRemovedFromRepository;

            _testRepository.ItemAdded += TestAddedToRepository;
            _testRepository.ItemRemoved += TestRemovedFromRepository;

            _clientRepository.ItemAdded += ClientAddedToRepository;
            _clientRepository.ItemRemoved += ClientRemovedFromRepository;
        }

        private void ProductAddedToRepository(object? sender, Product e)
        {
            UpdateAuditFile("Product added", e);
            Console.WriteLine($"Product:\n\t{e}\nadded to repository.");
        }

        private void ProductRemovedFromRepository(object? sender, Product e)
        {
            UpdateAuditFile("Product removed", e);
            Console.WriteLine($"Product:\n\t{e}\nremoved from repository.");
        }

        private void TestAddedToRepository(object? sender, Test e)
        {
            UpdateAuditFile("Test added", e);
            Console.WriteLine($"Test:\n\t{e}\nadded to repository.");
        }

        private void TestRemovedFromRepository(object? sender, Test e)
        {
            UpdateAuditFile("Test removed", e);
            Console.WriteLine($"Test:\n\t{e}\nremoved from repository.");
        }

        private void ClientAddedToRepository(object? sender, Client e)
        {
            UpdateAuditFile("Client added", e);
            Console.WriteLine($"Client:\n\t{e}\nadded to repository.");
        }

        private void ClientRemovedFromRepository(object? sender, Client e)
        {
            UpdateAuditFile("Client removed", e);
            Console.WriteLine($"Client:\n\t{e}\nremoved from repository.");
        }

        private void UpdateAuditFile<T>(string message, T entity) where T : class, IEntity
        {
            using (var writer = File.AppendText("audit.txt"))
            {
                writer.WriteLine($"{message}\t{entity}\t{DateTime.Now}");
            }    
        }
    }
}
