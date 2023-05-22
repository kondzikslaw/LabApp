﻿using LabApp.DataProviders;
using LabApp.Entities;
using LabApp.Repositories;

namespace LabApp.Components.Menu
{
    public class ClientMenuSql : Menu<Client>, IMenu<Client>
    {
        private readonly IRepository<Client> _clientRepository;

        private readonly IClientsProvider _clientsProvider;

        public ClientMenuSql(IRepository<Client> clientRepository, IClientsProvider clientsProvider) : base(clientRepository)
        {
            _clientRepository = clientRepository;
            _clientsProvider = clientsProvider;
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

            _clientRepository.Add(new Client { ClientName = name, ClientAddressStreet = street, ClientAddressNumber = number, ClientAddressCity = city, ClientAddressPostalCode = postalCode });
            _clientRepository.Save();
        }

        protected override void RemoveItem()
        {
            base.RemoveItem();
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
    }
}
