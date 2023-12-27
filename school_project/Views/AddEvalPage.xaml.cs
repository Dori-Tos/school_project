using school_project.Classes;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Collections.ObjectModel;
using school_project.Services;

namespace school_project.Views;

public partial class AddEvalPage : ContentPage
{
    private Student selectedStudent;

    public ObservableCollection<Acti> Activities { get; set; }

    public AddEvalPage(Student student)
	{
		InitializeComponent();

        selectedStudent = student;

        Activities = new ObservableCollection<Acti>();

        string relativeActiPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "acti.json");

        string jsonContent = File.ReadAllText(relativeActiPath);

        var activities = JsonConvert.DeserializeObject<List<Acti>>(jsonContent);        

        foreach (var activity in activities)
        {
            Activities.Add(activity);
        }

        activityPicker.ItemsSource = Activities;
    }

    private void OnAddEvaluationClicked(object sender, EventArgs e)
    {
        var evaluationInput = pointsEntry.Text;
        var selectedActivity = activityPicker.SelectedItem as Acti;

        Evaluation newEvaluation = null;

        if (int.TryParse(evaluationInput, out int numericNote))
        {
            newEvaluation = new Cote(selectedActivity, numericNote);
        }
        else
        {
            newEvaluation = new Appreciation(selectedActivity, evaluationInput);
        }

        selectedStudent.Add(newEvaluation);

        DataManagerStudent dataManagerStudent = new DataManagerStudent();
        //dataManagerStudent.ResendToJson(selectedStudent);

        pointsEntry.Text = string.Empty;

        Confirmation.Text = "Sucessfuly added the evaluation";
    }
}