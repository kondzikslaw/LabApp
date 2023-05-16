﻿using LabApp.Entities;
using LabApp.Repositories;

namespace LabApp.DataProviders
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
            Console.WriteLine($"{maxPriceTest:N2}zł");
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