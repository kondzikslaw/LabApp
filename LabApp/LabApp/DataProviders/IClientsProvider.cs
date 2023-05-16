namespace LabApp.DataProviders
{
    public interface IClientsProvider
    {
        void GetUniqueCities();

        void GetFirstInCity(string city);
    }
}
