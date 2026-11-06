using ExaminationSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExaminationS
{
    public class TrueFalseQuestion : Question
    {
        public bool CorrectAnswer { get; set; }

        public TrueFalseQuestion(string header, int marks, QuestionLevel level, bool correctAnswer)
            : base(header, marks, level)
        {
            CorrectAnswer = correctAnswer;
        }

        public override void Display()
        {
            Console.WriteLine($"[True/False | {Level} | {Marks} Marks] {Header}");
            Console.WriteLine("1. True\n2. False");
        }

        public override int CheckAnswer(string studentAnswer)
        {
            string normalized = studentAnswer.Trim().ToLower();
            bool isStudentTrue = normalized == "1" || normalized == "true";
            bool isStudentFalse = normalized == "2" || normalized == "false";

            if (isStudentTrue && CorrectAnswer) return Marks;
            if (isStudentFalse && !CorrectAnswer) return Marks;
            return 0; 
        }
    }

}
