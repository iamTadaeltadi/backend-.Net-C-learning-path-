using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class StudentList<T> where T : Student
{
    private List<T> students = new List<T>();

    public void AddStudent(T student)
    {
        student.Name = student.Name?.ToLower().Trim();
        students.Add(student);
    }

    public List<T> SearchByIdOrName(object nameOrId)
    {
       if (nameOrId is string name)
    {
        name = name.ToLower().Trim();
        return students.Where(s => s.Name.Equals(name)).ToList();
    }
    else if (nameOrId is int id)
    {
        return students.Where(s => s.RoleNumber == id).ToList();
    }
    else
    {
        Console.WriteLine("Incorrect input");
        return new List<T>(); // Return an empty list as a placeholder
    }
    }

    public void DisplayAllStudents()
    {
        foreach (var student in students)
        {
            Console.WriteLine(student);
        }
    }

    public void SerializeToJson(string filePath)
    {
        try
        {
            string json = JsonConvert.SerializeObject(students, Formatting.Indented);
            File.WriteAllText(filePath, json);
            Console.WriteLine("Data saved to JSON file successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while saving data: {ex.Message}");
            throw;
        }
    }

    public void DeserializeFromJson(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                students = JsonConvert.DeserializeObject<List<T>>(json);
                Console.WriteLine("Data loaded from JSON file successfully!");
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading data: {ex.Message}");
            throw;
        }
    }
}
