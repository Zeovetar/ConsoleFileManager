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
 /*           string[] folders = Directory.GetDirectories(PathToFolder);
            int i = 0;
            listdisk.Add(PathToFolder);
            foreach (var lsUpper in folders)
            {
                if (folders[i] != $"{PathToFolder}System Volume Information")
                    {
                    string[] insfolders = Directory.GetDirectories(folders[i]);
                    string[] insfiles = Directory.GetFiles(folders[i]);
                    string[] str = folders[i].Split("\\");
                    listdisk.Add(str[1]);
                    foreach (var ls in insfolders)
                    {
                        string[] str1 = ls.Split("\\");
                        listdisk.Add($"|    {str1[2]}");
                    }
                    foreach (var ls in insfiles)
                    {
                        string[] str1 = ls.Split("\\");
                        listdisk.Add($"|    {str1[2]}");
                    }
                    }
                i++;
            }*/

            static void drawtree(List<string> listdisk, int strCount)
            {
                int i = 0;
                foreach (var ls in listdisk)
                {
                    Console.WriteLine(ls);
                    i++;
                    if (i == strCount)
                    {
                        return;
                    }
                }
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
                //var listdata = new List<string>(folders.GetLength(0));
                foreach (var i in folders)
                    listdisk.Add(i);
                foreach (var i in files)
                    listdisk.Add(i);
                /*                int i = 0;
                                listdisk.Add(strmem);
                                foreach (var lsUpper in folders)
                                {
                                    if (folders[i] != $"{strmem}System Volume Information")
                                    {
                                        string[] insfolders = Directory.GetDirectories(folders[i]);
                                        string[] insfiles = Directory.GetFiles(folders[i]);
                                        string[] str = folders[i].Split("\\");
                                        listdisk.Add(str[1]);
                                        foreach (var ls in insfolders)
                                        {
                                            string[] str1 = ls.Split("\\");
                                            listdisk.Add($"|    {str1[2]}");
                                        }
                                        foreach (var ls in insfiles)
                                        {
                                            string[] str1 = ls.Split("\\");
                                            listdisk.Add($"|    {str1[2]}");
                                        }
                                    }
                                    i++;
                                }*/
            }
        }
    }
}
