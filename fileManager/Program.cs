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
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            var @PathToFolder = folder("lastFolder");
            int strCount = Int32.Parse(folder("strCount"));
            List<string> listdisk = new List<string>();
            data4draw(PathToFolder, strCount, listdisk);
            drawFullTree(listdisk, strCount);


            static void drawFullTree(List<string> listdisk, int strCount)
            {
                int fistpage = 0;
                int lastpage = strCount; // fistpage + strCount;
                ConsoleKey doing = ConsoleKey.A;
                do
                {
                    if (lastpage + strCount >= listdisk.Count)
                    {
                        lastpage = listdisk.Count;
                    }
                    else
                    {
                        lastpage = fistpage + strCount;
                    }    
                    int pages = listdisk.Count / strCount;
                    Console.WriteLine($"Дерево состоит из {pages + 1} страниц. \nДля показа следующей нажмите стрелку вправо, \nдля показа предыдущей стрелку влево");
                    drawTree(fistpage, lastpage, listdisk);
                    /*string[] disk = listdisk[0].Split("\\");
                    /*Console.WriteLine(disk[0]);
                    for (int i = fistpage; i < lastpage; i++)
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
                    Console.WriteLine("\n== Аттрибуты выбранного элемента =============================");
                    Console.WriteLine("\nДоступны команды: open\\close\\attrib\\copy\\del будете вводить? Если нет, нажмите \"с\"");*/
                    string ans = Console.ReadLine();
                    if (ans != "c")
                    {
                        //Console.WriteLine("awaiting commands");
                        //string splt = Console.ReadLine();
                        string[] cmd = cmdParse(ans);
                        //Console.WriteLine($"CMD {cmd[0]} PATH {cmd[1]}");
                        switch (cmd[0])
                        {
                            case "open": 
                                data4list(cmd[1], listdisk);
                                break;
                            case "attr": 
                                Console.WriteLine(attrib(cmd[1]));
                                Console.ReadKey();
                                break;
                            default: throw new ArgumentException("Недопустимый код операции");
                        }
                        int[] diapaz = checkLength(lastpage, fistpage, strCount, listdisk);
                        drawTree(diapaz[0], diapaz[1], listdisk);
                    }
                    Console.WriteLine($"Страница {lastpage/strCount + 1} из {pages + 1} <= | =>      Стрелка влево\\вправо для переключения страниц, пробел - остаться на странице и ввести команду");
                    doing = Console.ReadKey().Key;
                    if (doing == ConsoleKey.RightArrow && lastpage <= listdisk.Count && fistpage + strCount < lastpage)
                    {
                        fistpage = fistpage + strCount;
                        lastpage = fistpage + strCount;
                        //Console.WriteLine("right");
                    }
                    else if (doing == ConsoleKey.LeftArrow && fistpage >= strCount)
                    {
                        fistpage = fistpage - strCount;
                        lastpage = fistpage + strCount;
                        //Console.WriteLine("left");
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


        static void drawTree(int fistpage, int lastpage, List<string> listdisk)
            {
                Console.Clear();
                string[] disk = listdisk[0].Split("\\");
                Console.WriteLine(disk[0]);
                    for (int i = fistpage; i < lastpage; i++)
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
                    Console.WriteLine("\n== Аттрибуты выбранного элемента =============================");
                    Console.WriteLine("\nДоступны команды: open\\close\\attrib\\copy\\del будете вводить? Если нет, нажмите \"с\"");
            }


            static void data4list(string str, List<string> listdisk)
            {
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


            static FileAttributes attrib(string str)
            {
                FileAttributes attributes = File.GetAttributes(str);
                return attributes;
            }


            static string[] cmdParse(string str)
            {
                return str.Split(" ");
            }


            static int [] checkLength(int lastpage, int fistpage, int strCount, List<string> listdisk)
            {
                int[] result = new int[2];
                if (lastpage <= listdisk.Count && listdisk.Count <= strCount)
                {
                    result[0] = fistpage;// + strCount;
                    result[1] = listdisk.Count;
                    //Console.WriteLine("right");
                }
                else if (fistpage >= strCount)
                {
                    result[0] = fistpage - strCount;
                    result[1] = fistpage + strCount;
                    //Console.WriteLine("left");
                }
                return result;
            }
        }
    }
}
