using System;
using System.Collections.Generic;
using System.IO;

namespace fileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> listdisk = new List<string>();
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            var PathToFolder = @"g:\";
            string[] folders = Directory.GetDirectories(PathToFolder);
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
            }

            i = 0;
            foreach (var ls in listdisk)
            {
                Console.WriteLine(ls);
                i++;
                if (i == 29)
                {
                    return;
                }
            }
            /*List<string> ls = GetRecursFiles(PathToFolder);
            foreach (string fname in ls)
            {
                Console.WriteLine(fname);
            }

            List<string> GetRecursFiles(string start_path)
            {
                List<string> ls = new List<string>();
                try
                {
                    string[] folders = Directory.GetDirectories(start_path);
                    foreach (string folder in folders)
                    {
                        ls.Add("Папка: " + folder);
                        ls.AddRange(GetRecursFiles(folder));
                    }
                    string[] files = Directory.GetFiles(start_path);
                    foreach (string filename in files)
                    {
                        ls.Add("Файл: " + filename);
                    }
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return ls;
            }*/
        }
    }
}
