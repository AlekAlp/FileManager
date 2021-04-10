using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileManager
{
    class ShowList
    {
        public List<stringElement> list;
        public ShowList(string path, int sizePage, Settings set)
        {
            list = WriteToList(path, set);
            ShowInfo(path, sizePage, list, set);
            ShowLine(list, list.Count, sizePage, set);            
        }
        public static void FileInformation(int sizePage, string information)
        {
            Console.SetCursorPosition(1, 7 + sizePage);
            Console.Write(information);
        }
        static void ShowInfo(string path, int sizePage, List<stringElement> list, Settings set)
        {
            stringElement info = new stringElement();
            info.titleElement = "Название директории/файла";
            info.sizeElement = "Размер:";
            info.timeCreate = "Создан:";
            info.timeChange = "Изменён:";
            ShowElement(-1, info);

            FileInformation(set.sizePage - 16, path);

            FileInformation(set.sizePage - 2, $"Страница {(sizePage + set.sizePage) / set.sizePage} из {list.Count / (set.sizePage + 1) + 1}");

            FileInformation(set.sizePage + 2, $"<{set.backCommand}> на страницу назад, если это не первая страница");
            FileInformation(set.sizePage + 3, $"<{set.nextCommand}> на страницу вперёд, если это не последняя страница");
            FileInformation(set.sizePage + 4, $"<{set.openCommand}> [название] открыть директорию, диск, или приложение");
            FileInformation(set.sizePage + 5, $"<{set.upDirCommand}> подняться на директорию выше, если это не корневой каталог");
            FileInformation(set.sizePage + 6, $"<{set.copyCommand}> [название] копировать каталог или файл. Файлы с одинаковым названием заменяются");
            FileInformation(set.sizePage + 7, $"<{set.deleteCommand}> [название] удалить каталог или файл");
            FileInformation(set.sizePage + 8, $"<{set.exitCommand}> выход из программы, с сохранением последего каталога");            
        }
        static void ShowLine(List<stringElement> list, int size, int sizePage, Settings set)
        {
            if (size < sizePage + set.sizePage)
                for (int i = sizePage, str = 0; i < size; i++, str++)
                {
                    ShowElement(str, list[i]);
                }
            else
                for (int i = sizePage, str = 0; i < sizePage + set.sizePage; i++, str++)
                {
                    ShowElement(str, list[i]);
                }            
        }
        static void ShowElement(int line, stringElement list)
        {            
                Console.SetCursorPosition(1, 4 + line);
                Console.Write(list.titleElement);
                Console.SetCursorPosition(45, 4 + line);
                Console.Write(list.timeCreate);
                Console.SetCursorPosition(57, 4 + line);
                Console.Write(list.timeChange);
                try
                {
                if (Convert.ToInt32(list.countFiles)!=-1)
                    {
                        Console.SetCursorPosition(69, 4 + line);
                        Console.Write(list.countFiles);
                    }
                }
                catch (FormatException)
                {
                    {
                        Console.SetCursorPosition(69, 4 + line);
                        Console.Write(list.countFiles);
                    }
                }
                catch (ArgumentNullException)
                {
                    {
                        Console.SetCursorPosition(69, 4 + line);
                        Console.Write(list.countFiles);
                    }
                }
                finally
                {
                    Console.SetCursorPosition(69, 3);
                    Console.Write("Файлов:");
                }
                Console.SetCursorPosition(77, 4 + line);
                Console.Write(list.sizeElement);            
        }
        static List<stringElement> WriteLineDir(string files, List<stringElement> FileLines, Settings set)
        {
            string[] name = files.Split("\\");
            DirectoryInfo fileInf = new DirectoryInfo(files);
            DateTime shortDateCreation = fileInf.CreationTime;
            DateTime shortDateLastAccess = fileInf.LastAccessTime;
            char[] delimiterForbyteSize = { ',', '(', ')', ' ' };
            string[] byteSize;
            FileInfo fileSize = new FileInfo(files);
            if (SizeElements(fileInf.FullName, 0) != 0)
                byteSize = Convert.ToString(ByteSizeElement(0, SizeElements(fileInf.FullName, 0))).Split(delimiterForbyteSize);
            else
                try
                {
                    byteSize = Convert.ToString(ByteSizeElement(0, fileSize.Length)).Split(delimiterForbyteSize);
                }
                catch(FileNotFoundException)
                {
                    byteSize = Convert.ToString(ByteSizeElement(0, SizeElements(fileInf.FullName, 0))).Split(delimiterForbyteSize);
                }
            if(set.showHidden)
            FileLines.Add(new stringElement()
            {
                fileAddress = files,
                timeCreate = Convert.ToString(shortDateCreation.ToShortDateString()),
                timeChange = Convert.ToString(shortDateLastAccess.ToShortDateString()),
                countFiles = Convert.ToString(CountElements(fileInf.FullName, 0)),
                sizeElement = $"{byteSize[1]}.{byteSize[3]} {byteSize[5]}",
                titleElement = name[name.Length - 1],
                hiddenFile = fileInf.Attributes.HasFlag(FileAttributes.Hidden)
            });       
            else if(!set.showHidden)
                if(!fileInf.Attributes.HasFlag(FileAttributes.Hidden))
                {
                    FileLines.Add(new stringElement()
                    {
                        fileAddress = files,
                        timeCreate = Convert.ToString(shortDateCreation.ToShortDateString()),
                        timeChange = Convert.ToString(shortDateLastAccess.ToShortDateString()),
                        countFiles = Convert.ToString(CountElements(fileInf.FullName, 0)),
                        sizeElement = $"{byteSize[1]}.{byteSize[3]} {byteSize[5]}",
                        titleElement = name[name.Length - 1],
                        hiddenFile = fileInf.Attributes.HasFlag(FileAttributes.Hidden)
                    });
                }
            return FileLines;
        }
        static List<stringElement> WriteToList(string path, Settings set) //Получение данных по каталогам, затем по файлам
        {
            Console.Clear();
            List<stringElement> FileLines = new List<stringElement>();
            string[] files;            
            files = Directory.GetDirectories(path);            
            int sizeList = 0;
            for (int i = 0; i < files.Length; i++, sizeList++)
            {
                FileLines = WriteLineDir(files[i], FileLines, set);
            }
            files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++, sizeList++)
            {
                FileLines = WriteLineDir(files[i], FileLines, set);
            }
            return FileLines;
        }
        static int CountElements(string path, int count)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                DirectoryInfo[] dirs = dir.GetDirectories();
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    count++;
                }
                if (dirs.Count() != 0)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        count = CountElements(subdir.FullName, count);
                    }
                }
                return count;
            }
            catch (UnauthorizedAccessException) // Если есть защищённые файлы в каталоге, то возвращаем значение -1 и не выводим количество файлов в консоль
            {                                   
                return -1;
            }
            catch (IOException)                 // или если это обычный файл, а не каталог
            {
                return -1;
            }
        }
        static long SizeElements(string path, long count)
        {
            long fullSize = 0;
            DirectoryInfo checkRoot = new DirectoryInfo(path);
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                DirectoryInfo[] dirs = dir.GetDirectories();
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    fullSize += file.Length;
                }
                if (dirs.Count() != 0)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        fullSize += SizeElements(subdir.FullName, fullSize);
                    }
                }
                return fullSize;
            }
            catch (UnauthorizedAccessException) 
            {
                return 0;
            }
            catch (IOException)
            {                
                return 0;
            }
        }
            static (int, int, string) ByteSizeElement(int count, long fileSize)
            { 
                long fullSize = fileSize;
                int temp = 0;
                string size = "B";
                if (fullSize > 1024)
                {
                    temp = (int)(fullSize % 1024);
                    fullSize = fullSize / 1024;
                    size = "KB";
                    if (fullSize > 1024)
                    {
                        temp = (int)(fullSize % 1024);
                        fullSize = fullSize / 1024;
                        size = "MB";
                        if (fullSize > 1024)
                        {
                            temp = (int)(fullSize % 1024);
                            fullSize = fullSize / 1024;
                            size = "GB";
                        }
                    }
                }
            temp = temp / 103;
            count = Convert.ToInt32(fullSize);
                return (count, temp, size);
            }
        
    }
}
