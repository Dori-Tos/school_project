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

    public StudentsPage()
	{
		InitializeComponent();

        Students = new ObservableCollection<Student>();

        ListStudents.ItemsSource = Students;

        LoadStudents();
    }

    private async void OnAddEvalButtonClicked(object sender, EventArgs e)
    {
        Student selectedStudent = GetSelectedStudent();

        if (selectedStudent != null)
        {
            await Navigation.PushAsync(new AddEvalPage(selectedStudent));
        }
    }

    private async void OnBulletinButtonClicked(object sender, EventArgs e)
    {
        Student selectedStudent = GetSelectedStudent();

        if (selectedStudent != null)
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

            var students = JsonConvert.DeserializeObject<List<Student>>(jsonContent);

            foreach (var student in students)
            {
                Students.Add(student);
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine("Error loading students from JSON: {ex.Message}");
        }

    }

    private Student GetSelectedStudent()
    {
        // Implement logic to get the selected student
        // This depends on your UI design
        return null; // Placeholder
    }


    private void AddNewStudentClicked(object sender, EventArgs e)
    {
        var firstname = entryFirstName.Text;
        var lastname = entryLastName.Text;
        List<Evaluation> evaluations = new List<Evaluation>();

        var newStudent = new Student(firstname, lastname, evaluations);
        Students.Add(newStudent);

        DataManager dataManager = new DataManager();
        dataManager.AddToJson(newStudent);


        entryFirstName.Text = string.Empty;
        entryLastName.Text = string.Empty;
    }
}