using LabApp.DataAccess.Data;

namespace LabApp.ApplicationServices.Components.DataGenerator
{
    public class DataGeneratorSql : DataGenerator, IDataGenerator
    {
        private readonly LabAppDbContext _labAppDbContext;

        public DataGeneratorSql(LabAppDbContext labAppDbContext)
        {
            _labAppDbContext = labAppDbContext;
            _labAppDbContext.Database.EnsureCreated();
        }

        public override void AddClients()
        {
            if (!_labAppDbContext.Clients.Any())
            {
                var clients = GetClientsFromJsonFile();
                _labAppDbContext.Clients.AddRange(clients);
                _labAppDbContext.SaveChanges();
            }
        }

        public override void AddProducts()
        {
            if (!_labAppDbContext.Products.Any())
            {
                var products = GetProductsFromJsonFile();
                _labAppDbContext.Products.AddRange(products);
                _labAppDbContext.SaveChanges();
            }
        }

        public override void AddTests()
        {
            if (!_labAppDbContext.Tests.Any())
            {
                var tests = GetTestsFromJsonFile();
                _labAppDbContext.Tests.AddRange(tests);
                _labAppDbContext.SaveChanges();
            }
        }
    }
}
