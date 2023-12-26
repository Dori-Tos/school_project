using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using school_project.Classes;
using school_project.Services;
using System.Diagnostics;

namespace school_project
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Debug.WriteLine("Application Started ;-) ");


            DataManager dataManager = new DataManager();


            Debug.WriteLine("Finito JSON");

            MainPage = new AppShell();
        }
    }
}
