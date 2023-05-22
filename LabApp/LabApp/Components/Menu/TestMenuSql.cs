using LabApp.DataProviders;
using LabApp.Entities;
using LabApp.Repositories;
using System.Globalization;

namespace LabApp.Components.Menu
{
    public class TestMenuSql : Menu<Test>, IMenu<Test>
    {
        private readonly IRepository<Test> _testRepository;

        private readonly ITestsProvider _testsProvider;
        
        public TestMenuSql(IRepository<Test> testRepository, ITestsProvider testsProvider) : base(testRepository)
        {
            _testsProvider = testsProvider;
            _testRepository = testRepository;
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

            _testRepository.Add(new Test { TestName = name, TestType = type, Price = price });
            _testRepository.Save();
        }

        protected override void RemoveItem()
        {
            base.RemoveItem();
            _testRepository.Save();
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
                    _testsProvider.GetMaximumTestPrice();
                }
                else if (input == "2")
                {
                    _testsProvider.GetUniqueTestTypes();
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
