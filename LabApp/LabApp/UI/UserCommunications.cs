using LabApp.Components.Menu;
using LabApp.Entities;

namespace LabApp.UI
{
    public class UserCommunications : IUserCommunications
    {
        private readonly IMenu<Product> _productMenu;

        private readonly IMenu<Test> _testMenu;

        private readonly IMenu<Client> _clientMenu;

        public UserCommunications(IMenu<Product> productMenu, IMenu<Test> testMenu, IMenu<Client> clientMenu)
        {
            _productMenu = productMenu;
            _testMenu = testMenu;
            _clientMenu = clientMenu;
        }

        public void ChooseRepositoryMenu()
        {
            while (true)
            {
                Console.WriteLine("Welcome to Lab Management App. Please choose which resource would you like to work with:\n" +
                "1 - Products\n" +
                "2 - Tests\n" +
                "3 - Clients\n" +
                "X - Close app and save changes\n");

                var input = Console.ReadLine().ToUpper();

                if (input == "1")
                {
                    _productMenu.MenuActions();
                }
                else if (input == "2")
                {
                    _testMenu.MenuActions();
                }
                else if (input == "3")
                {
                    _clientMenu.MenuActions();
                }
                else if (input == "X")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong input. Please try again.");
                }
            }
        }
    }
}
