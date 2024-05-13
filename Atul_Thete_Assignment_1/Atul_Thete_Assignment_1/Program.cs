using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atul_Thete_Assignment_1
{
    class Program
    {
        static List<Task> tasks = new List<Task>();

        static void Main(string[] args)
        {
            bool firstRun = true;
            do
            {
                if (!firstRun)
                {
                    Console.WriteLine("\nDo you want to continue? (yes/no)");
                    string choice = Console.ReadLine().ToLower();
                    if (choice != "yes")
                        break;
                }

                Console.WriteLine("\nTask Management Application");
                Console.WriteLine("1. Create a task");
                Console.WriteLine("2. Read tasks");
                Console.WriteLine("3. Update a task");
                Console.WriteLine("4. Delete a task");
                Console.WriteLine("5. Exit");

                Console.Write("Enter your choice: ");
                int choiceInput;
                while (!int.TryParse(Console.ReadLine(), out choiceInput) || choiceInput < 1 || choiceInput > 5)
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    Console.Write("\nEnter your choice: ");
                }

                switch (choiceInput)
                {
                    case 1:
                        CreateTask();
                        break;
                    case 2:
                        ReadTasks();
                        break;
                    case 3:
                        UpdateTask();
                        break;
                    case 4:
                        DeleteTask();
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                }

                firstRun = false;
            } while (true);
        }

        static void CreateTask()
        {
            Console.Write("\nEnter task title: ");
            string title = Console.ReadLine();
            Console.Write("Enter task description: ");
            string description = Console.ReadLine();

            Task newTask = new Task { Title = title, Description = description };
            tasks.Add(newTask);
            Console.WriteLine("\nTask created successfully!");
        }

        static void ReadTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.");
            }
            else
            {
                Console.WriteLine("\nTasks List:");
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {tasks[i].Title}");
                }

                Console.WriteLine("\nDo you want to see the description of a particular task? (yes/no)");
                string choice = Console.ReadLine().ToLower();
                if (choice == "yes")
                {
                    Console.Write("\nEnter the number of the task you want to see: ");
                    int taskNumber;
                    while (!int.TryParse(Console.ReadLine(), out taskNumber) || taskNumber < 1 || taskNumber > tasks.Count)
                    {
                        Console.WriteLine($"Invalid task number. Please enter a number between 1 and {tasks.Count}.");
                        Console.Write("Enter the number of the task you want to see: ");
                    }

                    Task selectedTask = tasks[taskNumber - 1];
                    Console.WriteLine($"Title: {selectedTask.Title}");
                    Console.WriteLine($"Description: {selectedTask.Description}");
                }
            }
        }

        static void UpdateTask()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("\nNo tasks available to update.");
                return;
            }

            Console.WriteLine("\nTasks List:");
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i].Title}");
            }

            Console.Write("\nEnter the number of the task you want to update: ");
            int taskNumber;
            while (!int.TryParse(Console.ReadLine(), out taskNumber) || taskNumber < 1 || taskNumber > tasks.Count)
            {
                Console.WriteLine($"Invalid task number. Please enter a number between 1 and {tasks.Count}.");
                Console.Write("Enter the number of the task you want to update: ");
            }

            Task selectedTask = tasks[taskNumber - 1];

            Console.Write("Enter new task title (leave blank to keep the same): ");
            string newTitle = Console.ReadLine();
            if (!string.IsNullOrEmpty(newTitle))
            {
                selectedTask.Title = newTitle;
            }

            Console.Write("Enter new task description (leave blank to keep the same): ");
            string newDescription = Console.ReadLine();
            if (!string.IsNullOrEmpty(newDescription))
            {
                selectedTask.Description = newDescription;
            }

            Console.WriteLine("\nTask updated successfully!");

        }

        static void DeleteTask()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("\nNo tasks available to delete.");
                return;
            }

            Console.WriteLine("\nTasks List:");
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i].Title}");
            }

            Console.Write("\nEnter the number of the task you want to delete: ");
            int taskNumber;
            while (!int.TryParse(Console.ReadLine(), out taskNumber) || taskNumber < 1 || taskNumber > tasks.Count)
            {
                Console.WriteLine($"Invalid task number. Please enter a number between 1 and {tasks.Count}.");
                Console.Write("Enter the number of the task you want to delete: ");
            }

            Task selectedTask = tasks[taskNumber - 1];
            tasks.Remove(selectedTask);
            Console.WriteLine("\nTask deleted successfully!");
        }
    }
}


