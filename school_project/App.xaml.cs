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

        

            Teacher YUP = new Teacher("Elise", "Cocle", 2880);
            Acti Elec = new Acti(4, "Elec Num", "A456", YUP);

            dataManager.AddToJson(YUP);
            dataManager.AddToJson(Elec);


            Debug.WriteLine("Finito JSON");

            MainPage = new AppShell();
        }
    }
}
