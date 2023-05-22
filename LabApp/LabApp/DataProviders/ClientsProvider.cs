using LabApp.Entities;
using LabApp.Repositories;

namespace LabApp.DataProviders
{
    public class ClientsProvider : IClientsProvider
    {
        private readonly IRepository<Client> _clientRepository;

        public ClientsProvider(IRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public void GetFirstInCity(string city)
        {
            var clients = _clientRepository.GetAll();
            var clientInCity = clients.FirstOrDefault(x => x.ClientAddressCity == city,
                new Client { Id = -1});

            var message = clientInCity.Id == -1 ? "Client Not Found." : clientInCity.ToString();
            Console.WriteLine(message);
        }

        public void GetUniqueCities()
        {
            var clients = _clientRepository.GetAll();
            var cities = clients.Select(x => x.ClientAddressCity)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            foreach (var city in cities)
            {
                Console.WriteLine(city);
            }
        }
    }
}
