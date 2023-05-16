using LabApp.Entities;
using LabApp.Repositories;

namespace LabApp
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
                Console.WriteLine($"Choose what do you want to do with {typeof(T).Name}s:\n" +
                $"1 - Read all {typeof(T).Name}s\n" +
                $"2 - Add new {typeof(T).Name}\n" +
                $"3 - Remove {typeof(T).Name}\n" +
                $"4 - Filter data\n" +
                $"B - Go back to previous menu."); 
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

        protected abstract void RemoveItem();
        

    }
}
