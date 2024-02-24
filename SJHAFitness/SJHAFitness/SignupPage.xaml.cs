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

    // @author Jakob (Feb 23/2024) Adding a block to move to the landing page.
    async void OnButtonTappedC(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LandingPage());
    }

}

