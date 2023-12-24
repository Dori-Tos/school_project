namespace school_project.Views;

public partial class AddEvalPage : ContentPage
{
	public AddEvalPage()
	{
		InitializeComponent();
	}

    private async void OnStudentsButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///StudentsPage");
    }
}