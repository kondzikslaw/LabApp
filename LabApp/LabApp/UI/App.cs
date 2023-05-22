using LabApp.DataGenerator;
using LabApp.Events;

namespace LabApp.UI
{
    public class App : IApp
    {
        private readonly IUserCommunications _userCommunications;

        private readonly IDataGenerator _dataGenerator;

        private readonly IEventsHandler _eventsHandler;

        public App(IUserCommunications userCommunications, IDataGenerator dataGenerator, IEventsHandler eventsHandler)
        {
            _userCommunications = userCommunications;
            _dataGenerator = dataGenerator;
            _eventsHandler = eventsHandler;
        }

        public void Run()
        {
            _eventsHandler.AssignEvents();

            _dataGenerator.AddProducts();
            _dataGenerator.AddTests();
            _dataGenerator.AddClients();

            _userCommunications.ChooseRepositoryMenu();
        }
    }
}
