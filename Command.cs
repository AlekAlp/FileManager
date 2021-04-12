using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileManager
{
    class Command
    {
        public static bool DiskTitle(string inputDisk)
        {
            string alf = "";
            for (char i = 'A'; i <= 'Z'; i++)
            {
                if (i == 'A')
                    alf += Convert.ToString(i);
                else
                    alf += Convert.ToString($".{i}");
            }
            for (char i = 'a'; i <= 'z'; i++)
            {
                alf += Convert.ToString($".{i}");
            }
            string[] disks = alf.Split(".");
            for (int i = 0; i < disks.Length; i++)
            {
                if (disks[i] == inputDisk)
                    return true;
            }
            return false;
        }
        public static string CopyElement(string path, string title, int setSizePage)
        {
            string copyPath = path;
            Console.SetCursorPosition(1, 7 + setSizePage);
            Console.WriteLine("Введите путь куда будет скопирован файл: ");
            Console.SetCursorPosition(42, 7 + setSizePage);
            try
            {
                copyPath = Console.ReadLine();
            }
            catch
            {
                return copyPath;
            }
            if (copyPath + title != path)
                File.Copy(path, copyPath + title, true);
            return copyPath; // Возврат строки, для указания директории куда скопирован файл
        }
        public static void CopyDir(string path, string copyPath, bool copySubDirs)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                DirectoryInfo[] dirs = dir.GetDirectories();
                Directory.CreateDirectory(copyPath);
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string tempPath = Path.Combine(copyPath, file.Name);
                    File.Copy(file.FullName, tempPath, true);
                }
                if (copySubDirs)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string tempPath = Path.Combine(copyPath, subdir.Name);
                        CopyDir(subdir.FullName, tempPath, copySubDirs);
                    }
                }
            }
            catch
            {
                return;
            }
        }
        public static string CheckDir(int setSizePage)
        {
            string copyPath = null;
            Console.SetCursorPosition(1, 7 + setSizePage);
            Console.Write("Путь для копирования: ");
            try
            {
                copyPath = Console.ReadLine();
            }
            catch(FormatException)
            {
                return copyPath;
            }
            return copyPath;
        }
        public static string UpDir(string path)
        {
            string newPath = null;
            DirectoryInfo checkRoot = new DirectoryInfo(path); 
            string[] upDir = path.Split("\\");
            for (int i = 0; i < upDir.Length - 1; i++)
            {

                if (upDir[i + 1] != "")
                    newPath += $"{upDir[i]}\\";
            }
            if (path != Convert.ToString(checkRoot.Root))
                try
                { 
                    return newPath; 
                }
                catch(ArgumentNullException)
                { 
                    return path; 
                }            
            return path;
        }
        public static string OpenDir(string path, string title)
        {
            string newPath = path;
            string[] directories = Directory.GetDirectories(path);
            for (int i = 0; i < directories.Length; i++)
            {

                string[] name = directories[i].Split("\\");
                if (name[name.Length - 1] == title)
                    return directories[i];
            }
            return newPath;
        }
        public static bool DeleteFileOrDir(string question)
        {
            if (question == "yes")
                return true;
            else
                return false;
        }
    }
}
