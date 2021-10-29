using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;

namespace fileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(130,45);
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            var @PathToFolder = folder("lastFolder");
            int strCount = Int32.Parse(folder("strCount"));
            List<string> listdisk = new List<string>();
            data4draw(PathToFolder, strCount, listdisk);
            drawFullTree(listdisk, strCount);


            static void drawFullTree(List<string> listdisk, int strCount)
            {
                int firstLine = 0;
                int lastLine = strCount; // firstLine + strCount;
                FileAttributes fAttr = new FileAttributes();
                ConsoleKey doing = ConsoleKey.A;
                do
                {
                    if (lastLine >= listdisk.Count)
                    {
                        lastLine = listdisk.Count;
                    }
                    else
                    {
                        lastLine = firstLine + strCount;
                    }    
                    int pages = listdisk.Count / strCount;
                    
                    drawTree(firstLine, lastLine, listdisk, fAttr, pages, strCount);
                    string ans = Console.ReadLine();
                    if (ans != "c")
                    {
                        string[] cmd = cmdParse(ans);
                        switch (cmd[0])
                        {
                            case "open": 
                                openFolder(cmd[1], listdisk);
                                break;
                            case "attrib":
                                fAttr = attrib(cmd[1]);
                                //Console.ReadKey();
                                break;
                            case "close":
                                closeFolder(cmd[1], listdisk);
                                break;
                            default: throw new ArgumentException("Недопустимый код операции");
                        }
                        int[] diapaz = checkLength(lastLine, firstLine, strCount, listdisk);
                        drawTree(diapaz[0], diapaz[1], listdisk, fAttr, pages, strCount);
                    }
                    //Console.WriteLine($"Страница {lastLine/strCount + 1} из {pages + 1} <= | =>      Стрелка влево\\вправо для переключения страниц, \nпробел - остаться на странице и ввести команду. Выйти \"x\"");
                    Console.WriteLine($"Страница {lastLine / strCount} из {pages + 1} <= | =>      Стрелка влево\\вправо для переключения страниц, \nпробел - остаться на странице и ввести команду. Выйти \"x\"");
                    doing = Console.ReadKey().Key;
                    if (doing == ConsoleKey.RightArrow && lastLine <= listdisk.Count && firstLine + strCount <= lastLine)
                    {
                        firstLine = firstLine + strCount;
                        lastLine = firstLine + strCount;
                    }
                    else if (doing == ConsoleKey.LeftArrow && firstLine >= strCount)
                    {
                        firstLine = firstLine - strCount;
                        lastLine = firstLine + strCount;
                    }
                } while (doing != ConsoleKey.X);
            }

        static string folder(string str)
            {
                var last = ConfigurationManager.AppSettings[str];
                return last;
            }


        static void data4draw(string strmem, int count, List<string> listdisk)
            {
                string[] folders = Directory.GetDirectories(strmem);
                string[] files = Directory.GetFiles(strmem);
                foreach (var i in folders)
                    listdisk.Add(i);
                foreach (var i in files)
                    listdisk.Add(i);
            }


        static void drawTree(int firstLine, int lastLine, List<string> listdisk, FileAttributes fla, int pages, int strcount)
            {
                // отрисовка дерева папок и файлов
                Console.Clear();
                Console.WriteLine($"Дерево состоит из {pages + 1} страниц. \nДля показа следующей нажмите стрелку вправо, \nдля показа предыдущей стрелку влево");
                string[] disk = listdisk[0].Split("\\");
                Console.WriteLine(disk[0]);
                    for (int i = firstLine; i < lastLine; i++)
                    {
                        string[] lsspl = listdisk[i].Split("\\");
                        char[] mask = new char[lsspl.Length + 1];
                        mask[lsspl.Length] = '|';
                        for (int j = 0; j < lsspl.Length - 1; j++)
                        {
                            mask[j] = ' ';
                        }
                        string strMask = new string(mask);
                        Console.WriteLine($"{strMask}{lsspl[lsspl.Length - 1]}");
                    }
                    Console.WriteLine("\n== Аттрибуты выбранного элемента =============================\n");
                if (fla != 0)
                {
                    Console.WriteLine(fla);
                }
                    Console.WriteLine("\n== Аттрибуты выбранного элемента =============================");
                    Console.WriteLine("\nДоступны команды: open\\close\\attrib будете вводить? Если нет, нажмите \"с\" и \"enter\"");
                //Console.WriteLine($"Страница {lastLine / strcount} из {pages + 1} <= | =>      Стрелка влево\\вправо для переключения страниц, \nпробел - остаться на странице и ввести команду. Выйти \"x\"");
            }


            static void openFolder(string str, List<string> listdisk)
            {
                //вставляю данные из открытой папки в список
                int elInd = 0;
                string[] folders = Directory.GetDirectories(str);
                string[] files = Directory.GetFiles(str);
                foreach (var i in listdisk)
                {
                    if (i == str)
                    {
                        elInd = listdisk.IndexOf(i);
                    }
                }
                if ((folders.Length > 0 && folders[0] != listdisk[elInd + 1]) || (files.Length > 0 && files[0] != listdisk[elInd + 1]))
                {
                    foreach (var i in folders)
                    {
                        listdisk.Insert(elInd + 1, i);
                        elInd++;
                    }
                    foreach (var i in files)
                    {
                        listdisk.Insert(elInd + 1, i);
                        elInd++;
                    } 
                }
                
            }


            static void closeFolder(string str, List<string> listdisk)
            {
                //удаляю данные закрытой папки из списка
                int elInd = 0;
                string[] folders = Directory.GetDirectories(str);
                string[] files = Directory.GetFiles(str);
                foreach (var i in listdisk)
                {
                    if (i == str)
                    {
                        elInd = listdisk.IndexOf(i);
                        break;
                    }
                }
                int fullLength = folders.Length + files.Length;
                elInd++;
                for (int i = elInd; i < fullLength + elInd; i++)
                { 
                    listdisk.RemoveAt(elInd);
                }
            }


            static FileAttributes attrib(string str)
            {
                FileAttributes attributes = File.GetAttributes(str);
                return attributes;
            }


            static string[] cmdParse(string str)
            {
                return str.Split(" ");
            }


            static int [] checkLength(int lastLine, int firstLine, int strCount, List<string> listdisk)
            {
                // Проверяю границы и пересчитываю вывод
                int[] result = new int[2];
                if (lastLine <= listdisk.Count && listdisk.Count <= strCount)
                {
                    result[0] = firstLine;
                    result[1] = listdisk.Count;
                }
                else if (firstLine >= strCount)
                {
                    result[0] = firstLine - strCount;
                    result[1] = firstLine + strCount;
                }
                else
                {
                    result[0] = firstLine;
                    result[1] = strCount;
                }
                return result;
            }
        }
    }
}
