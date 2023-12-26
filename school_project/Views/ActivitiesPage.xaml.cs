using Microsoft.Maui.Controls;
using school_project.Classes;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Diagnostics.Tracing;

namespace school_project.Views;

public partial class ActivitiesPage : ContentPage
{
    public ObservableCollection<Activity> Activities { get; set; }

    public ActivitiesPage()
	{
		InitializeComponent();

        Activities = new ObservableCollection<Activity>();

        ListActivities.ItemsSource = Activities;

        LoadActivities();
	}

    private void LoadActivities()
    {
        try
        {
            string filepath = "C:\\Private\\Folders\\Ecam\\3BE\\Programmation orientée objet\\school_files\\activities.txt";

            string jsonContent = File.ReadAllText(filepath);

            var activities = JsonConvert.DeserializeObject<List<Activity>>(jsonContent);

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

}