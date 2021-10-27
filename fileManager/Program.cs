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
            drawtree(listdisk, strCount);


            static void drawtree(List<string> listdisk, int strCount)
            {
                int fistpage = 0;
                int lastpage = 0;
                do
                {

                    if (lastpage > listdisk.Count)
                    {
                        lastpage = listdisk.Count;
                    }
                    else
                    {
                        lastpage = fistpage + strCount;
                    }    
                    int pages = listdisk.Count / strCount;
                    Console.WriteLine($"Дерево состоит из {pages} страниц. \nДля показа следующей нажмите стрелку вправо, \nдля показа предыдущей стрелку влево");
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
                    if (Console.ReadKey().Key == ConsoleKey.RightArrow)
                    {
                        fistpage = fistpage + strCount;
                        lastpage = fistpage + strCount;
                    }
                    else if (Console.ReadKey().Key == ConsoleKey.LeftArrow)
                    {
                        fistpage = fistpage - strCount;
                        lastpage = fistpage + strCount;
                    }
                } while (Console.ReadKey().Key != ConsoleKey.X);
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
        }
    }
}
