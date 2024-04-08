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
            // Assuming _databaseHelper is your DatabaseHelper instance and has the CancelMembershipAsync method
            bool result = await DatabaseHelper.CancelMembershipAsync(App.CurrentUser.UserID);

            if (result)
            {
                await DisplayAlert("Membership Cancelled", "Your membership has been cancelled.", "OK");
                await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
          
        }
    }

    private async void OnChangeMembershipClicked(object sender, EventArgs e)
    {
        var popup = new ManageMembershipItems();
       
        var result = await this.ShowPopupAsync(popup);
        //await Navigation.PushAsync(new ManageMembership());
    }

    public ManageMembership()
    {
       InitializeComponent();
        var sessions = DatabaseHelper.GetSessionsByUserAsync(App.CurrentUser.UserID);
        firstNameLabel.Text = $"First Name: {App.CurrentUser.FirstName}";
        lastNameLabel.Text = $"Last Name: {App.CurrentUser.LastName}";
        birthdayLabel.Text = App.CurrentUser.Birthday.HasValue
                     ? $"Birthday: {App.CurrentUser.Birthday.Value.ToShortDateString()}"
                     : "Birthday: N/A";
        emailLabel.Text = $"Email: {App.CurrentUser.Email}";
        membershipNameLabel.Text = $"Membership Type: {App.CurrentUser.MembershipName}";
        membershipTermLabel.Text = $"Membership Length: {App.CurrentUser.MembershipTerm} Months";
        membershipStartLabel.Text = App.CurrentUser.MembershipStartDate.HasValue
                            ? $"Membership Start Date: {App.CurrentUser.MembershipStartDate.Value.ToShortDateString()}"
                            : "Membership Start Date: N/A";
        membershipEndLabel.Text = App.CurrentUser.MembershipEndDate.HasValue
                          ? $"Membership Expiry Date: {App.CurrentUser.MembershipEndDate.Value.ToShortDateString()}"
                          : "Membership Expiry Date: N/A";
    }

}
