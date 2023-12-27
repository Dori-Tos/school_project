using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace school_project.Classes
{
    public class Acti
    {
        public int ECTS { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public Teacher teacher { get; set; }
        public Acti(int ECTS, string name, string code, Teacher teacher)
        {
            this.ECTS = ECTS;
            this.name = name;
            this.code = code;
            this.teacher = teacher;
        }
        public string DisplayName
        {
            get { return $"{name}"; }
        }
        public void Save() { }
        public void Delete() { }

        public void Load(string file) { }
    }
}
