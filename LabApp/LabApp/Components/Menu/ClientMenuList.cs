using LabApp.DataProviders;
using LabApp.Entities;
using LabApp.Repositories;
using System.Text.Json;

namespace LabApp.Components.Menu
{
    public class ClientMenuList : Menu<Client>, IMenu<Client>
    {
        private readonly IRepository<Client> _clientRepository;

        private readonly IClientsProvider _clientsProvider;

        public ClientMenuList(IRepository<Client> clientRepository, IClientsProvider clientsProvider) : base(clientRepository)
        {
            _clientRepository = clientRepository;
            _clientsProvider = clientsProvider;
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
            Console.WriteLine("Enter Client Name: ");
            var name = Console.ReadLine();
            Console.WriteLine("Enter Client Address Street: ");
            var street = Console.ReadLine();
            Console.WriteLine("Enter Client Address Number: ");
            var number = Console.ReadLine();
            Console.WriteLine("Enter Client Address City: ");
            var city = Console.ReadLine();
            Console.WriteLine("Enter Client Address Postal Code: ");
            var postalCode = Console.ReadLine();

            _clientRepository.Add(new Client { Id = GetLastId(), ClientName = name, ClientAddressStreet = street, ClientAddressNumber = number, ClientAddressCity = city, ClientAddressPostalCode = postalCode });
            SaveClientsToJsonFile();
        }

        protected override void RemoveItem()
        {
            ReadAllItems();
            Console.WriteLine("Choose Id of Client you would like to remove: ");
            var input = int.Parse(Console.ReadLine());
            var client = _clientRepository.GetById(input);

            _clientRepository.Remove(client);
            SaveClientsToJsonFile();
        }

        protected void SaveClientsToJsonFile()
        {
            var clients = _clientRepository.GetAll();
            File.WriteAllText("clients.json", string.Empty);
            foreach (var client in clients)
            {
                var json = JsonSerializer.Serialize(client);
                using (var writer = File.AppendText("clients.json"))
                {
                    writer.WriteLine(json);
                }
            }
            _clientRepository.Save();
        }

        protected override void FilterMenu()
        {
            while (true)
            {
                Console.WriteLine("Choose what filter you want to use:\n" +
                "1 - Show first Client in chosen city\n" +
                "2 - Show cities\n" +
                "B - Go back to previous menu.");

                var input = Console.ReadLine().ToUpper();

                if (input == "1")
                {
                    Console.WriteLine("Enter city: ");
                    var city = Console.ReadLine();
                    _clientsProvider.GetFirstInCity(city);
                }
                else if (input == "2")
                {
                    _clientsProvider.GetUniqueCities();
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

        private int GetLastId()
        {
            if (_clientRepository.GetAll().Count() == 0)
            {
                return 1;
            }
            else
            {
                return _clientRepository.GetAll().Last().Id + 1;
            }
        }
    }
}
