using MillerInc.Convert.Lists;
using MillerInc.Convert.Strings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MillerInc.UI
{
    public class FileUI
    {
        // Gets the user to get file path through directories
        public static string GetFilePathUI()
        {
            List<string> path = new();
            bool search;
            string input = null;
            string fullPath;
            Console.WriteLine("Enter Drive Letter: ");
            List<DriveInfo> drives = DriveInfo.GetDrives().ToList();
            List<string> driveList = new();
            foreach (DriveInfo drive in drives)
            {
                driveList.Add(drive.Name);
            }
            Console.WriteLine("\nChoose a drive: \n" + ListToString.ListString(driveList, " \n", "\n"));
            path.Add(GetDriveUI());
            search = true;
            path.Add(input);
            fullPath = path[0];
            Console.WriteLine("\nChoose file or path: \n" +
                ListToString.ListString(GetDirectory(fullPath), " \n", " \n"));
            Console.WriteLine("Enter \"back\" go back a directory");
            while (search)
            {
                try
                {
                    input = Console.ReadLine();
                    if ((input.ToLower() == "back") || (input == " "))
                    {
                        path.RemoveAt(path.Count - 1);
                        fullPath = path[path.Count - 1];
                    }
                    else if (IsIn(input, GetDirec(fullPath)))
                    {
                        Console.WriteLine("Are you sure? ");
                        if (StringToBoolean.GetBool(Console.ReadLine()))
                        {
                            input = input.Replace("\\", "");
                            path.Add(path[path.Count - 1] + "\\" + input);
                            fullPath += "\\" + input;
                        }
                    }
                    else if (IsIn(input, GetFile(fullPath)))
                    {
                        Console.WriteLine("Are you sure? ");
                        if (StringToBoolean.GetBool(Console.ReadLine()))
                        {
                            input = input.Replace("\\", "");
                            path.Add(path[path.Count - 1] + "\\" + input);
                            fullPath += "\\" + input;
                            break;
                        }
                    }
                    else
                    {
                        List<string> temp = GetDirec(fullPath);
                        foreach (string s in GetFile(fullPath))
                        {
                            temp.Add(s);
                        }
                        Console.WriteLine(ListToString.ListString(temp, "\n", "\n"));
                    }
                    Console.WriteLine("\nChoose file or path: \n" +
                        ListToString.ListString(GetDirectory(fullPath), " \n", "\n"));
                    Console.WriteLine("Enter \"back\" go back a directory");
                }
                catch (ArgumentException)
                {
                    path.Clear();
                    Console.WriteLine("\nChoose file or path: \n" +
                        ListToString.ListString(GetDirectory(fullPath), " \n", "\n"));
                    Console.WriteLine("Enter \"back\" go back a directory");
                    path.Add(GetDriveUI());
                    fullPath = path[0];
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error Occured...");
                    Console.WriteLine(e.ToString());
                    Console.WriteLine("File Path Current: " + fullPath);
                }
            }
            return fullPath;
        }
        public static bool IsIn<T>(T ele, List<T> values)
        {
            foreach (T value in values)
            {
                if (ele.Equals(value)) return true;
            }
            return false;
        }
        public static List<string> GetDirectory(string path)
        {
            List<string> tempDirectory = Directory.GetDirectories(path).ToList();
            List<string> files = new();
            List<string> tempDirec = Directory.GetFiles(path).ToList();
            List<string> output = new();
            string sub;
            foreach (string t in tempDirec)
            {
                /*start = t.IndexOf(pathList[pathList.Count() - 2]);
                sub = t.Substring(start + 1);*/
                sub = t.Remove(0, path.Length);
                files.Add(sub);
            }
            foreach (string t in tempDirectory)
            {/*
                start = t.IndexOf(pathList[pathList.Count() - 2]);
                sub = t.Substring(start + 1);*/
                sub = t.Remove(0, path.Length);
                output.Add(sub);
            }
            foreach (string f in files)
            {
                output.Add(f);
            }
            return output;
        }
        public static string GetDriveUI()
        {
            List<DriveInfo> drives = DriveInfo.GetDrives().ToList();
            List<string> driveList = new();
            foreach (DriveInfo drive in drives)
            {
                driveList.Add(drive.Name);
            }
            string input = null;
            bool search = true;
            while (search)
            {
                input = Console.ReadLine();
                if (IsIn(input, driveList))
                {
                    Console.WriteLine("Are you sure?");
                    if (StringToBoolean.GetBool(Console.ReadLine()))
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("The drive you entered does not exist, try again");
                }
                Console.WriteLine("Choose a drive: \n" + ListToString.ListString(driveList, " \n", "\n"));
            }
            return input;
        }
        public static List<string> GetFile(string path)
        {
            List<string> files = new();
            List<string> tempDirec = Directory.GetFiles(path).ToList();
            string sub;
            foreach (string t in tempDirec)
            {
                sub = t.Remove(0, path.Length);
                files.Add(sub);
            }
            return files;
        }
        public static List<string> GetDirec(string path)
        {
            List<string> direc = new();
            List<string> tempDirec = Directory.GetDirectories(path).ToList();
            string sub;
            foreach (string t in tempDirec)
            {
                sub = t.Remove(0, path.Length);
                direc.Add(sub);
            }
            return direc;
        }
    }
}
