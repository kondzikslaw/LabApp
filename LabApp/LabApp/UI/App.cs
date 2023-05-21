using LabApp.DataGenerator;

namespace LabApp.UI
{
    public class App : IApp
    {
        private readonly IUserCommunications _userCommunications;

        private readonly IDataGenerator _dataGenerator;

        public App(IUserCommunications userCommunications, IDataGenerator dataGenerator)
        {
            _userCommunications = userCommunications;
            _dataGenerator = dataGenerator;
        }

        public void Run()
        {
            _dataGenerator.AddProducts();
            _dataGenerator.AddTests();
            _dataGenerator.AddClients();

            _userCommunications.ChooseRepositoryMenu();
        }
    }
}
