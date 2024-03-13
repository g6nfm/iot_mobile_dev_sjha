using CommunityToolkit.Maui.Views;
using SJHAFitness.Views;


namespace SJHAFitness.Views;

public partial class ManageAccount : ContentPage
{
    public ManageAccount()
    {
        InitializeComponent();
    }

    private void MenuPopup(object sender, EventArgs e)
    {
        var popup = new MenuBarItems();

        this.ShowPopup(popup);
    }
}
