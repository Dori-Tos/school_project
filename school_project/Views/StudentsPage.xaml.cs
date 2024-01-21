using Microsoft.Maui.Controls;
using school_project.Classes;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Diagnostics.Tracing;
using school_project.Services;

namespace school_project.Views;

public partial class StudentsPage : ContentPage
{

    public ObservableCollection<Student> Students { get; set; }

    public int selectedID { get; set; }
    

    public StudentsPage()
	{
		InitializeComponent();

        Students = new ObservableCollection<Student>();

        ListStudents.ItemsSource = Students;

        LoadStudents();
    }
    
    private async void OnStudentSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
        { 
            return; 
        }
        if (e.SelectedItem is Student selectedStudent) 
        {
            int selectedStudentID = e.SelectedItemIndex;

            await Navigation.PushAsync(new AddEvalPage(selectedStudent, selectedStudentID)); 
        }
    }

    private async void OnBulletinButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Student selectedStudent)
        {
            await Navigation.PushAsync(new BulletinPage(selectedStudent));
        }
    }

    private void LoadStudents()
    {
        try
        {
            string relativeStudentPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "student.json");

            string jsonContent = File.ReadAllText(relativeStudentPath);
            List<Student> students = new List<Student>();
            if (jsonContent != null)
            {
                students = JsonConvert.DeserializeObject<List<Student>>(jsonContent);
                if (students.Count > 0)
                {
                    foreach (var student in students)
                    {
                        Students.Add(student);
                    }
                }
            }
    
            else
            {
            }

        }

        catch (Exception ex)
        {
            Console.WriteLine("Error loading students from JSON: {ex.Message}");
        }

    }

    private void AddNewStudentClicked(object sender, EventArgs e)
    {
        var firstname = entryFirstName.Text;
        var lastname = entryLastName.Text;

        if (firstname != null && lastname != null)
        {
            List<Evaluation> evaluations = new List<Evaluation>();

            var newStudent = new Student(firstname, lastname);
            Students.Add(newStudent);

            DataManager dataManager = new DataManager();
            dataManager.AddToJson(newStudent);


            entryFirstName.Text = string.Empty;
            entryLastName.Text = string.Empty;
        }
    }
}