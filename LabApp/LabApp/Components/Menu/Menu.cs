using LabApp.Entities;
using LabApp.Repositories;
using System.Text.Json;

namespace LabApp.Components.Menu
{
    public abstract class Menu<T> : IMenu<T> where T : class, IEntity
    {
        private readonly IRepository<T> _repository;

        public Menu(IRepository<T> repository)
        {
            _repository = repository;
        }

        public void MenuActions()
        {
            while (true)
            {
                Console.WriteLine($"Choose what do you want to do with {typeof(T).Name}s:\n" +
                $"1 - Read all {typeof(T).Name}s\n" +
                $"2 - Add new {typeof(T).Name}\n" +
                $"3 - Remove {typeof(T).Name}\n" +
                $"4 - Filter data\n" +
                $"B - Go back to previous menu.");

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
                    try
                    {
                        AddNewItem();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (input == "3")
                {
                    try
                    {
                        RemoveItem();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (input == "4")
                {
                    try
                    {
                        FilterMenu();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
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

        protected void ReadAllItems()
        {
            if (!_repository.GetAll().Any())
            {
                throw new Exception("There is no data to present.");
            }
            var items = _repository.GetAll();

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        protected abstract void AddNewItem();

        protected virtual void RemoveItem()
        {
            ReadAllItems();
            Console.WriteLine($"Choose Id of {typeof(T).Name} you would like to remove: ");
            var input = int.Parse(Console.ReadLine());
            var item = _repository.GetById(input);

            _repository.Remove(item);
        }

        protected abstract void FilterMenu();

        protected void SaveToJsonFile()
        {
            var items = _repository.GetAll();
            var fileName = ".json";
            if (typeof(T).Name == "Product")
            {
                fileName = $"products{fileName}";
            }
            else if (typeof(T).Name == "Test")
            {
                fileName = $"tests{fileName}";
            }
            else if (typeof(T).Name == "Client")
            {
                fileName = $"clients{fileName}";
            }

            File.WriteAllText(fileName, string.Empty);
            foreach (var item in items)
            {
                var json = JsonSerializer.Serialize(item);
                using (var writer = File.AppendText(fileName))
                {
                    writer.WriteLine(json);
                }
            }
            _repository.Save();
        }

        protected int GetLastId()
        {
            if (_repository.GetAll().Count() == 0)
            {
                return 1;
            }
            else
            {
                return _repository.GetAll().Last().Id + 1;
            }
        }
    }
}
