namespace SJHAFitness;

public partial class LandingPage : ContentPage
{
	public LandingPage()
	{
		InitializeComponent();
	}

    // @author Jakob (Feb 23/2024) Adding a block to move to the landing page.
    async void OnButtonTappedS(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ScanPage());
    }
}