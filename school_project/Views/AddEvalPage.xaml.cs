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

    private DataManagerStudent dataManagerStudent = new DataManagerStudent();

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
            if (evaluationInput != null && selectedApprectiationID != -1)
            {
                Confirmation.Text = "A new eval must contain a cote or an appreciation, not both.";

                pointsEntry.Text = null;

                activityPicker.SelectedItem = null;
                appreciationPicker.SelectedIndex = -1;
            }
            else
            {
                if (int.TryParse(evaluationInput, out int numericNote))
                {
                    Debug.WriteLine("COTE");
                    newEvaluation = new Cote(selectedActivity, numericNote);
                }
                if (evaluationInput == null && selectedApprectiationID != -1)
                {
                    Debug.WriteLine("APPRECIATION");
                    var selectedAppreciation = appreciations[selectedApprectiationID];

                    newEvaluation = new Appreciation(selectedActivity, selectedAppreciation);
                }


                selectedStudent.Add(newEvaluation);

                dataManagerStudent.ResendToJson(selectedStudent, selectedStudentID);

                pointsEntry.Text = null;

                activityPicker.SelectedItem = null;
                appreciationPicker.SelectedIndex = -1;

                Confirmation.Text = "Sucessfuly added the evaluation";
            }
        }
    }

    private void OnActivityPickerClicked(object sender, EventArgs e)
    {
        if (activityPicker.SelectedIndex != -1)
        {
            Confirmation.Text = string.Empty;
        }
    }
}