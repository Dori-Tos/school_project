using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using school_project.Services;

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

        //public string DisplayName()
        //{
        //    return String.Format("{0} {1}", firstname, lastname);
        //}
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

        public Student(string firstname, string lastname, List<Evaluation> cours) :
            base(firstname, lastname)
        {
            this.cours = cours;
        }

        public void Add(Evaluation new_evaluation)
        {
            cours.Add(new_evaluation);
        }
        double credits = 0;

        double avg = 0;
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

        public void Bulletin()
        {
            for (int i = 0; i < cours.Count; i++)
            {
                Console.WriteLine("{0} : {1} donné par {2}, points : {3}", cours[i].activity.code, cours[i].activity.name, cours[i].activity.teacher, cours[i].Note());
            }
            Console.WriteLine("La moyenne générale est : {0}", Average());
        }
    }

}


