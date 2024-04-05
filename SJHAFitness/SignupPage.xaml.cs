using System;
using System.Reflection;


namespace SJHAFitness
{
    public partial class SignupPage : ContentPage
    {
        private Button _selectedSubscriptionButton;
        private int _selectedMembershipLength; // selected membership length

        public SignupPage()
        {
            InitializeComponent();
        }

        private Dictionary<int, string> _membershipOptions = new Dictionary<int, string>
        {
        { 1, "One Month All Access" },
        { 3, "Three Month All Access" },
        { 6, "Six Month All Access" },
        { 12,"One Year All Access" }
        };

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
            int weight;
            int height;

            if (!int.TryParse(weightEntry.Text, out weight))
            {
                await DisplayAlert("Error", "Please enter a valid weight.", "OK");
                return;
            }

            if (!int.TryParse(heightEntry.Text, out height))
            {
                await DisplayAlert("Error", "Please enter a valid height.", "OK");
                return;
            }

            if (_selectedMembershipLength == 0)
            {
                await DisplayAlert("Error", "Please select a membership length.", "OK");
                return;
            }

            string membershipName = _membershipOptions.ContainsKey(_selectedMembershipLength)
                            ? _membershipOptions[_selectedMembershipLength]
                            : "Unknown";

            DateTime membershipStartDate = DateTime.Now;

            // Calculate the end date based on the selected membership length
            DateTime membershipEndDate = membershipStartDate.AddMonths(_selectedMembershipLength);

            string hashedPassword = PasswordHasher.HashPassword(password);

            var isSignupSuccessful = DatabaseHelper.SignupUser(firstName, lastName, email, hashedPassword, height, weight, birthday, _selectedMembershipLength,
                membershipStartDate, membershipEndDate, membershipName);

            if (isSignupSuccessful)
            {
                App.CurrentUser = DatabaseHelper.GetAccountByEmail(email);
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                // Handle signup failure, such as displaying an error message to the user
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
}