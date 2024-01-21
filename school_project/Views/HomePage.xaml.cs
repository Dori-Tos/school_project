using school_project.Services;
using System.Diagnostics;
namespace school_project.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        Debug.WriteLine("JE SUIS ICI");
        DataManager datamanager = new DataManager();
        datamanager.EnsureJsonFilesExist();
    }

    private async void OnTombolaButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///TombolaPage");
    }
}