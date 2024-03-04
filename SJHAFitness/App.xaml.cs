using SJHAFitness.Views;

namespace SJHAFitness
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DatabaseHelper.InitializeDatabase();

            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
