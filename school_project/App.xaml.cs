using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using school_project.Classes;
using school_project.Services;
using System;
using System.Diagnostics;
//using Windows.Web.UI;

namespace school_project
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Debug.WriteLine("Application Started ;-) ");

            Student YIP = new Student("Arthur", "Van Belle");
            Student YGG = new Student("Dorian", "Smaelen");
            Teacher YUP = new Teacher("Quentin", "Lurkin", 1500);

            Acti ELEC = new Acti(5, "ANAL", "A125", YUP);
            Acti SIGN = new Acti(4, "POO", "S45", YUP);
            Acti NEW = new Acti(5, "OLD", "OLD", YUP);

            DataManager dataManager = new DataManager();
            DataManagerStudent dataManagerStudent = new DataManagerStudent();

            //dataManager.AddToJson(YIP);
            //dataManager.AddToJson(YGG);

            //Action button ADD
            Cote eval = new Cote(NEW,16);
            Student antony = dataManagerStudent.GetStudentById(1);
            antony.Add(eval);
            dataManager.AddToJson(antony);


            Debug.WriteLine("Finito JSON");

            MainPage = new AppShell();
        }
    }
}
