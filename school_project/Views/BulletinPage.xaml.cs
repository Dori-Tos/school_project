using school_project.Classes;
using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace school_project.Views;

public partial class BulletinPage : ContentPage
{
    private Student student;

    public BulletinPage(Student student)
    {
        InitializeComponent();
        this.student = student;
    }

    private async void OnStudentsButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///StudentsPage");
    }
}