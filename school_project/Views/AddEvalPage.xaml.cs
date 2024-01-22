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

    private int selectedStudentID;

    private List<Acti> activities;

    private List<string> appreciations;

    public ObservableCollection<string> Activities { get; set; }

    public ObservableCollection<string> Appreciations { get; set; }

    public AddEvalPage(Student student, int studentID)
	{
		InitializeComponent();

        selectedStudent = student;
        selectedStudentID = studentID;

        Activities = new ObservableCollection<string>();
        Appreciations = new ObservableCollection<string>();

        string relativeActiPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "acti.json");

        string jsonContent = File.ReadAllText(relativeActiPath);

        activities = JsonConvert.DeserializeObject<List<Acti>>(jsonContent);        

        foreach (var activity in activities)
        {
            Activities.Add(activity.DisplayName);
        }

        activityPicker.ItemsSource = Activities;

        appreciations = new List<string> { "N", "C", "B", "TB", "X" };

        Appreciations.Add("N");
        Appreciations.Add("C");
        Appreciations.Add("B");
        Appreciations.Add("TB");
        Appreciations.Add("X");

        appreciationPicker.ItemsSource = Appreciations;
    }

    private void OnAddEvaluationClicked(object sender, EventArgs e)
    {
        var evaluationInput = pointsEntry.Text;
        var selectedActivityID = activityPicker.SelectedIndex;
        var selectedApprectiationID = appreciationPicker.SelectedIndex;

        var selectedActivity = activities[selectedActivityID];

        Evaluation newEvaluation = new Evaluation(null);

        if (selectedActivity != null)
        {
            if (int.TryParse(evaluationInput, out int numericNote))
            {
                newEvaluation = new Cote(selectedActivity, numericNote);
            }
            if (evaluationInput == null && selectedApprectiationID != -1)
            {
                var selectedAppreciation = appreciations[selectedApprectiationID];

                newEvaluation = new Appreciation(selectedActivity, selectedAppreciation);
            }

            selectedStudent.Add(newEvaluation);

            DataManagerStudent dataManagerStudent = new DataManagerStudent();
            dataManagerStudent.ResendToJson(selectedStudent, selectedStudentID);

            pointsEntry.Text = string.Empty;

            activityPicker.SelectedItem = null;
            appreciationPicker.SelectedItem = null;

            Confirmation.Text = "Sucessfuly added the evaluation";
        }
    }

    private void OnActivityPickerClicked(object sender, EventArgs e)
    {
        Confirmation.Text = null;
    }
}