using LabApp.DataAccess.Data.Entities;

namespace LabApp.DataAccess.Data.Repositories
{
    public interface IReadRepository<out T> where T : class, IEntity
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
    }
}
