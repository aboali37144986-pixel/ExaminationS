using ExaminationSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExaminationS
{
    public abstract class Question
    {
        public string Header { get; set; }
        public int Marks { get; set; }
        public QuestionLevel Level { get; set; }

        protected Question(string header, int marks, QuestionLevel level)
        {
            Header = header;
            Marks = marks;
            Level = level;
        }

        public abstract void Display();
        public abstract int CheckAnswer(string studentAnswer);
    }

}
