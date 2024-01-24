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
using System.Reflection;
//using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
//using Windows.UI.ViewManagement;
//using static Android.Provider.MediaStore;

namespace school_project.Services
{
    public class DataManager
    {
        /*

        private List<Teacher> Teachers { get; set; } = new List<Teacher>();
        private List<Student> Students { get; set; } = new List<Student>();
        private List<Acti> Activities { get; set;  } = new List<Acti>();
        */
        // Chemin relatif depuis le répertoire de base de l'application
        

        private string relativeTeacherPath =  System.IO.Path.Combine(FileSystem.AppDataDirectory, "teacher.json");
        private string relativeStudentPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "student.json");
        private string relativeActiPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "acti.json");
        private string relativeUnexpectedPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "unexpectedObjectType.json");


        public void EnsureJsonFilesExist()
        {
            EnsureFileExistsAndNotEmpty(relativeTeacherPath);
            EnsureFileExistsAndNotEmpty(relativeStudentPath);
            EnsureFileExistsAndNotEmpty(relativeActiPath);
            EnsureFileExistsAndNotEmpty(relativeUnexpectedPath);
        }

        private void EnsureFileExistsAndNotEmpty(string filePath)
        {
            if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
            {
                // Créer le fichier s'il n'existe pas ou s'il est vide
                File.WriteAllText(filePath, "[]");
            }
        }
        public List<T> LoadListFromJson<T>()
        {
            string pathElement = SetPath(typeof(T));

            if (!File.Exists(pathElement))
            {
                return new List<T>();
            }

            string json = File.ReadAllText(pathElement);
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
        
        public List<object> LoadListFromJson(Type objectType)
        {
            string pathElement = SetPath(objectType);

            if (!File.Exists(pathElement))
            {
                return new List<object>();
            }

            string json = File.ReadAllText(pathElement);
            return JsonConvert.DeserializeObject<List<object>>(json);
        }
        
        public void SaveListToJson<T>(List<T> listElement, Type objectType)
        {
            string pathElement = SetPath(objectType);
            string updatedJson = JsonConvert.SerializeObject(listElement, Formatting.Indented);
            File.WriteAllText(pathElement, updatedJson);
        }

        public void AddToJson(object schoolElement)
        {
            // Charger la liste existante depuis le fichier JSON en utilisant le type de l'objet
            List<object> listElement = LoadListFromJson(schoolElement.GetType());

            // Ajouter l'élément à la liste
            listElement.Add(schoolElement);

            // Enregistrer la liste mise à jour dans le fichier JSON en utilisant le type de l'objet
            SaveListToJson(listElement, schoolElement.GetType());
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
            Debug.WriteLine(PathElement);
            Debug.WriteLine(typeof(Student));
            Debug.WriteLine(typeof(Teacher));

            return PathElement;   
        }
        public T GetElementById<T>(int index)
        {
            List<T> listElement = LoadListFromJson<T>();
            if (index >= 0 && index < listElement.Count)
            {
                T ToReturn = (T)listElement[index];
                return ToReturn;
            }
            return default(T);
        }
    }

    public class DataManagerStudent : DataManager
    {
        public void ResendToJson(Student student, int index)
        {
            List<Student> listStudents = LoadListFromJson<Student>();
            listStudents[index] = student;
            File.WriteAllText(SetPath(typeof(Student)), "[]");
            SaveListToJson<Student>(listStudents,typeof(Student));
        }

        public string Tombola()
        {
            List<Student> listStudents = LoadListFromJson<Student>();
            List<Acti> evalList = LoadListFromJson<Acti>();

            Random rnd = new Random();

            int studentIndex = rnd.Next(listStudents.Count);
            int randomNote = rnd.Next(20);
            int actiIndex = rnd.Next(evalList.Count);

            Acti randomActi = evalList[actiIndex];
            Student randomStudent = GetElementById<Student>(studentIndex);

            string randomStudentName = randomStudent.DisplayName;
            string randomActiName = randomActi.DisplayName;

            Cote coteRecord = new Cote(randomActi, randomNote);

            randomStudent.Add(coteRecord);
            ResendToJson(randomStudent, studentIndex);

            return $"{randomStudentName} a obtenu la note de {randomNote}/20 pour l'activité {randomActiName} ";

        }

    }
    public class DataManagerActi : DataManager
    {
        public void ResendToJson(Acti acti, int index)
        {
            List<Acti> listActi = LoadListFromJson<Acti>();
            listActi[index] = acti;
            SaveListToJson(listActi, typeof(Acti));
        }

    }

    public class DataManagerTeacher : DataManager
    {
    }

}
