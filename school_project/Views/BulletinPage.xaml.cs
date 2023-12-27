using school_project.Classes;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace school_project.Views;

public partial class BulletinPage : ContentPage
{
    private Student selectedStudent;

    private string selectedStudentName {  get; set; }

    private double selectedStudentAverage { get; set; }

    public ObservableCollection<Evaluation> Evaluations { get; set; }

    public List<string> studentActiList { get; set; }

    public List<string> studentNotesList { get; set; }

    public BulletinPage(Student student)
    {
        InitializeComponent();

        selectedStudent = student;

        selectedStudentName = selectedStudent.DisplayName;

        studentActiList = student.GetActivitiesList();

        studentNotesList = student.GetNotesList();

        //Evaluations = new ObservableCollection<Evaluation>();

        StudentDisplayName.Text = selectedStudentName;

        ListActivities.ItemsSource = studentActiList;

        ListNotes.ItemsSource = studentNotesList;

        selectedStudentAverage = selectedStudent.Average();

        StudentDisplayAverage.Text = selectedStudentAverage.ToString("#.00");

        //LoadEvaluations();
    }

    private void LoadEvaluations()
    {
        try
        {
            var evalutions = selectedStudent.cours;

            foreach (var evaluation in evalutions)
            {
                Evaluations.Add(evaluation);
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine("Error loading evaluations from JSON: {ex.Message}");
        }

    }
}