using CommunityToolkit.Maui.Views;

namespace SJHAFitness.Views;

public partial class MenuBarItems : Popup
{
    public MenuBarItems()
    {
        InitializeComponent();
        currentUser.Text = $"{App.CurrentUser.FirstName}";
    }

    // Public method to initialize the popup asynchronously

    
    public async Task InitializeAsync()
    {
        var account = await DatabaseHelper.GetAccountByEmailAsync(App.CurrentUser.Email);
    }

    private void CloseMenu(object sender, EventArgs e)
    {
        Close();
    }

    async void QRCode(object sender, EventArgs e)
    {
        if (Application.Current?.MainPage?.Navigation != null)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new QRCode());
            Close();
        }
    }

    async void Home(object sender, EventArgs e)
    {
        if (Application.Current?.MainPage?.Navigation != null)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
            Close();
        }
    }

    async void ManageSessions(object sender, EventArgs e)
    {
        if (Application.Current?.MainPage?.Navigation != null)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ManageSessions());
            Close();
        }
    }

    async void Logout(object sender, EventArgs e)
    {
        if (Application.Current?.MainPage?.Navigation != null)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            Close();
        }
    }

    async void ManageAccount(object sender, EventArgs e)
    {
        if (Application.Current?.MainPage?.Navigation != null)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ManageAccount());
            Close();
        }
    }
}
