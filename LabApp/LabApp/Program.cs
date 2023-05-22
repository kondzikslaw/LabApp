using LabApp.Components.Menu;
using LabApp.Data;
using LabApp.DataGenerator;
using LabApp.DataProviders;
using LabApp.Entities;
using LabApp.Events;
using LabApp.Repositories;
using LabApp.UI;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IEventsHandler, EventsHandler>();
services.AddSingleton<IRepository<Product>, SqlRepository<Product>>();
services.AddSingleton<IRepository<Test>, SqlRepository<Test>>();
services.AddSingleton<IRepository<Client>, SqlRepository<Client>>();
services.AddSingleton<IUserCommunications, UserCommunications>();
services.AddSingleton<IMenu<Product>, ProductMenuSql>();
services.AddSingleton<IMenu<Test>, TestMenuSql>();
services.AddSingleton<IMenu<Client>, ClientMenuSql>();
services.AddSingleton<IProductsProvider, ProductsProvider>();
services.AddSingleton<ITestsProvider, TestsProvider>();
services.AddSingleton<IClientsProvider, ClientsProvider>();
services.AddSingleton<IDataGenerator, DataGeneratorSql>();
services.AddDbContext<LabAppDbContext>();
    

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();
app.Run();
