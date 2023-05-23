using LabApp.DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabApp.DataAccess.Data.Repositories
{
    public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly DbSet<T> _dbSet;
        private readonly LabAppDbContext _labAppDbContext;

        public SqlRepository(LabAppDbContext labAppDbContext)
        {
            _labAppDbContext = labAppDbContext;
            _dbSet = _labAppDbContext.Set<T>();
        }

        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;

        public IEnumerable<T> GetAll()
        {
            return _dbSet.OrderBy(item => item.Id).ToList();
        }

        public T? GetById(int id)
        {
            return _dbSet.SingleOrDefault(x => x.Id == id);
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
            ItemAdded?.Invoke(this, item);
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
            ItemRemoved?.Invoke(this, item);
        }

        public void Save()
        {
            _labAppDbContext.SaveChanges();
        }
    }
}
