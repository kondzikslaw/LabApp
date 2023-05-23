using LabApp.ApplicationServices.Components.DataGenerator;
using LabApp.ApplicationServices.Components.DataProviders;
using LabApp.ApplicationServices.Components.Menu;
using LabApp.ApplicationServices.Events;
using LabApp.DataAccess.Data;
using LabApp.DataAccess.Data.Entities;
using LabApp.DataAccess.Data.Repositories;
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
