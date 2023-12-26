using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using school_project.Classes;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.Maui.Storage;
using System.Collections;
//using Windows.UI.ViewManagement;
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
        private string relativeUnexpectedPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "unexpectedObjectType.json");

        public void AddToJson(Object schoolElement)
        {
            // Obtenir le type de l'objet
            Type elementType = schoolElement.GetType();

            // Charger la liste existante depuis le fichier JSON
            List<object> listElement = LoadListFromJson(elementType);

            // Ajouter l'élément à la liste
            listElement.Add(schoolElement);

            // Enregistrer la liste mise à jour dans le fichier JSON
            SaveListToJson(listElement, elementType);
        }

        private List<object> LoadListFromJson(Type objectType)
        {
            string pathElement = SetPath(objectType);

            // Vérifier si le fichier existe
            if (!File.Exists(pathElement))
            {
                return new List<object>();
            }

            string json = File.ReadAllText(pathElement);
            return JsonConvert.DeserializeObject<List<object>>(json);
        }

        private void SaveListToJson(List<object> ListElement, Type objectType)
        {
            string PathElement = SetPath(objectType);
            Debug.WriteLine(PathElement);
            // Écrire la liste dans le fichier JSON
            string updatedJson = JsonConvert.SerializeObject(ListElement, Formatting.Indented);
            File.WriteAllText(PathElement, updatedJson);
        }


        protected string SetPath(Type objectType)
        {
            var PathElement = relativeUnexpectedPath;
            Type elementType = objectType;

            if (elementType == typeof(Teacher))
            {
                PathElement = relativeTeacherPath;
            }
            else if (elementType == typeof(Student))
            {
                PathElement = relativeStudentPath;
            }
            else if (elementType == typeof(Acti))
            {
                PathElement = relativeActiPath;
            }
            return PathElement;
            
        }

        

    }

    public class DataManagerStudent : DataManager
    {
        //Modified for student only to be used easily in GetStudentById()
        private List<Student> LoadListFromJson()
        {
            string pathElement = SetPath(typeof(Student));

            // Vérifier si le fichier existe
            if (!File.Exists(pathElement))
            {
                return new List<Student>();
            }

            string json = File.ReadAllText(pathElement);
            return JsonConvert.DeserializeObject<List<Student>>(json);
        }

        //Modified for student only to be used easily in ResendToJson
        private void SaveListToJson(List<Student> ListElement, Type objectType)
        {
            string PathElement = SetPath(objectType);

            // Écrire la liste dans le fichier JSON
            string updatedJson = JsonConvert.SerializeObject(ListElement, Formatting.Indented);
            File.WriteAllText(PathElement, updatedJson);
        }

        public Student GetStudentById(int index)
        {
            List<Student> listStudents = LoadListFromJson();
            if (index >= 0 && index < listStudents.Count)
            { 
                Student student = (Student)listStudents[index];
                return student;
            }
            return null;
        }

        public void ResendToJson(Student student, int index)
        {
            List<Student> listStudents = LoadListFromJson();
            listStudents[index] = student;
            SaveListToJson(listStudents, typeof(Student));
        }
        public void DeleteStudentById(int index)
        {
            List<Student> listStudents = LoadListFromJson();
            listStudents.RemoveAt(index);
        }
    }
}
