
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
            var account = await DatabaseHelper.GetAccountByEmailAsync(email);

            // After the await, account is no longer a Task<Account>, but just Account, so you can access its properties
            if (account != null
                && PasswordHasher.VerifyPassword(password, account.Password))
            {
                // Set the current logged-in account
                App.CurrentUser = account;

                // Verification success
                await DisplayAlert("Login Success", "You are now logged in!", "OK");
                await Navigation.PushAsync(new MainPage()); // Go to main page
            }
            else
            {
                // Failed login
                await DisplayAlert("Login Failed", "Incorrect email or password.", "OK");
            }
        }
    }
}
