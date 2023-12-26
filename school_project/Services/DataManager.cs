using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using school_project.Classes;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.Maui.Storage;
//using static Android.Provider.MediaStore;

namespace school_project.Services
{
    public class DataManager
    {

        private List<Teacher> Teachers { get; set; } = new List<Teacher>();
        private List<Student> Students { get; set; } = new List<Student>();
        private List<Acti> Activities { get; set;  } = new List<Acti>();

        // Chemin relatif depuis le répertoire de base de l'application
        

        private string relativeTeacherPath =  System.IO.Path.Combine(FileSystem.AppDataDirectory, "teacher.json");
        private string relativeStudentPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "student.json");
        private string relativeActiPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "acti.json");

        private bool flagStop = false;

        public void AddToJson(Object schoolElement)
        {
            var ListElement = new List<object>();
            var PathElement = "";
            Type elementType = schoolElement.GetType();

            if (elementType == typeof(Teacher))
            {
                ListElement = Teachers.Cast<object>().ToList();
                PathElement = relativeTeacherPath;
            }
            else if (elementType == typeof(Student))
            {
                ListElement = Students.Cast<object>().ToList();
                PathElement = relativeStudentPath;
            }
            else if (elementType == typeof(Acti))
            {
                ListElement = Activities.Cast<object>().ToList();
                PathElement = relativeActiPath;
            }

            // Vérifier si le fichier existe
            if (!File.Exists(PathElement))
            {
                // Créer le fichier s'il n'existe pas
                File.WriteAllText(PathElement, "[]");
            }

            string json = File.ReadAllText(PathElement);

            ListElement = JsonConvert.DeserializeObject<List<object>>(json);

            ListElement.Add(schoolElement);

            string updatedJson = JsonConvert.SerializeObject(ListElement, Formatting.Indented);

            File.WriteAllText(PathElement, updatedJson);
            Debug.WriteLine(PathElement);
        }
    }
}
