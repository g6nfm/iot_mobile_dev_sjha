using CommunityToolkit.Maui.Views;
using Microsoft.Maui.ApplicationModel.Communication;
using SJHAFitness.Models;
using System;
using System.Reflection;
using static SQLite.SQLite3;

namespace SJHAFitness.Views;

public partial class ManageMembershipItems : Popup
{
    private Button _selectedSubscriptionButton;
    private int _selectedMembershipLength; // selected membership length

    private Dictionary<int, string> _membershipOptions = new Dictionary<int, string>
        {
        { 1, "One Month All Access" },
        { 3, "Three Month All Access" },
        { 6, "Six Month All Access" },
        { 12,"One Year All Access" }
        };

    public ManageMembershipItems()
    {
        InitializeComponent();
    }

    private void CancelButton(object sender, EventArgs e)
    {
        Close();
    }

    async private void SaveButton(object sender, EventArgs e)
    {
        string membershipName = _membershipOptions.ContainsKey(_selectedMembershipLength)
                        ? _membershipOptions[_selectedMembershipLength]
                        : "Unknown";

        DateTime membershipStartDate = DateTime.Now;

        // Calculate the end date based on the selected membership length
        DateTime membershipEndDate = membershipStartDate.AddMonths(_selectedMembershipLength);

        // Get the current user's ID
        int userId = App.CurrentUser.UserID;

        // Update the membership in the SQLite database
        bool isUpdated = await DatabaseHelper.ChangeMembershipAsync(userId, _selectedMembershipLength, membershipName);

        if (isUpdated)
        {
            // If the update was successful, close the popup
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            Close();
        }
        else
        {
            // Handle the case where the update was not successful
            // For example, show an error message to the user
        }
    }


    private void OnSubscriptionOptionClicked(object sender, EventArgs e)
    {
        var clickedButton = sender as Button;
        if (clickedButton != null)
        {
            int membershipLength = Convert.ToInt32(clickedButton.CommandParameter);

            if (_selectedSubscriptionButton != clickedButton)
            {
                if (_selectedSubscriptionButton != null)
                {
                    DeselectButton(_selectedSubscriptionButton);
                }
                SelectButton(clickedButton);
                _selectedSubscriptionButton = clickedButton;
                _selectedMembershipLength = membershipLength;
            }
            else
            {
                DeselectButton(clickedButton);
                _selectedSubscriptionButton = null;
                _selectedMembershipLength = 0;
            }
        }
    }

    private void SelectButton(Button button)
    {
        button.BackgroundColor = Color.FromArgb("#00510A");
        // Other visual changes to indicate selection, if needed
    }

    private void DeselectButton(Button button)
    {
        button.BackgroundColor = Colors.LimeGreen;
        // Revert any visual changes that indicate selection, if needed
    }
}