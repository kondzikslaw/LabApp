using LabApp.DataProviders;
using LabApp.Entities;
using LabApp.Repositories;
using System.Globalization;
using System.Text.Json;

namespace LabApp
{
    public class TestMenu : Menu<Test>, IMenu<Test>
    {
        private readonly IRepository<Test> _testRepository;

        private readonly ITestsProvider _testProvider;

        public TestMenu(IRepository<Test> testRepository, ITestsProvider testsProvider) :base(testRepository)
        {
            _testRepository = testRepository;
            _testProvider = testsProvider;
        }

        public new void MenuActions()
        {
            GetTestsFromJsonFile();            
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
                    FilterTestsMenu();
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
            Console.WriteLine("Enter Test Name: ");
            var name = Console.ReadLine();
            Console.WriteLine("Enter Test Type: ");
            var type = Console.ReadLine();
            Console.WriteLine("Enter Price: ");
            var price = decimal.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            _testRepository.Add(new Test { Id = GetLastId(), TestName = name, TestType = type, Price = price });
            SaveTestsToJsonFile();
        }

        protected override void RemoveItem()
        {
            ReadAllItems();
            Console.WriteLine("Choose Id of Test you would like to remove: ");
            var input = int.Parse(Console.ReadLine());
            var test = _testRepository.GetById(input);

            _testRepository.Remove(test);
            SaveTestsToJsonFile();
        }

        protected void GetTestsFromJsonFile()
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
            foreach (var test in tests)
            {
                _testRepository.Add(test);
            }
        }

        protected void SaveTestsToJsonFile()
        {
            var tests = _testRepository.GetAll();
            File.WriteAllText("tests.json", String.Empty);
            foreach (var test in tests)
            {
                var json = JsonSerializer.Serialize(test);
                using (var writer = File.AppendText("tests.json"))
                {
                    writer.WriteLine(json);
                }
            }
            _testRepository.Save();
        }

        protected void FilterTestsMenu()
        {
            while (true)
            {
                Console.WriteLine("Choose what filter you want to use:\n" +
                "1 - Show test with the highest price\n" +
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

        private int GetLastId()
        {
            if (_testRepository.GetAll().Count() == 0)
            {
                return 1;
            }
            else
            {
                return _testRepository.GetAll().Last().Id + 1;
            }
        }
    }
}
