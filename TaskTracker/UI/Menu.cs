using System;

namespace TaskTracker.UI
{
    internal static class Menu
    {
        public static int ProcessArrowMenu(string mainTile, string postTile, params string[] titles)
        {
            Console.Clear();
            PaintArrowMenuSkeleton(mainTile, postTile, titles);
            return ProcessArrowMovement(titles.Length);
        }

        private static void PaintArrowMenuSkeleton(string mainTile, string postTile, params string[] titles)
        {
            Console.WriteLine(mainTile + '\n');
            for (int i = 0; i < titles.Length; i++)
            {
                Console.WriteLine("  " + titles[i]);
            }
            Console.SetCursorPosition(0, 2);
            Console.Write('>');
            Console.SetCursorPosition(0, titles.Length + 3);
            Console.WriteLine(postTile);
        }

        private static int ProcessArrowMovement(int len)
        {
            int position = 1;
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                Console.SetCursorPosition(0, position + 1);
                Console.Write(' ');
                if (key.Key == ConsoleKey.UpArrow)
                    position += position > 1 ? -1 : 0;
                else if (key.Key == ConsoleKey.DownArrow)
                    position += position < len ? 1 : 0;
                else if (key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    return position;
                }
                Console.SetCursorPosition(0, position + 1);
                Console.Write('>');
                Console.SetCursorPosition(0, len + 4);
            }
        }
    }
}
