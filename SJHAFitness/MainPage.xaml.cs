using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using SJHAFitness.Views;

namespace SJHAFitness
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var sessions = DatabaseHelper.GetSessionsByUser(App.CurrentUser.UserID);

            sessionList.ItemsSource = sessions;

            welcomeLabel.Text = $"Welcome back, {App.CurrentUser.FirstName}";
        }

        private void MenuPopup(object sender, EventArgs e)
        {
            var popup = new MenuBarItems();

            this.ShowPopup(popup);
        }
    }
}
