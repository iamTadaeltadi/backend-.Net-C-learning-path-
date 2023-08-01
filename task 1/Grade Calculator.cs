using System;
using System.Collections.Generic;

class Student
{
    public string subject;
    public int grade;
    public Dictionary<string, int> myDictionary = new Dictionary<string, int>();

    public Student(string subject, int grade)
    {
        this.subject = subject;
        this.grade = grade;
    }

    public bool AddSubjectGrade(string subject, int grade)
    {
        if (grade >= 0 && grade <= 100)
        {
            myDictionary[subject] = grade;
            return true;
        }
        else
        {
            Console.WriteLine("Invalid grade! Grade should be between 0 and 100.");
            return false;

        }
    }

    public double calAvg(Dictionary<string, int> allStatus)
    {
        int tot = 0;

        foreach (KeyValuePair<string, int> sub in allStatus)
        {
            tot += sub.Value;
        }
        return tot / (double)allStatus.Count; 
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter Your Name");
        String name = Console.ReadLine();

        Console.WriteLine("Enter how many subject you are taking");
        string subjectCountInput = Console.ReadLine();
        int subjectCount = int.Parse(subjectCountInput);

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Invalid name! Please provide a valid name.");
            return; 
        }

        Student student = new Student(name, subjectCount);
        while (subjectCount > 0)
        {
            Console.WriteLine("Enter Your Subject");
            String subject = Console.ReadLine();
            Console.WriteLine("Enter Your Grade");
            String grade = Console.ReadLine();
            int gradeInt = int.Parse(grade);

            if (student.AddSubjectGrade(subject, gradeInt))
                subjectCount--;

        }

        foreach (KeyValuePair<string, int> sub in student.myDictionary)
        {
            Console.WriteLine($"{name} Your subject is {sub.Key} and your grade of this '{sub.Key}' subject is '{sub.Value}' out of 100");
        }

        double average = student.calAvg(student.myDictionary);
        Console.WriteLine($"Your average is '{average}'");
    }
}

