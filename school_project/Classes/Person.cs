using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using school_project.Services;
using System.Diagnostics;

namespace school_project.Classes
{

    public class Person
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public Person(string firstname, string lastname)
        {
            this.firstname = firstname;
            this.lastname = lastname;
        }

        public string DisplayName
        {
            get { return $"{firstname} {lastname}"; }
        }
    }

    public class Teacher : Person
    {
        public int salary { get; set; }


    public Teacher(string firstname, string lastname, int salary) :
            base(firstname, lastname)
        {
            this.salary = salary;
        }

    }

    public class Student : Person
    {
        public List<Evaluation> cours = new List<Evaluation>();
        private double credits = 0;
        private double avg = 0;

        public Student(string firstname, string lastname) :
            base(firstname, lastname)
        {    
        }

        public List<Evaluation> EvalList
        {
            get { return cours; }
        }


        public void Add(Evaluation new_evaluation)
        {
            cours.Add(new_evaluation);
            Debug.WriteLine(new_evaluation.activity.name);
        }

        public double Average()
        {
            for (int i = 0; i < cours.Count; i++)
            {
                credits = credits + cours[i].activity.ECTS;
            }

            for (int i = 0; i < cours.Count; i++)
            {
                avg = avg + cours[i].Note() * (cours[i].activity.ECTS / credits);
            }
            return (avg);
        }

        public List<string> GetActivitiesList()
        {
            List<string> actiList = new List<string>();
            for (int i = 0; i < cours.Count; i++)
            {
                if (!actiList.Contains(cours[i].activity.name))
                {
                    actiList.Add(cours[i].activity.name.ToString());
                }
            }
            Debug.WriteLine("Contenu de coteList :");
            foreach (var valeur in actiList)
            {
                Debug.WriteLine(valeur);
            }
            return actiList;
        }

        public List<float> GetNotesList()
        {
            List<float> coteList = new List<float>();
            List<string> actiList = new List<string>();
            List<int> denom = new List<int>();

            for (int i = 0; i < cours.Count; i++)
            {
                if (!actiList.Contains(cours[i].activity.name))
                {
                    //Debug.WriteLine("DIFF");
                    actiList.Add(cours[i].activity.name.ToString());
                    coteList.Add(cours[i].Note());
                    denom.Add(1);
                }
                else
                {
                    //Debug.WriteLine("SAME");
                    var doublon = cours[i].activity.name;
                    int index = actiList.FindIndex(item => item == doublon);
                    coteList[index] += cours[i].Note();
                    Debug.WriteLine(cours[i].Note());
                    denom[index] += 1;
                }
            }
            coteList = coteList.Zip(denom, (num, denom) => num / denom).ToList();
            Debug.WriteLine("Contenu de coteList :");
            foreach (var valeur in coteList)
            {
                Debug.WriteLine(valeur);
            }
            return coteList;
        }
    }

}


