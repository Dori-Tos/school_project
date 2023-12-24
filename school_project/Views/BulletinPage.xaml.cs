namespace school_project.Views;

public partial class BulletinPage : ContentPage
{
	public BulletinPage()
	{
		InitializeComponent();
	}

    private async void OnStudentsButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///StudentsPage");
    }
}