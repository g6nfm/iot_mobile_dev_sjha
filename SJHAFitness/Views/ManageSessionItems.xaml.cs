using CommunityToolkit.Maui.Views;
using SJHAFitness.Models;
using static SQLite.SQLite3;

namespace SJHAFitness.Views;

public partial class ManageSessionItems : Popup
{
	public ManageSessionItems()
	{
		InitializeComponent();
	}

    private void CancelButton(object sender, EventArgs e)
    {
        Close(); 
    }

    private void SaveButton(object sender, EventArgs e)
    {
        Sessions session = new Sessions
        {
            UserID = App.CurrentUser.UserID,

            Date = SessionPicker.Date,
            Time = SessionPicker2.Time,
            Session = SessionType.SelectedItem.ToString(),
            Focus = SessionFocus.Text,
        };

        if (session.Session == "Andrew")
        {
            session.Image = "andrew.jpg";
        }
        else if (session.Session == "Hanbo")
        {
            session.Image = "hanbo.png";
        }
        else if (session.Session == "Shawn")
        {
            session.Image = "shawn.jpg";
        }
        else if (session.Session == "Jakob")
        {
            session.Image = "jakob.jpg";
        }

        DatabaseHelper.AddSessionAsync(session);

        Close(session);
    }
}