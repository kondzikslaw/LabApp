using LabApp.DataProviders;
using LabApp.Entities;
using LabApp.Repositories;
using System.Globalization;
using System.Text.Json;

namespace LabApp.Components.Menu
{
    public class TestMenuList : Menu<Test>, IMenu<Test>
    {
        private readonly IRepository<Test> _testRepository;

        private readonly ITestsProvider _testProvider;

        public TestMenuList(IRepository<Test> testRepository, ITestsProvider testsProvider) : base(testRepository)
        {
            _testRepository = testRepository;
            _testProvider = testsProvider;
        }
                
        protected override void AddNewItem()
        {
            Console.WriteLine("Enter Test Name: ");
            var name = Console.ReadLine();
            Console.WriteLine("Enter Test Type: ");
            var type = Console.ReadLine();
            Console.WriteLine("Enter Price: ");
            var priceString = Console.ReadLine();
            var price = decimal.TryParse(priceString, CultureInfo.InvariantCulture, out decimal result) ? result : throw new Exception("Wrong input. Please try again");

            _testRepository.Add(new Test { Id = GetLastId(), TestName = name, TestType = type, Price = price });
            SaveToJsonFile();
        }

        protected override void RemoveItem()
        {
            base.RemoveItem();
            SaveToJsonFile();
        }

        protected override void FilterMenu()
        {
            while (true)
            {
                Console.WriteLine("Choose what filter you want to use:\n" +
                "1 - Show the highest price\n" +
                "2 - Show test types\n" +
                "B - Go back to previous menu.");

                var input = Console.ReadLine().ToUpper();

                if (input == "1")
                {
                    _testProvider.GetMaximumTestPrice();
                }
                else if (input == "2")
                {
                    _testProvider.GetUniqueTestTypes();
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
