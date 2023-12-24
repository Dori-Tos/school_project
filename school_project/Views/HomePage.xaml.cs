namespace school_project.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private async void OnTombolaButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///TombolaPage");
    }
}