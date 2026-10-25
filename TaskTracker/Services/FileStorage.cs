using System;
using System.Collections.Generic;
using System.IO;
using TaskTracker.Models;

namespace TaskTracker.Services
{
    public class FileStorage
    {
        private string filePath;

        public FileStorage()
        {
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "tasks.txt");
        }

        public void SaveTasks(List<TaskItem> tasks)
        {
            List<string> lines = new List<string>();
            for (int i = 0; i < tasks.Count; i++)
            {
                TaskItem t = tasks[i];
                string line = $"{t.Id}|{t.Title}|{t.Description}|{t.Status}|{t.Priority}";
                lines.Add(line);
            }
            File.WriteAllLines(filePath, lines);
        }

        public List<TaskItem> LoadTasks()
        {
            List<TaskItem> tasks = new List<TaskItem>();

            if (!File.Exists(filePath))
            {
                return tasks;
            }

            string[] lines = File.ReadAllLines(filePath);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (line.Length == 0) continue;

                string[] parts = line.Split('|');
                if (parts.Length < 5) continue;

                int id = int.Parse(parts[0]);
                string title = parts[1];
                string description = parts[2];

                TaskItem task = new TaskItem(id, title, description);
                task.Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), parts[3]);
                task.Priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), parts[4]);
                tasks.Add(task);
            }

            return tasks;
        }
    }
}
