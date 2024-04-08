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

            // Since the constructor cannot be async, you need to call an async method without awaiting it
            InitializeSessionList();
        }

        private async void InitializeSessionList()
        {
            try
            {
                // Await the GetSessionsByUserAsync method to get the actual List<Sessions>
                var sessions = await DatabaseHelper.GetSessionsByUserAsync(App.CurrentUser.UserID);

                // Now you have the List<Sessions>, which you can assign to the ItemsSource
                sessionList.ItemsSource = sessions;

                // Update UI elements like labels asynchronously on the UI thread
                welcomeLabel.Text = $"Welcome back {App.CurrentUser.FirstName}!";
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during initialization
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private void MenuPopup(object sender, EventArgs e)
        {
            var popup = new MenuBarItems();

            this.ShowPopup(popup);
        }
    }
}
