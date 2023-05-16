using LabApp.DataProviders;

namespace LabApp
{
    public class App : IApp
    {
        private readonly IUserCommunications _userCommunications;

        public App(IUserCommunications userCommunications)
        {
            _userCommunications = userCommunications;
        }

        public void Run()
        {
            _userCommunications.ChooseRepositoryMenu();
        }
    }
}
