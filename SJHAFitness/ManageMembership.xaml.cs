using CommunityToolkit.Maui.Views;
using SJHAFitness.Models;
using SJHAFitness.Views;
using System.Collections.ObjectModel;
using SQLite;
using System.Runtime.CompilerServices;

namespace SJHAFitness;

public partial class ManageMembership : ContentPage
{
    ObservableCollection<Sessions> Session;

    private void MenuPopup(object sender, EventArgs e)
    {
        var popup = new MenuBarItems();
        this.ShowPopup(popup);
    }

    private async void OnCancelMembershipClicked(object sender, EventArgs e)
    {
        // Ask the user for confirmation
        bool isUserSure = await DisplayAlert("Cancel Membership", "Are you sure you'd like to cancel your membership?", "Yes", "No");

        if (isUserSure)
        {
            // logic for removing account from database will be here

            // Show a confirmation message
            await DisplayAlert("Membership Cancelled", "Your membership has been cancelled.", "OK");

            
            // await Navigation.PopAsync(); // navigate user back to the login page once account is removed
        }
        // if user selects no, nothing else is done
    }

    private async void OnChangeMembershipClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManageMembership());
    }

    public ManageMembership()
    {
       InitializeComponent();
        var sessions = DatabaseHelper.GetSessionsByUser(App.CurrentUser.UserID);
        firstNameLabel.Text = $"First Name: {App.CurrentUser.FirstName}";
        lastNameLabel.Text = $"Last Name: {App.CurrentUser.LastName}";
        birthdayLabel.Text = $"Birthday: {App.CurrentUser.Birthday.Date.ToShortDateString()}";
        emailLabel.Text = $"Email: {App.CurrentUser.Email}";
    }

}
