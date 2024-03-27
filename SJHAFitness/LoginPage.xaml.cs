
using SJHAFitness;
using YourNamespace;

namespace SJHAFitness
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage());
        }

        void OnForgotPasswordClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ForgotPasswordPage());
        }



        private async void Login(object sender, EventArgs e)
        {
            string email = emailEntry.Text;
            string password = passwordEntry.Text;

            // Basic validation
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Validation Error", "Email and password are required.", "OK");
                return;
            }

            // grab account from database
            var account = DatabaseHelper.GetAccountByEmail(email);

            if (account != null && PasswordHasher.VerifyPassword(password, account.Password))
            {
                // Set the current logged-in account
                App.CurrentUser = account;

                // verification success
                await DisplayAlert("Login Success", "You are now logged in!", "OK");
                await Navigation.PushAsync(new MainPage()); // go to mainpage
            }
            else
            {
                // failed login
                await DisplayAlert("Login Failed", "Incorrect email or password.", "OK");
            }
        }
    }
}
