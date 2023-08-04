using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TaskCategoryNameSpace;
using TaskItemNameSpace;

class CsvTaskManager
{
    private const string FilePath = "/Users/tadaelshewaregagebre/Desktop/sefr/cheru.csv";

    public List<TaskItem> ReadTasksFromFile()
    {
        try
        {
            using (StreamReader reader = new StreamReader(FilePath, Encoding.UTF8))
            {
                reader.ReadLine(); // Skip header line

                List<TaskItem> taskManager = new List<TaskItem>();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    List<string> lineList = line.Split(',').ToList();

                    TaskItem customTask = new TaskItem
                    {
                        Name = lineList[0],
                        Description = lineList[1],
                        Category = (TaskCategory)Enum.Parse(typeof(TaskCategory), lineList[2], true),
                        IsCompleted = bool.Parse(lineList[3])
                    };

                    taskManager.Add(customTask);
                }

                Console.WriteLine("Task manager has been successfully loaded from the CSV file.");
                return taskManager;
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found. Please make sure the file exists at the specified location.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error while reading the file: " + ex.Message);
        }

        return new List<TaskItem>();
    }

    public async Task WriteTasksToFile(List<TaskItem> tasks)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(FilePath, false, Encoding.UTF8))
            {
                await writer.WriteLineAsync("Name,Description,Category,IsCompleted");

                foreach (TaskItem task in tasks)
                {
                    await writer.WriteLineAsync($"{task.Name},{task.Description},{task.Category},{task.IsCompleted}");
                }
            }

            Console.WriteLine("Task manager has been successfully written to the CSV file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error while writing to the file: " + ex.Message);
        }
    }
}