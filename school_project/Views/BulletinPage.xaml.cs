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

    public ObservableCollection<Evaluation> Evaluations { get; set; }

    public BulletinPage(Student student)
    {
        InitializeComponent();

        selectedStudent = student;

        ListEvaluations.ItemsSource = Evaluations;

        LoadEvaluations();
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