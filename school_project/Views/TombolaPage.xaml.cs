using school_project.Services;

namespace school_project.Views;

public partial class TombolaPage : ContentPage
{
    string TombolaText { get; set; }

	public TombolaPage()
	{
		InitializeComponent();

        DataManagerStudent dataManagerStudent = new DataManagerStudent();

        TombolaText = dataManagerStudent.Tombola();

        TombolaDisplay.Text = TombolaText;
	}

    private async void OnHomeButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///HomePage");
    }
}