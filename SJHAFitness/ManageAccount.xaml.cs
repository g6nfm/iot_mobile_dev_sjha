using CommunityToolkit.Maui.Views;
using Microsoft.Maui.ApplicationModel.Communication;
using SJHAFitness.Views;
using SQLite;

namespace SJHAFitness;

public partial class ManageAccount : ContentPage
{
	public ManageAccount()
	{
		InitializeComponent();
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
        // Validate the new password and confirmation match
        if (NewPassword.Text == ConfirmPassword.Text)
        {
            // Update the password in the database
            bool success = DatabaseHelper.UpdatePassword(DatabaseHelper.CurrentAccount, NewPassword.Text);
            if (success)
            {
                // Provide feedback to the user
                DisplayAlert("Success", "Password updated successfully.", "OK");
            }
            else
            {
                // Provide feedback to the user
                DisplayAlert("Error", "Failed to update password.", "OK");
            }
        }
        else
        {
            // Provide feedback to the user
            DisplayAlert("Error", "New password and confirmation do not match.", "OK");
        }
    }

}



