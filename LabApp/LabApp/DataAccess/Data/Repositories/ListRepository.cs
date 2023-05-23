using LabApp.DataAccess.Data.Entities;

namespace LabApp.DataAccess.Data.Repositories
{
    public class ListRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly List<T> _items = new();

        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;

        public IEnumerable<T> GetAll()
        {
            return _items.ToList();
        }

        public void Add(T item)
        {
            _items.Add(item);
        }

        public T? GetById(int id)
        {
            return _items.Single(item => item.Id == id);
        }

        public void Remove(T item)
        {
            _items.Remove(item);
        }

        public void Save()
        {
            Console.WriteLine("Dane zapisane do listy.");
        }
    }
}
