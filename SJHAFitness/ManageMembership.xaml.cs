using CommunityToolkit.Maui.Views;
using SJHAFitness.Models;
using SJHAFitness.Views;
using System.Collections.ObjectModel;
using SQLite;
using System.Runtime.CompilerServices;

namespace SJHAFitness;

public partial class ManageMembership : ContentPage
{
    ObservableCollection<Sessions> Session;

    public ManageMembership()
    {
       InitializeComponent();
    }

}
