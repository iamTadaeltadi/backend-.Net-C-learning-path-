using System;
using System.Collections.Generic; // Add this for List<T>

namespace StudentListManagement
{
    class Program
    {
        static void Main()
        {
            StudentList<Student> studentsList1 = new StudentList<Student>();
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Search Student");
                Console.WriteLine("3. Display All Students");
                Console.WriteLine("4. Save to JSON");
                Console.WriteLine("5. Load from JSON");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter student name: ");
                        string name = Console.ReadLine();
                        string trimmedName = name.ToLower().Trim();
                        if (string.IsNullOrWhiteSpace(trimmedName)){
                            Console.WriteLine("the name can't be empty");
                            continue;

                        }
                        Console.Write("Enter student age: ");
                        if (!int.TryParse(Console.ReadLine(), out int age))
                        {
                            Console.WriteLine("Invalid age input. Please enter a valid integer.");
                            continue;
                        }
                        Console.Write("Enter student role number: ");
                        if (!int.TryParse(Console.ReadLine(), out int roleNumber))
                        {
                            Console.WriteLine("Invalid role number input. Please enter a valid integer.");
                            continue;
                        }
                        Console.Write("Enter student grade: ");
                        if (!int.TryParse(Console.ReadLine(), out int grade) || grade < 0 || grade > 100)
                        {
                            Console.WriteLine("Invalid grade input. Please enter a valid integer between 0 and 100.");
                            continue;
                        }

                        Student student1 = new Student(roleNumber) { Name = trimmedName, Age = age, Grade = grade };
                        studentsList1.AddStudent(student1);
                        Console.WriteLine("Student added successfully!");
                        break;

                    case "2":
                        Console.Write("Enter a student name or role number to search: ");
                        string input = Console.ReadLine();
                        List<Student> filteredStudents = studentsList1.SearchByIdOrName(input);

                        if (filteredStudents.Count > 0)
                        {
                            Console.WriteLine("Matching Students:");
                            foreach (var student in filteredStudents)
                            {
                                Console.WriteLine(student);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No students found.");
                        }
                        break;

                    case "3":
                        Console.WriteLine("All Students:");
                        studentsList1.DisplayAllStudents();
                        break;

                    case "4":
                        Console.Write("Enter file path to save students data: ");
                        string saveFilePath = Console.ReadLine();
                        try{
                            studentsList1.SerializeToJson(saveFilePath);
                        }
                        catch(Exception ){
                            continue;
                        }
                        
                        break;

                    case "5":
                        Console.Write("Enter file path to load students data: ");
                        string loadFilePath = Console.ReadLine();
                        try
                        {
                            studentsList1.SerializeToJson(loadFilePath);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        break;

                    case "6":
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
