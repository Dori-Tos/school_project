using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school_project.Classes
{
    public class Acti
    {
        public int ECTS;
        public string name;
        public string code;
        public Teacher teacher;
        public Acti(int ECTS, string name, string code, Teacher teacher)
        {
            this.ECTS = ECTS;
            this.name = name;
            this.code = code;
            this.teacher = teacher;
        }
    }
}
