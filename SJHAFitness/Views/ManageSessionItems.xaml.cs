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

        switch (session.Session)
        {
            case "Andrew":
                session.Image = "andrew.jpg";
                break;
            case "Hanbo":
                session.Image = "hanbo.png";
                break;
            case "Jakob":
                session.Image = "jakob.jpeg";
                break;
            case "Shawn":
                session.Image = "shawn.jpg";
                break;
        }



        DatabaseHelper.AddSessionAsync(session);

        Close(session);
    }
}