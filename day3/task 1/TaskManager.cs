using TaskItemNameSpace;

namespace TaskCategoryNameSpace;



class TaskManagerApp
{
    private static List<TaskItem> TaskManager = new List<TaskItem>();
    private static  CsvTaskManager csvTaskManager = new CsvTaskManager();

    static bool UpdateTask(string name, TaskItem updatedTask)
    {
        name = name.ToLower().Replace(" ", "");
        TaskItem existingTask = TaskManager.FirstOrDefault(t => t.Name.ToLower().Replace(" ", "") == name);

        if (string.IsNullOrWhiteSpace(name) || existingTask == null)
        {
            throw new Exception("Input cannot be empty or null or not found task.");
        }

        existingTask.Name = updatedTask.Name;
        existingTask.Description = !string.IsNullOrEmpty(updatedTask.Description) ? updatedTask.Description : existingTask.Description;
        existingTask.Category = updatedTask.Category;
        existingTask.IsCompleted = updatedTask.IsCompleted;
        Console.WriteLine("Updated successfully");
        return true;
    }

     static void ShowTasksBasedOnCategory(string category)
    {
        try
        {
            TaskCategory categoryToFind = (TaskCategory)Enum.Parse(typeof(TaskCategory), category, true);
            List<TaskItem> foundTasks = TaskManager.FindAll(task => task.Category == categoryToFind);
            if (foundTasks.Count > 0)
            {
                foreach (var task in foundTasks)
                {
                    Console.WriteLine($"Name: {task.Name}, Description: {task.Description}, Category: {task.Category}, IsCompleted: {task.IsCompleted}");
                }
            }
            else
            {
                throw new Exception("No tasks found in the specified category.");
            }
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Invalid category. Please enter a valid category.");
        }
    }

    static void DisplayTaskManager()
    {
        if (TaskManager.Count > 0)
        {
            foreach (var task in TaskManager)
            {
                Console.WriteLine($"Name: {task.Name}, Description: {task.Description}, Category: {task.Category}, IsCompleted: {task.IsCompleted}");
            }
        }
        else
        {
            Console.WriteLine("No tasks found in the task manager.");
        }
    }

    static void AddOrUpdateTask(TaskItem newTask)
    {
        string taskName = newTask.Name.ToLower().Replace(" ", "");
        TaskItem existingTask = TaskManager.FirstOrDefault(t => t.Name.ToLower().Replace(" ", "") == taskName);

        if (existingTask != null)
        {
            UpdateTask(taskName, newTask);
        }
        else
        {
            TaskManager.Add(newTask);
        }
    }

    static async Task Main()
    {
        TaskManager = csvTaskManager.ReadTasksFromFile();

        while (true)
        {
            Console.WriteLine("\nTask Manager Menu:");
            Console.WriteLine("1. Add/Update Task");
            Console.WriteLine("2. Show Tasks based on Category");
            Console.WriteLine("3. Display All Tasks");
            Console.WriteLine("4. Save to CSV File");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice (1/2/3/4/5): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter task name: ");
                    string taskName = Console.ReadLine();
                    Console.Write("Enter task description: ");
                    string description = Console.ReadLine();

                    Console.Write("Enter task category (Personal, Work, Errands, Health, Other): ");
                    string categoryInput = Console.ReadLine();

                    if (!Enum.TryParse(typeof(TaskCategory), categoryInput, true, out object categoryObj))
                    {
                        Console.WriteLine("Invalid category. Setting category to Other.");
                        categoryObj = TaskCategory.Other;
                    }
                    TaskCategory category = (TaskCategory)categoryObj;

                    Console.Write("Is the task completed? (true/false): ");
                    if (!bool.TryParse(Console.ReadLine(), out bool isCompleted))
                    {
                        Console.WriteLine("Invalid input. Setting IsCompleted to false.");
                        isCompleted = false;
                    }

                    TaskItem newTask = new TaskItem
                    {
                        Name = taskName,
                        Description = description,
                        Category = category,
                        IsCompleted = isCompleted
                    };

                    try
                    {
                        AddOrUpdateTask(newTask);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "2":
                    Console.Write("Enter the category to filter tasks: ");
                    string categoryFilter = Console.ReadLine();
                    ShowTasksBasedOnCategory(categoryFilter);
                    break;
                case "3":
                    DisplayTaskManager();
                    break;
                case "4":
                    try
                    {
                        await csvTaskManager.WriteTasksToFile(TaskManager);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "5":
                    try
                    {
                        await csvTaskManager.WriteTasksToFile(TaskManager); // Save before exiting
                        Console.WriteLine("Exiting the Task Manager.");
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }
}
