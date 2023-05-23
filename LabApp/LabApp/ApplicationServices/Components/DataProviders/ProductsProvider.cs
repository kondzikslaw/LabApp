using LabApp.DataAccess.Data.Entities;
using LabApp.DataAccess.Data.Repositories;

namespace LabApp.ApplicationServices.Components.DataProviders
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly IRepository<Product> _productRepository;
        public ProductsProvider(IRepository<Product> productsRepository)
        {
            _productRepository = productsRepository;
        }

        public void FirstOrDefaultByContainer(string container)
        {
            var products = _productRepository.GetAll();
            var firstProduct = products.FirstOrDefault(x => x.Container == container,
                new Product { Id = -1 });

            var message = firstProduct.Id == -1 ? "Product not found." : firstProduct.ToString();
            Console.WriteLine(message);
        }

        public void GetFrozenProducts()
        {
            var products = _productRepository.GetAll();
            var frozenProducts = products
            .Where(x => x.ArrivalTemperature < 0)
            .OrderBy(x => x.ArrivalTemperature)
            .ToList();

            foreach (var frozenProduct in frozenProducts)
            {
                Console.WriteLine(frozenProduct);
            }
        }

        public void GetProductsWithTooHighDifferenceTemperatures()
        {
            decimal difference = 0.5M;
            var products = _productRepository.GetAll();
            var productsWithDifference = products.Where(x => Math.Abs(x.ArrivalTemperature - x.StorageTemperature) > difference)
                .OrderBy(x => x.ProductName)
                .ToList();

            foreach (var product in productsWithDifference)
            {
                Console.WriteLine(product);
            }
        }

        public void GetUniqueProductTypes()
        {
            var products = _productRepository.GetAll();
            var types = products.Select(x => x.ProductType)
                .Distinct()
                .OrderBy(types => types)
                .ToList();

            foreach (var type in types)
            {
                Console.WriteLine(type);
            }
        }
    }
}
