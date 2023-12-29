using Microsoft.Maui.Controls;
using school_project.Classes;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Diagnostics.Tracing;
using school_project.Services;

namespace school_project.Views;

public partial class ActivitiesPage : ContentPage
{
    public ObservableCollection<Acti> Activities { get; set; }

    public ObservableCollection<string> Teachers { get; set; }

    private List<Teacher> teachers;

    public ActivitiesPage()
	{
		InitializeComponent();

        Activities = new ObservableCollection<Acti>();

        Teachers = new ObservableCollection<string>();

        teachers = new List<Teacher>();

        ListActivities.ItemsSource = Activities;

        TeacherPicker.ItemsSource = Teachers;

        LoadActivities();

        LoadTeachers();
	}

    private void LoadActivities()
    {
        try
        {
            string relativeActiPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "acti.json");

            string jsonContent = File.ReadAllText(relativeActiPath);

            var activities = JsonConvert.DeserializeObject<List<Acti>>(jsonContent);

            foreach (var activity in activities)
            {
                Activities.Add(activity);
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine("Error loading activities from JSON: {ex.Message}");
        }

    }

    private void LoadTeachers()
    {
        try
        {
            string relativeTeacherPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "teacher.json");

            string jsonContent = File.ReadAllText(relativeTeacherPath);

            teachers = JsonConvert.DeserializeObject<List<Teacher>>(jsonContent);

            foreach (var teacher in teachers)
            {
                Teachers.Add(teacher.DisplayName);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading teachers from JSON: {ex.Message}");
        }
    }

    private void AddNewActivityClicked(object sender, EventArgs e)
    {
        var ECTS = int.TryParse(entryECTS.Text, out int parsedECTS) ? parsedECTS : 0;
        var name = entryName.Text;
        var code = entryCode.Text;
        var selectedTeacherID = TeacherPicker.SelectedIndex;

        Teacher selectedTeacher = teachers[selectedTeacherID];

        if (name != null && code != null)
        {
            Acti newActi = new Acti(ECTS, name, code, selectedTeacher);
            Activities.Add(newActi);

            DataManager dataManager = new DataManager();
            dataManager.AddToJson(newActi);

            entryECTS.Text = string.Empty;
            entryName.Text = string.Empty;
            entryCode.Text = string.Empty;
        }
    }

}