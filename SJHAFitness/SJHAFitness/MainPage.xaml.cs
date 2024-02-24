namespace SJHAFitness
{
    public partial class MainPage : ContentPage
    {
<<<<<<< Updated upstream
        int count = 0;
=======
>>>>>>> Stashed changes

        public MainPage()
        {
            InitializeComponent();
        }

<<<<<<< Updated upstream
        private void OnCounterClicked(object sender, EventArgs e)
        {
           
=======
        private async void OnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage());
>>>>>>> Stashed changes
        }
    }

}
