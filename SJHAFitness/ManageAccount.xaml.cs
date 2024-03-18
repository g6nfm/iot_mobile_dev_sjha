using CommunityToolkit.Maui.Views;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using SJHAFitness.Views;
using SQLite;

namespace SJHAFitness;

public partial class ManageAccount : ContentPage
{
	public ManageAccount()
	{
		InitializeComponent();

        var sessions = DatabaseHelper.GetSessionsByUser(App.CurrentUser.UserID);
        firstNameLabel.Text = $"First Name: {App.CurrentUser.FirstName}";

        lastNameLabel.Text = $"Last Name: {App.CurrentUser.LastName}";

        birthdayLabel.Text = $"D.O.B: {App.CurrentUser.Birthday.Date.ToShortDateString()}";

        emailLabel.Text = $"Email: {App.CurrentUser.Email}";
    }
    private void MenuPopup(object sender, EventArgs e)
    {
        var popup = new MenuBarItems();

        this.ShowPopup(popup);
    }


    async void OnUploadPictureClicked(object sender, EventArgs e)
    {
        var result = await MediaPicker.Default.PickPhotoAsync();
        if (result != null)
        {
            var stream = await result.OpenReadAsync();
            ProfilePicture.Source = ImageSource.FromStream(() => stream);
        }
    }
    void OnChangePasswordClicked(object sender, EventArgs e)
    {
        // Toggle the visibility of the password fields
        PasswordFields.IsVisible = !PasswordFields.IsVisible;
    }

    void OnSubmitClicked(object sender, EventArgs e)
    {
        // Check if the new password field is not null or empty
        if (!string.IsNullOrEmpty(NewPassword.Text))
        {
            // Validate the new password and confirmation match
            if (NewPassword.Text == ConfirmPassword.Text)
            {
                // Check if CurrentAccount is not null
                if (DatabaseHelper.CurrentAccount != null)
                {
                    // Update the password in the database
                    bool success = DatabaseHelper.UpdatePassword(DatabaseHelper.CurrentAccount, NewPassword.Text);

                    if (success)
                    {
                        DisplayAlert("Success!", "Password updated successfully.", "OK");
                    }
                    else
                    {
                        DisplayAlert("Error", "Failed to update the password.", "OK");
                    }
                }
                else
                {
                    DisplayAlert("Error", "Current account is not in the table", "OK");
                }
            }
            else
            {
                // Provide feedback to the user
                DisplayAlert("Error", "New password and confirmation do not match.", "OK");
            }
        }
        else
        {
            // Provide feedback to the user
            DisplayAlert("Error", "New password field cannot be empty.", "OK");
        }
    }

}



