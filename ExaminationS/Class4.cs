using ExaminationSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExaminationS
{
    public class MultipleChoiceQuestion : Question
    {
        public string[] Choices { get; set; }
        public List<int> CorrectChoiceIndices { get; set; }

        public MultipleChoiceQuestion(string header, int marks, QuestionLevel level, string[] choices, List<int> correctChoiceIndices)
            : base(header, marks, level)
        {
            Choices = choices;
            CorrectChoiceIndices = correctChoiceIndices;
        }

        public override void Display()
        {
            Console.WriteLine($"[Multiple Choice | {Level} | {Marks} Marks] {Header}");
            Console.WriteLine("(Enter comma-separated numbers for your answers, e.g., 1,3)");
            for (int i = 0; i < Choices.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Choices[i]}");
            }
        }

        public override int CheckAnswer(string studentAnswer)
        {
            try
            {
                var studentChoices = studentAnswer.Split(',')
                    .Select(s => int.Parse(s.Trim()))
                    .OrderBy(x => x)
                    .ToList();

                var correctOrdered = CorrectChoiceIndices.OrderBy(x => x).ToList();

                if (studentChoices.SequenceEqual(correctOrdered))
                {
                    return Marks;
                }
            }
            catch
            {
            }
            return 0;
        }
    }
}
