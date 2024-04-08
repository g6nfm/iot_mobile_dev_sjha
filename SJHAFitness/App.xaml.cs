using Microsoft.EntityFrameworkCore;
using SJHAFitness.Views;
using System.ComponentModel;

namespace SJHAFitness
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Manually create a new DbContextOptionsBuilder and configure it
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=tcp:sjhadb.database.windows.net,1433;Initial Catalog=SJHAdb;Persist Security Info=False;User ID=sjhadev;Password=DevAccount12345;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            // Manually create an instance of AppDbContext
            var context = new AppDbContext(optionsBuilder.Options);

            // Pass this instance to DatabaseHelper to store it in _context
            DatabaseHelper.Initialize(context);

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
