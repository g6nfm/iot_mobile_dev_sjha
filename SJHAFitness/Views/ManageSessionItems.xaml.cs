using CommunityToolkit.Maui.Views;
using SJHAFitness.Models;

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
            Date = SessionPicker.Date,
            Time = SessionPicker2.Time,
            Session = SessionType.SelectedItem.ToString(),
            Focus = SessionFocus.Text,
        };

        Close(session);
    }
}