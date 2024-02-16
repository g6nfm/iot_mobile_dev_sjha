namespace SJHA1;

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
}