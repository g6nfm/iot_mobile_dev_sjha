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
            // Retrieve the email and new password entered by the user
            string email = emailEntry.Text;
            string newPassword = newPasswordEntry.Text;

            // Validate that the email and new password fields are not empty
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(newPassword))
            {
                await DisplayAlert("Validation Error", "Email and new password are required.", "OK");
                return;
            }

            // Validate the new password and confirmation match
            if (newPasswordEntry.Text != confirmPasswordEntry.Text)
            {
                await DisplayAlert("Error", "New password and confirmation do not match.", "OK");
                return;
            }

            // Update the password in the database
            bool success = DatabaseHelper.UpdatePasswordByEmail(email, newPassword);

            if (success)
            {
                // Display the new password in an alert
                await DisplayAlert("Success!", $"Password updated successfully.\n\nNew Password: {newPasswordEntry.Text}", "OK");

                // Navigate back to the login page
                await Navigation.PopToRootAsync();
            }
            else
            {
                // Handle the case where the email is not associated with any account
                await DisplayAlert("Error", "No account associated with this email.", "OK");
            }
        }
    }
}
