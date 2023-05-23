namespace LabApp.ApplicationServices.Components.DataProviders
{
    public interface IClientsProvider
    {
        void GetUniqueCities();

        void GetFirstInCity(string city);
    }
}
