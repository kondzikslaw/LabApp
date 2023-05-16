using LabApp.Entities;

namespace LabApp
{
    public interface IMenu<T> where T : class
    {
        void MenuActions();
    }
}
