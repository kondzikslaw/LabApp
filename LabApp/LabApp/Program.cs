using LabApp.Repositories;
using LabApp.Entities;
using LabApp.Data;
using LabApp.Repositories.Extensions;
using System.Text.Json;

bool closeApp = false;

var files = new[]
{
     "products.json",
     "tests.json",
     "clients.json",
     "audit.txt"
};

foreach (var file in files)
{
    if (!File.Exists(file))
    {
        using (FileStream fs = File.Create(file)) ;
    }
}

var productRepository = new SqlRepository<Product>(new LabAppDbContext());

var productFile = File.ReadAllLines(files[0]);
if (productFile.Length > 0)
{
    foreach (var line in productFile)
    {
        Product product = JsonSerializer.Deserialize<Product>(line);
        productRepository.Add(product);
        productRepository.Save();
    }
}
productRepository.ItemAdded += ProductRepositoryOnItemAdded;
productRepository.ItemRemoved += ProductRepositoryOnItemRemoved;

var testRepository = new SqlRepository<Test>(new LabAppDbContext());

var testFile = File.ReadAllLines(files[1]);
if (testFile.Length > 0)
{
    foreach (var line in testFile)
    {
        Test test = JsonSerializer.Deserialize<Test>(line);
        testRepository.Add(test);
        testRepository.Save();
    }
}
testRepository.ItemAdded += TestRepositoryOnItemAdded;
testRepository.ItemRemoved += TestRepositoryOnItemRemoved;

var clientRepository = new SqlRepository<Client>(new LabAppDbContext());

var clientFile = File.ReadAllLines(files[2]);
if (clientFile.Length > 0)
{
    foreach (var line in clientFile)
    {
        Client client = JsonSerializer.Deserialize<Client>(line);
        clientRepository.Add(client);
        clientRepository.Save();
    }
}
clientRepository.ItemAdded += ClientRepositoryOnItemAdded;
clientRepository.ItemRemoved += ClientRepositoryOnItemRemoved;

while (!closeApp)
{
    Console.WriteLine("Welcome to Lab Management App. Please choose one option from below:\n" +
        "1 - Show Products\n" +
        "2 - Show Tests\n" +
        "3 - Show Clients\n" +
        "4 - Add new Product\n" +
        "5 - Add new Test\n" +
        "6 - Add new Client\n" +
        "7 - Remove Products\n" +
        "8 - Remove Tests\n" +
        "9 - Remove Clients\n" +
        "X - Close app and save changes\n");

    var input = Console.ReadLine().ToUpper();

    switch (input)
    {
        case "1":
            WriteAllToConsole(productRepository);
            break;
        case "2":
            WriteAllToConsole(testRepository);
            break;
        case "3":
            WriteAllToConsole(clientRepository);
            break;
        case "4":
            AddProducts(productRepository);
            break;
        case "5":
            AddTests(testRepository);
            break;
        case "6":
            AddClients(clientRepository);
            break;
        case "7":
            RemoveItems(productRepository);
            break;
        case "8":
            RemoveItems(testRepository);
            break;
        case "9":
            RemoveItems(clientRepository);
            break;
        case "X":
            closeApp = true;
            CloseAppAndSave();
            break;
    }
}

void ProductRepositoryOnItemAdded(object? sender, Product e)
{
    Console.WriteLine($"Product added => {e.ProductName} from {sender?.GetType().Name}");
    SaveAudit($"{DateTime.Now} - ProductAdded - [{e.ProductName}]");
}

void ProductRepositoryOnItemRemoved(object? sender, Product e)
{
    Console.WriteLine($"Product removed => {e.ProductName} from {sender?.GetType().Name}");
    SaveAudit($"{DateTime.Now} - ProductRemoved - [{e.ProductName}]");
}

void TestRepositoryOnItemAdded(object? sender, Test e)
{
    Console.WriteLine($"Test added => {e.TestName} from {sender?.GetType().Name}");
    SaveAudit($"{DateTime.Now} - TestAdded - [{e.TestName}]");
}

void TestRepositoryOnItemRemoved(object? sender, Test e)
{
    Console.WriteLine($"Test removed => {e.TestName} from {sender?.GetType().Name}");
    SaveAudit($"{DateTime.Now} - TestRemoved - [{e.TestName}]");
}

void ClientRepositoryOnItemAdded(object? sender, Client e)
{
    Console.WriteLine($"Client added => {e.ClientName} from {sender?.GetType().Name}");
    SaveAudit($"{DateTime.Now} - ClientAdded - [{e.ClientName}]");
}

void ClientRepositoryOnItemRemoved(object? sender, Client e)
{
    Console.WriteLine($"Client removed => {e.ClientName} from {sender?.GetType().Name}");
    SaveAudit($"{DateTime.Now} - ClientRemoved - [{e.ClientName}]");
}

static void AddProducts(IRepository<Product> productRepository)
{
    Console.WriteLine("Insert Product name.");
    var name = Console.ReadLine();
    Console.WriteLine("Insert Product type");
    var type = Console.ReadLine();

    var product = new Product { ProductName = name, ProductType = type };
    productRepository.Add(product);
    productRepository.Save();
}

static void AddTests(IRepository<Test> testRepository)
{
    Console.WriteLine("Insert Test name.");
    var name = Console.ReadLine();
    Console.WriteLine("Insert Test type");
    var type = Console.ReadLine();

    var test = new Test { TestName = name, TestType = type };
    testRepository.Add(test);
    testRepository.Save();
}

static void AddClients(IRepository<Client> clientRepository)
{
    Console.WriteLine("Insert Client name.");
    var name = Console.ReadLine();
    Console.WriteLine("Insert Client address street");
    var street = Console.ReadLine();
    Console.WriteLine("Insert Client address street number");
    var streetNumber = Console.ReadLine();
    Console.WriteLine("Insert Client address city");
    var city = Console.ReadLine();
    Console.WriteLine("Insert Client address postal code");
    var postalCode = Console.ReadLine();

    var client = new Client { ClientName = name, ClientAddressStreet = street, ClientAddressNumber = streetNumber, ClientAddressCity = city, ClientAddressPostalCode = postalCode };
    clientRepository.Add(client);
    clientRepository.Save();
}

static void RemoveItems<T>(IRepository<T> repository) where T : class, IEntity
{
    Console.WriteLine($"Please insert number of Items to remove.");
    var amountToRemove = int.Parse(Console.ReadLine());
    var itemsByIdToRemove = new int[amountToRemove];
    string input;
    for (int i = 0; i < amountToRemove; i++)
    {
        Console.WriteLine("Please insert id of item you want to remove.");
        input = Console.ReadLine();
        var itemToRemove = repository.GetById(int.Parse(input));
        repository.Remove(itemToRemove);
    }
    repository.Save();
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}

void CloseAppAndSave()
{
    var products = productRepository.GetAll();
    var tests = testRepository.GetAll();
    var clients = clientRepository.GetAll();
    File.WriteAllText(files[0], String.Empty);
    File.WriteAllText(files[1], String.Empty);
    File.WriteAllText(files[2], String.Empty);
    foreach (var product in products)
    {
        var json = JsonSerializer.Serialize(product);
        using (var writer = File.AppendText(files[0]))
        {
            writer.WriteLine(json);
        }
    }
    foreach (var test in tests)
    {
        var json = JsonSerializer.Serialize(test);
        using (var writer = File.AppendText(files[1]))
        {
            writer.WriteLine(json);
        }
    }
    foreach (var client in clients)
    {
        var json = JsonSerializer.Serialize(client);
        using (var writer = File.AppendText(files[2]))
        {
            writer.WriteLine(json);
        }
    }
}

void SaveAudit(string log)
{
    using (var writer = File.AppendText(files[3]))
    {
        writer.WriteLine(log);
    }
}