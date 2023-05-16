using LabApp;
using LabApp.DataProviders;
using LabApp.Entities;
using LabApp.Repositories;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Product>, ListRepository<Product>>();
services.AddSingleton<IRepository<Test>, ListRepository<Test>>();
services.AddSingleton<IRepository<Client>, ListRepository<Client>>();
services.AddSingleton<IUserCommunications, UserCommunications>();
services.AddSingleton<IMenu<Product>, ProductMenu>();
services.AddSingleton<IMenu<Test>, TestMenu>();
services.AddSingleton<IMenu<Client>, ClientMenu>();
services.AddSingleton<IProductsProvider, ProductsProvider>();
services.AddSingleton<ITestsProvider, TestsProvider>();
services.AddSingleton<IClientsProvider, ClientsProvider>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();
app.Run();