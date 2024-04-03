using System;
using System.Reflection;

namespace SJHAFitness
{
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        void OnGoBackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        async void OnLabelTappedW(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Waiver());
        }

        async void OnLabelTappedT(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TermsAndConditions());
        }

        async void OnContinueClicked(object sender, EventArgs e)
        {
            var firstName = FirstName.Text;
            var lastName = LastName.Text;
            var email = emailEntry.Text;
            var password = passwordEntry.Text;
            DateTime birthday = birthdayEntry.Date;
            // Initialize weight and height variables
            int weight;
            int height;

            // Validate and parse weight input
            if (!int.TryParse(weightEntry.Text, out weight))
            {
                // Display an alert if weight input is invalid
                await DisplayAlert("Error", "Please enter a valid weight.", "OK");
                return;
            }

            // Validate and parse height input
            if (!int.TryParse(heightEntry.Text, out height))
            {
                // Display an alert if height input is invalid
                await DisplayAlert("Error", "Please enter a valid height.", "OK");
                return;
            }

            // Validate other input fields (e.g., email, password, etc.)
            // Proceed with signup logic if all input is valid

            // Hash the password before storing it in the database
            string hashedPassword = PasswordHasher.HashPassword(password);

            // Attempt to sign up the user
            var isSignupSuccessful = DatabaseHelper.SignupUser(firstName, lastName, email, hashedPassword, height, weight, birthday);

            if (isSignupSuccessful)
            {
                // Set the current logged-in user after successful signup
                App.CurrentUser = DatabaseHelper.GetAccountByEmail(email);

                // Navigate to the ManageAccount page after successful signup
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                // If signup fails, show an error message
                //
            }
        }

        private Button _selectedSubscriptionButton = null;

        private void OnSubscriptionOptionClicked(object sender, EventArgs e)
        {
            var clickedButton = sender as Button;

            // Deselect the previously selected button if it's not the same as the clicked one
            if (_selectedSubscriptionButton != null && _selectedSubscriptionButton != clickedButton)
            {
                DeselectButton(_selectedSubscriptionButton);
            }

            // Select the clicked button if it was not already selected, otherwise deselect it
            if (_selectedSubscriptionButton == clickedButton)
            {
                // Button was already selected, so deselect it
                DeselectButton(clickedButton);
                _selectedSubscriptionButton = null; // Clear the current selection
            }
            else
            {
                // Button is now selected
                SelectButton(clickedButton);
                _selectedSubscriptionButton = clickedButton; // Set the new selection
            }
        }

        // Method to visually mark a button as selected
        private void SelectButton(Button button)
        {
            button.BackgroundColor = Color.FromArgb("#00510A"); // Or any other color to indicate selection
        }

        // Method to revert a button's visual state to deselected
        private void DeselectButton(Button button)
        {
            button.BackgroundColor = Colors.Green; // Use the default or another color to indicate deselection
        }
    }
}
