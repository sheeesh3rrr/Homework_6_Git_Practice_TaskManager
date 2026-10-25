using System;
using TaskTracker.Services;

namespace TaskTracker.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigReader config = new ConfigReader();
            TaskService taskService = new TaskService();
            FileStorage storage = new FileStorage();

            bool useArrowMenu = config.IsProductionMode();

            Console.WriteLine($"Режим: {config.GetMode()}");
            Console.WriteLine();

            ConsoleUI ui = new ConsoleUI(taskService, storage, useArrowMenu);
            ui.Run();
        }
    }
}
