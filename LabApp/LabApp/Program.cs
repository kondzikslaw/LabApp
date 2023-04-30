using LabApp.Repositories;
using LabApp.Entities;
using LabApp.Data;

var productRepository = new SqlRepository<Product>(new LabAppDbContext());
AddProducts(productRepository);
WriteAllToConsole(productRepository);
Console.WriteLine();

var testRepository = new SqlRepository<Test>(new LabAppDbContext());
AddTests(testRepository);
WriteAllToConsole(testRepository);
Console.WriteLine();

var clientRepository = new SqlRepository<Client>(new LabAppDbContext());
AddClients(clientRepository);
AddRegularClients(clientRepository);
WriteAllToConsole(clientRepository);

static void AddProducts(IRepository<Product> productRepository)
{
    productRepository.Add(new Product { ProductName = "Sausage", ProductType = "Meat" });
    productRepository.Add(new Product { ProductName = "Frozen Pizza", ProductType = "Frozen Food" });
    productRepository.Add(new Product { ProductName = "Apple", ProductType = "Fruits" });
    productRepository.Save();
}

static void AddTests(IRepository<Test> testRepository)
{
    testRepository.Add(new Test { TestName = "Listeria monocytogenes", TestType = "Listeria" });
    testRepository.Add(new Test { TestName = "Listeria spp.", TestType = "Listeria" });
    testRepository.Add(new Test { TestName = "Salmonella spp.", TestType = "Salmonella" });
    testRepository.Add(new Test { TestName = "Total Viable Count", TestType = "TVC" });
    testRepository.Save();
}

static void AddClients(IRepository<Client> clientRepository)
{
    clientRepository.Add(new Client { ClientName = "Sokołów", ClientAddressStreet = "Mięsna", ClientAddressNumber = "43b", ClientAddressCity = "Sokołów Podlaski", ClientAddressPostalCode = "95-435" });
    clientRepository.Add(new Client { ClientName = "Dr. Oetker", ClientAddressStreet = "Żelazna", ClientAddressNumber = "6", ClientAddressCity = "Płock", ClientAddressPostalCode = "08-342" });
    clientRepository.Add(new Client { ClientName = "Stary Sad", ClientAddressStreet = "Owocowa", ClientAddressNumber = "52", ClientAddressCity = "Jarocin", ClientAddressPostalCode = "34-644" });
    clientRepository.Save();
}

static void AddRegularClients(IWriteRepository<RegularClient> regularClientRepository)
{
    regularClientRepository.Add(new RegularClient { ClientName = "Auchan Polska", ClientAddressStreet = "Zakupowa", ClientAddressNumber = "19", ClientAddressCity = "Warszawa", ClientAddressPostalCode = "53-445" });
    regularClientRepository.Add(new RegularClient { ClientName = "Carrefour Polska", ClientAddressStreet = "Zakupowa", ClientAddressNumber = "20", ClientAddressCity = "Warszawa", ClientAddressPostalCode = "53-445" });
    regularClientRepository.Save();
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}