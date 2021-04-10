using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FileManager
{
    class ComComplete : Command
    {
        public int newPageSize; // Переменная для сброса на первую страницу, если открыт новый каталог
        public string newPath; // Установка нового пути, или сохранение старого, чтобы программа не вылетала с неизвестным путём
        public ComComplete()
        { }
        public ComComplete(string input,
                           List<stringElement> files,
                           string path,
                           int pageSize,
                           Settings set)
        {
            char[] delimeterForDisk = {' ', ':'}; 
            string[] disk = input.Split(delimeterForDisk);
            newPageSize = pageSize;
            for (int i = 0; i <= files.Count; i++)
            {
                if (i < files.Count)
                {
                    if (input == $"{set.openCommand} {files[i].titleElement}")
                    {
                        if (Directory.Exists(files[i].fileAddress))
                        {
                            newPath = OpenDir(path, files[i].titleElement);
                            newPageSize = 0;
                            
                        }
                        else if (File.Exists(files[i].fileAddress))
                        {
                            try
                            {
                                Process.Start(new ProcessStartInfo(files[i].fileAddress));
                                newPageSize = 0;
                                newPath = path;
                                
                            }
                            catch (Win32Exception)
                            {
                                ShowList.FileInformation(set.sizePage, "Файл не может быть открыт. Нажмите Enter чтобы продолжить");
                                Console.ReadLine();
                                newPageSize = 0;
                                newPath = path;
                                
                            }                            
                        }
                        break;
                    }
                    if (input == $"{set.copyCommand} {files[i].titleElement}")
                    {
                        if (Directory.Exists(files[i].fileAddress))
                        {
                            string copyPath = CheckDir(set.sizePage);
                            if (copyPath != null)
                            {
                                CopyDir(files[i].fileAddress, $"{copyPath}\\{files[i].titleElement}", true);
                                ShowList.FileInformation(set.sizePage, $"Каталог скопирован по адресу {copyPath}\\{files[i].titleElement}. Нажмите Enter чтобы продолжить");
                                Console.ReadLine();
                            }
                        }
                        else if (File.Exists(files[i].fileAddress))
                        {
                            string title = $"\\{files[i].titleElement}";
                            newPath = CopyElement(files[i].fileAddress, title, set.sizePage);
                            newPageSize = 0;
                        }
                        newPath = path;
                        newPageSize = 0;
                        break;
                    }
                    if (input == $"{set.deleteCommand} {files[i].titleElement}")
                    {
                        if (Directory.Exists(files[i].fileAddress))
                        {
                            ShowList.FileInformation(set.sizePage, "Вы точно хотите удалить этот каталог?(yes/no) ");
                            if (DeleteFileOrDir(Console.ReadLine()))
                            {
                                Directory.Delete(files[i].fileAddress, true);
                                ShowList.FileInformation(set.sizePage, $"Каталог <{files[i].titleElement}> удалён, включая всё содержимое. Нажмите Enter чтобы продолжить");
                                Console.ReadLine();
                            }
                        }
                        else if (File.Exists(files[i].fileAddress))
                        {
                            ShowList.FileInformation(set.sizePage, "Вы точно хотите удалить этот файл?(yes/no) ");
                            if (DeleteFileOrDir(Console.ReadLine()))
                            {
                                File.Delete(files[i].fileAddress);
                                ShowList.FileInformation(set.sizePage, $"Файл <{files[i].titleElement}> удалён. Нажмите Enter чтобы продолжить");
                                Console.ReadLine();
                            }
                        }
                        newPath = path;
                        break;
                    }
                }
                if (input == $"{set.upDirCommand}")
                {
                    newPath = UpDir(path);
                    newPageSize = 0;
                    break;
                }
                if (input == $"{set.nextCommand}")
                {
                    if (files.Count > newPageSize + set.sizePage)
                    {
                        newPageSize += set.sizePage;
                        newPath = path;
                        break;
                    }
                    newPath = path;
                    break;
                }
                if (input == $"{set.backCommand}")
                {
                    if (newPageSize != 0)
                    {
                        newPageSize -= set.sizePage;
                        newPath = path;
                        break;
                    }
                    newPath = path;
                    break;
                }
                if (disk.Length > 1) // Для перехода на другой диск
                    if (DiskTitle(disk[1]))
                        if (input == $"{set.openCommand} {disk[1]}:\\")
                        {
                            if (Directory.Exists($"{disk[1]}:\\"))
                                newPath = $"{disk[1]}:\\";
                            else
                                newPath = path;

                            newPageSize = 0;
                            break;
                        }
                newPath = path;
            }
        }
    }
}
