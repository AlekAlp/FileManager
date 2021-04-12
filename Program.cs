using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace FileManager
{
    class Program
    {        
        static void Main(string[] args)
        {        
            start:
            if (!File.Exists(@"Settings.json"))
            {
                //Задаём стандартные настройки если файла нет
                Settings setUp = new Settings();
                setUp.copyCommand = "copy";
                setUp.openCommand = "open";
                setUp.upDirCommand = "up";
                setUp.nextCommand = "next";
                setUp.backCommand = "back";
                setUp.deleteCommand = "delete";
                setUp.exitCommand = "exit";

                setUp.sizePage = 10;
                setUp.showHidden = false; // отображать или нет скрытые файлы
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Введите путь для старта программы");
                    setUp.lastPath = Console.ReadLine();
                    if (Directory.Exists(setUp.lastPath))
                        break;
                }
                string pathSetUp = JsonSerializer.Serialize(setUp);
                File.WriteAllText(@"Settings.json", pathSetUp);
            }
            Settings set;
                string pathSettings = File.ReadAllText(@"Settings.json");
            try
            {
                set = JsonSerializer.Deserialize<Settings>(pathSettings);
            }
            catch (JsonException)
            {
                File.Delete(@"Settings.json");
                goto start; // Прыжок для пересоздания файла, если файл json не соответствует условиям
            }
            FixWindowSize(set.sizePage);
            int pageSize = 0;
            string path = set.lastPath;
            while(true)
            {
                ShowList show = new ShowList(path, pageSize, set);
                Interface.Print(set.sizePage);
                string input = Console.ReadLine();
                if (input == set.exitCommand)
                {
                    set.lastPath = path;
                    string pathSet = JsonSerializer.Serialize(set);
                    File.WriteAllText(@"Settings.json", pathSet);
                    break;
                }
                ComComplete command = new ComComplete(input, show.list, path, pageSize, set);
                pageSize = command.newPageSize;
                path = command.newPath;                
            }
            Console.SetCursorPosition(0, 17 + set.sizePage);
        }
        //Фиксация консоли, чтобы всё приложение не поползло по экрану
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;
        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        static void FixWindowSize(int pageSize) 
        {
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }
            if(17 + pageSize < 40)
                Console.SetWindowSize(102, 17 + pageSize);
            else
                Console.SetWindowSize(102, 40);
        }        
    }    
}
