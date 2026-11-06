using ExaminationS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExaminationSystem
{
  
    public class Program
    {
        private static List<Question> QuestionBank = new List<Question>();

        public static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== Examination System ===");
                Console.WriteLine("1. Doctor Mode");
                Console.WriteLine("2. Student Mode");
                Console.WriteLine("3. Exit");
                Console.Write("\nSelect an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DoctorMode();
                        break;
                    case "2":
                        StudentMode();
                        break;
                    case "3":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void DoctorMode()
        {
            Console.Clear();
            Console.WriteLine("=== Doctor Mode | Question Bank ===");
            int numQuestions = GetValidInt("How many questions do you want to add? ");

            for (int i = 1; i <= numQuestions; i++)
            {
                Console.WriteLine($"\n--- Entering Question {i} ---");
                Console.WriteLine("Types: 1. True/False | 2. Choose One | 3. Multiple Choice");
                int typeChoice = GetValidInt("Select Type (1-3): ", 1, 3);

                Console.WriteLine("Levels: 1. Easy | 2. Medium | 3. Hard");
                QuestionLevel level = (QuestionLevel)GetValidInt("Select Level (1-3): ", 1, 3);

                Console.Write("Enter Question Header: ");
                string header = Console.ReadLine();

                int marks = GetValidInt("Enter Marks: ");

                switch (typeChoice)
                {
                    case 1: 
                        Console.Write("Is the correct answer True or False? (t/f): ");
                        bool tfAnswer = Console.ReadLine().Trim().ToLower().StartsWith("t");
                        QuestionBank.Add(new TrueFalseQuestion(header, marks, level, tfAnswer));
                        break;

                    case 2: 
                        string[] choicesOne = new string[4];
                        for (int c = 0; c < 4; c++)
                        {
                            Console.Write($"Enter Choice {c + 1}: ");
                            choicesOne[c] = Console.ReadLine();
                        }
                        int correctChoice = GetValidInt("Enter correct choice number (1-4): ", 1, 4);
                        QuestionBank.Add(new ChooseOneQuestion(header, marks, level, choicesOne, correctChoice));
                        break;

                    case 3: 
                        string[] choicesMulti = new string[4];
                        for (int c = 0; c < 4; c++)
                        {
                            Console.Write($"Enter Choice {c + 1}: ");
                            choicesMulti[c] = Console.ReadLine();
                        }
                        Console.Write("Enter correct choice numbers (comma separated, e.g., 1,3): ");
                        string multiInput = Console.ReadLine();
                        List<int> correctMulti = multiInput.Split(',')
                                                           .Select(s => int.Parse(s.Trim()))
                                                           .ToList();
                        QuestionBank.Add(new MultipleChoiceQuestion(header, marks, level, choicesMulti, correctMulti));
                        break;
                }
                Console.WriteLine("Question added successfully!");
            }

            Console.WriteLine("\nFinished adding questions. Press any key to return to Main Menu.");
            Console.ReadKey();
        }

        private static void StudentMode()
        {
            Console.Clear();
            Console.WriteLine("=== Student Mode ===");

            if (QuestionBank.Count == 0)
            {
                Console.WriteLine("The question bank is empty. Please ask the Doctor to add questions first.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Exam Types: 1. Practical | 2. Final");
            ExamType examType = (ExamType)GetValidInt("Choose Exam Type (1-2): ", 1, 2);

            Console.WriteLine("Levels: 1. Easy | 2. Medium | 3. Hard");
            QuestionLevel level = (QuestionLevel)GetValidInt("Choose Level (1-3): ", 1, 3);

            
            var eligibleQuestions = QuestionBank.Where(q => q.Level == level).ToList();

            if (eligibleQuestions.Count == 0)
            {
                Console.WriteLine($"No questions found for the {level} level. Press any key to return.");
                Console.ReadKey();
                return;
            }

            
            int questionCount = examType == ExamType.Practical
                ? (int)Math.Ceiling(eligibleQuestions.Count / 2.0)
                : eligibleQuestions.Count;

            var examQuestions = eligibleQuestions.Take(questionCount).ToList();

            int totalMarksPossible = examQuestions.Sum(q => q.Marks);
            int studentMarks = 0;

            Console.Clear();
            Console.WriteLine($"Starting {examType} Exam ({level} Level)");
            Console.WriteLine("------------------------------------------");

           
            foreach (var question in examQuestions)
            {
                Console.WriteLine();
                question.Display();
                Console.Write("Your Answer: ");
                string answer = Console.ReadLine();

                studentMarks += question.CheckAnswer(answer);
            }

            Console.WriteLine("\n==========================================");
            Console.WriteLine($"Your Result: {studentMarks} / {totalMarksPossible}");
            Console.WriteLine("==========================================");
            Console.WriteLine("Press any key to return to Main Menu.");
            Console.ReadKey();
        }

        private static int GetValidInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out result) && result >= min && result <= max)
                {
                    return result;
                }
                Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
            }
        }
    }
}