using System;

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
                await Navigation.PushAsync(new ManageAccount());
            }
            else
            {
                // If signup fails, show an error message
                // You can handle this based on your application's logic
            }
        }
    }
}
