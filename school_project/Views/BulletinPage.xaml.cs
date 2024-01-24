using school_project.Classes;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace school_project.Views;

public partial class BulletinPage : ContentPage
{
    private string selectedStudentName {  get; set; }

    private double selectedStudentAverage { get; set; }

    public ObservableCollection<Evaluation> Evaluations { get; set; }

    public List<string> studentActiList { get; set; }

    public List<string> studentNotesList { get; set; }

    public BulletinPage(Student student)
    {
        InitializeComponent();

        selectedStudentName = student.DisplayName;

        studentActiList = new List<string>();

        studentNotesList = new List<string>();

        LoadEvaluations(student);

        StudentDisplayName.Text = selectedStudentName;

        ListActivities.ItemsSource = studentActiList;

        ListNotes.ItemsSource = studentNotesList;

        if (selectedStudentAverage != 0)
        {
            StudentDisplayAverage.Text = selectedStudentAverage.ToString("#.00");
        }
        else
        {
            StudentDisplayAverage.Text = "The student must have notes is his bulletin to show an average.";
        }
        
    }

    private void LoadEvaluations(Student student)
    {
        studentActiList = student.GetActivitiesList();

        studentNotesList = student.GetNotesList();

        selectedStudentAverage = student.Average();
    }
}