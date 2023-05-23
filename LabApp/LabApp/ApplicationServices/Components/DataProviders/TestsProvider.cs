using LabApp.DataAccess.Data.Entities;
using LabApp.DataAccess.Data.Repositories;

namespace LabApp.ApplicationServices.Components.DataProviders
{
    public class TestsProvider : ITestsProvider
    {
        private readonly IRepository<Test> _testRepository;
        public TestsProvider(IRepository<Test> testRepository)
        {
            _testRepository = testRepository;
        }

        public void GetMaximumTestPrice()
        {
            var tests = _testRepository.GetAll();
            var maxPriceTest = tests.Select(x => x.Price).Max();
            Console.WriteLine(maxPriceTest);
        }

        public void GetUniqueTestTypes()
        {
            var tests = _testRepository.GetAll();
            var types = tests.Select(x => x.TestType)
                .Distinct()
                .ToList();

            foreach (var type in types)
            {
                Console.WriteLine(type);
            }
        }
    }
}
