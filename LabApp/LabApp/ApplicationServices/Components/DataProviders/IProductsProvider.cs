namespace LabApp.ApplicationServices.Components.DataProviders
{
    public interface IProductsProvider
    {
        void GetUniqueProductTypes();

        void GetFrozenProducts();

        void GetProductsWithTooHighDifferenceTemperatures();

        void FirstOrDefaultByContainer(string container);
    }
}
