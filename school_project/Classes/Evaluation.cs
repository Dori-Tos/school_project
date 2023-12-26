﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace school_project.Classes
{
    public abstract class Evaluation
    {
        public Acti activity;
        public Evaluation(Acti activity)
        {
            this.activity = activity;
        }

        public abstract int Note();
    }

    public class Cote : Evaluation
    {
        public int note;

        public Cote(Acti activity, int note) :
            base(activity)
        {
            this.note = note;
        }

        public override int Note()
        {
            return note;
        }
        public void setNote(int new_note)
        {
            this.note = new_note;
        }
    }

    public class Appreciation : Evaluation
    {
        public string appreciation;

        public string[] letters = { "X", "TB", "B", "C", "N" };
        public int[] numbers = { 20, 16, 12, 8, 4 };

        public Appreciation(Acti activity, string appreciation) :
            base(activity)
        {
            this.appreciation = appreciation;
        }
        int res = 0;
        public override int Note()
        {
            for (int i = 0; i < letters.Length; i++)
            {
                if (letters[i] == appreciation)
                    res = numbers[i];
            }
            return res;
        }

        public void setAppreciation(string new_appreciation)
        {
            this.appreciation = new_appreciation;
        }
    }
}


