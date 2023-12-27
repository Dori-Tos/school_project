using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using school_project.Classes;
using school_project.Services;
using System.Collections.ObjectModel;
using System.IO;

namespace school_project.Views;

public partial class TeachersPage : ContentPage
{
    public ObservableCollection<Teacher> Teachers { get; set; }


    public TeachersPage()
    {
        InitializeComponent();

        Teachers = new ObservableCollection<Teacher>();

        ListTeachers.ItemsSource = Teachers;

        LoadTeachers();
    }

    private void LoadTeachers()
    {
        try
        {

            string relativeTeacherPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "teacher.json");

            string jsonContent = File.ReadAllText(relativeTeacherPath);

            var teachers = JsonConvert.DeserializeObject<List<Teacher>>(jsonContent);

            foreach (var teacher in teachers)
            {
                Teachers.Add(teacher);
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine("Error loading teachers from JSON: {ex.Message}");
        }

    }

    private void AddNewTeacherClicked(object sender, EventArgs e)
    {
        var firstname = entryFirstName.Text;
        var lastname = entryLastName.Text;
        var salary = int.TryParse(entrySalary.Text, out int parsedSalary) ? parsedSalary : 0;

        if (firstname != null && lastname != null)
        {
            var newTeacher = new Teacher(firstname, lastname, salary);
            Teachers.Add(newTeacher);

            DataManager dataManager = new DataManager();
            dataManager.AddToJson(newTeacher);

            entryFirstName.Text = string.Empty;
            entryLastName.Text = string.Empty;
            entrySalary.Text = string.Empty;
        }
    }
}