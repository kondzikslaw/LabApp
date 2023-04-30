using LabApp.Entities;

namespace LabApp.Repositories
{
    public interface IReadRepository<out T> where T : class, IEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
