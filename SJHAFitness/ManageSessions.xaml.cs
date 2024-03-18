using CommunityToolkit.Maui.Views;
using SJHAFitness.Models;
using SJHAFitness.Views;
using System.Collections.ObjectModel;
using SQLite;
using System.Runtime.CompilerServices;

namespace SJHAFitness;

public partial class ManageSessions : ContentPage
{
    ObservableCollection<Sessions> Session;

	public ManageSessions()
	{
		InitializeComponent();

        LoadSessionsFromDatabase();

        var sessions = DatabaseHelper.GetSessionsByUser(App.CurrentUser.UserID);
        fullNameLabel.Text = $"Full Name: {App.CurrentUser.FirstName} {App.CurrentUser.LastName}";

        emailLabel.Text = $"Email: {App.CurrentUser.Email}";

    }

    private void MenuPopup(object sender, EventArgs e)
    {
        var popup = new MenuBarItems();

        this.ShowPopup(popup);
    }

    private async void ManageSession(object sender, EventArgs e)
    {
        var popup = new ManageSessionItems();

        var result = await this.ShowPopupAsync(popup);

        if (result != null)
        {
            var session = (Sessions)result;

            session.UserID = App.CurrentUser.UserID;

            if (session.Session == "Andrew")
            {
                session.Image = "andrew.jpg";
            }
            else if (session.Session == "Hanbo")
            {
                session.Image = "hanbo.png";
            }
            else if (session.Session == "Shawn")
            {
                session.Image = "shawn.jpg";
            }
            else if (session.Session == "Jakob")
            {
                session.Image = "jakob.jpg";
            }

            Session.Add(session);

            sessionsList.ItemsSource = Session;

        }
    }

    private async void CancelButton(object sender, EventArgs e)
    {
        var button = sender as Button;

        var session = button?.BindingContext as Sessions;

        if (session != null)
        {
            bool answer = await DisplayAlert("Confirmation", "Are you sure you want to cancel?", "Yes", "No");

            if (answer)
            {
                var items = sessionsList.ItemsSource as ObservableCollection<Sessions>;

                if (items != null)
                {
                    items.Remove(session);
                    DatabaseHelper.DeleteSession(session);
                }
            }
        }
    }



    public void LoadSessionsFromDatabase()
    {
        var sessions = DatabaseHelper.GetSessionsByUser(App.CurrentUser.UserID);

        Session = new ObservableCollection<Sessions>(sessions);

        sessionsList.ItemsSource = Session;
    }
}
