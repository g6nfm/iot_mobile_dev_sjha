using Microsoft.Maui.Controls;
using System;

namespace YourNamespace
{
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            // Check if the new password field is not null or empty
            if (!string.IsNullOrEmpty(newPasswordEntry.Text))
            {
                // Validate the new password and confirmation match
                if (newPasswordEntry.Text == confirmPasswordEntry.Text)
                {
                    // Update the password for the account associated with the email
                    var account = DatabaseHelper.GetAccountByEmail(emailEntry.Text);
                    if (account != null)
                    {
                        // Update the password in the database
                        bool success = DatabaseHelper.UpdatePassword(account, newPasswordEntry.Text);

                        if (success)
                        {
                            // Display the new password in an alert
                            await DisplayAlert("Success!", $"Password updated successfully.\n\nNew Password: {newPasswordEntry.Text}", "OK");

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
