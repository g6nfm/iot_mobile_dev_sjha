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

        private async void Login(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }

}
