using CommunityToolkit.Maui.Views;
using SJHAFitness.Models;
using SJHAFitness.Views;
using System.Collections.ObjectModel;

namespace SJHAFitness;

public partial class ManageSessions : ContentPage
{
    ObservableCollection<Sessions> Session;

	public ManageSessions()
	{
		InitializeComponent();
        Session = new ObservableCollection<Sessions>();

        var sessions = DatabaseHelper.GetSessionsByUser(App.CurrentUser.UserID);
        fullNameLabel.Text = $"Member Name: {App.CurrentUser.FirstName} {App.CurrentUser.LastName}";

        emailLabel.Text = $"Member Email: {App.CurrentUser.Email}";
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
}