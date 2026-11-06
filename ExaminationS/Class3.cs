using ExaminationSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExaminationS
{
    public class ChooseOneQuestion : Question
    {
        public string[] Choices { get; set; }
        public int CorrectChoiceIndex { get; set; }

        public ChooseOneQuestion(string header, int marks, QuestionLevel level, string[] choices, int correctChoiceIndex)
            : base(header, marks, level)
        {
            Choices = choices;
            CorrectChoiceIndex = correctChoiceIndex;
        }

        public override void Display()
        {
            Console.WriteLine($"[Choose One | {Level} | {Marks} Marks] {Header}");
            for (int i = 0; i < Choices.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Choices[i]}");
            }
        }

        public override int CheckAnswer(string studentAnswer)
        {
            if (int.TryParse(studentAnswer.Trim(), out int choice) && choice == CorrectChoiceIndex)
            {
                return Marks;
            }
            return 0;
        }
    }
}
