using System;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.ApplicationModel.Communication;
using SJHAFitness.Views;
using SQLite;

namespace SJHAFitness
{
    public partial class ManageAccount : ContentPage
    {
        public ManageAccount()
        {
            InitializeComponent();

            var sessions = DatabaseHelper.GetSessionsByUserAsync(App.CurrentUser.UserID);
            firstNameLabel.Text = $"First Name: {App.CurrentUser.FirstName}";
            lastNameLabel.Text = $"Last Name: {App.CurrentUser.LastName}";
            birthdayLabel.Text = App.CurrentUser.Birthday.HasValue
                     ? $"Birthday: {App.CurrentUser.Birthday.Value.ToShortDateString()}"
                     : "Birthday: N/A";
            emailLabel.Text = $"Email: {App.CurrentUser.Email}";
        }

        private void MenuPopup(object sender, EventArgs e)
        {
            var popup = new MenuBarItems();
            this.ShowPopup(popup);
        }

        async void OnManageMembershipClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageMembership());
        }

        void OnChangePasswordClicked(object sender, EventArgs e)
        {
            // Toggle the visibility of the password fields
            PasswordFields.IsVisible = !PasswordFields.IsVisible;
            if (App.CurrentUser != null)
            {
                // Access the UserID and other properties of App.CurrentUser
                int userId = App.CurrentUser.UserID;
                string userName = $"{App.CurrentUser.FirstName} {App.CurrentUser.LastName}";

                // Display the user ID and name
                DisplayAlert("User Information", $"User ID: {userId}\nUser Name: {userName}", "OK");
            }
            else
            {
                DisplayAlert("Error", "User information not available.", "OK");
            }
        }

        async void OnSubmitClicked(object sender, EventArgs e)
        {
            // Check if the new password field is not null or empty
            if (!string.IsNullOrEmpty(NewPassword.Text))
            {
                // Validate the new password and confirmation match
                if (NewPassword.Text == ConfirmPassword.Text)
                {
                    // Update the password for the current logged-in account
                    if (App.CurrentUser != null)
                    {
                        // Await the UpdatePasswordAsync method to get the actual boolean result
                        bool success = await DatabaseHelper.UpdatePasswordAsync(App.CurrentUser, NewPassword.Text);

                        if (success)
                        {
                            // Display the new password confirmation alert
                            await DisplayAlert("Success", $"Password successfully changed to: {NewPassword.Text}", "OK");

                            // Navigate back to the login page
                            await Navigation.PopToRootAsync();
                        }
                        else
                        {
                            // Display error message if password update fails
                            await DisplayAlert("Error", "Failed to update the password.", "OK");
                        }
                    }
                    else
                    {
                        // Handle the case where no user is logged in
                        await DisplayAlert("Error", "No user is currently logged in.", "OK");
                    }
                }
                else
                {
                    // Provide feedback to the user if passwords don't match
                    await DisplayAlert("Error", "New password and confirmation do not match.", "OK");
                }
            }
            else
            {
                // Provide feedback to the user if new password is empty
                await DisplayAlert("Error", "New password field cannot be empty.", "OK");
            }
        }

    }

}
