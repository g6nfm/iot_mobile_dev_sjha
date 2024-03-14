using SJHAFitness.Views;
using System.ComponentModel;

namespace SJHAFitness
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DatabaseHelper.InitializeDatabase();

            MainPage = new NavigationPage(new LoginPage());

            var currentUser = CurrentUser;

        }

        private static Account loggedinUser;

        public static Account CurrentUser
        {
            get
            {
                return loggedinUser;
            }
            set
            {
                if (loggedinUser != value)
                {
                    loggedinUser = value;

                }
            }
        }
    }
}
