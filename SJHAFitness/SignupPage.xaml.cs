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
            var weight = Convert.ToInt32(weightEntry.Text);
            var height = Convert.ToInt32(heightEntry.Text);
            DateTime birthday = birthdayEntry.Date;

            // validate + parse birthday 
            if (birthday == DateTime.Today)
            {
                // if invalid
                return;
            }

            // are checkboxes checked
            if (!myCheckBox1.IsChecked || !myCheckBox2.IsChecked)
            {
                // maybe show a message to the user here if boxes aren't checked.
                return;
            }

            // Hash the password before storing it in the database
            string hashedPassword = PasswordHasher.HashPassword(password);

            // DatabaseHelper.SignupUser returns a boolean
            var isSignupSuccessful = DatabaseHelper.SignupUser(firstName, lastName, email, hashedPassword, height, weight, birthday);

            if (isSignupSuccessful)
            {
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                // failed signup show error
            }
        }
    }
}