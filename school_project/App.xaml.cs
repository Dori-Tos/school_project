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
            /*
            Acti ELEC = new Acti(5, "ANALOG", "A125", YUP);
            Acti SIGN = new Acti(4, "SIGN", "S45", YUP);
            Acti PROG = new Acti(5, "PROG", "P125", YUP);
            Acti POO = new Acti(3, "POO", "PO25", YUP);

            Cote eval = new Cote(ELEC, 16);
            Cote eval2 = new Cote(ELEC, 18);
            Cote eval3 = new Cote(ELEC, 5);
            Cote eval4 = new Cote(SIGN, 12);
            Cote eval5 = new Cote(SIGN, 13);

            YIP.Add(eval4);
            YIP.Add(eval);
            YIP.Add(eval2);
            YIP.Add(eval3);
            YIP.Add(eval5);
            */
            Debug.WriteLine(YIP.GetActivitiesList());
            Debug.WriteLine(YIP.GetNotesList());

            DataManager dataManager = new DataManager();
            DataManagerStudent dataManagerStudent = new DataManagerStudent();

            Debug.WriteLine(dataManagerStudent.Tombola());

            //dataManager.AddToJson(YIP);

            //Action button Print Bulletin
/*
            Student anonym = dataManagerStudent.GetStudentById(1);
            anonym.GetActivitiesList();
            anonym.GetNotesList();
            anonym.Average();
 */



            //Action button ADD Eval
/*
            Cote eval = new Cote(NEW,16);
            Student lambda = dataManagerStudent.GetStudentById(0);
            lambda.Add(eval);
            dataManagerStudent.ResendToJson(lambda,0);
*/
            Debug.WriteLine("Finito JSON");

            MainPage = new AppShell();
        }
    }
}
