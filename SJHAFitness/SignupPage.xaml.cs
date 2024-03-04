using System.Reflection;

namespace SJHAFitness;

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
        var email = emailEntry.Text;
        var password = passwordEntry.Text;
        var weight = Convert.ToInt32(weightEntry.Text);
        var height = Convert.ToInt32(heightEntry.Text);
        DateTime birthday;

        // validate + parse birthday 
        if (!DateTime.TryParse(birthdayEntry.Text, out birthday))
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

        // DatabaseHelper.SignupUser returns a boolean
        var isSignupSuccessful = DatabaseHelper.SignupUser(email, password, height, weight, birthday);

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

