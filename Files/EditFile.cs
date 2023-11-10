using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO; 

namespace MillerInc.Files
{
    public class EditFile
    {
        public static void EditFileLine(string file, int lineNum, string newLine)
        {
            string[] arrLine = File.ReadAllLines(file);
            arrLine[lineNum] = newLine;
            File.WriteAllLines(file, arrLine);
            return;
        }
        public static void EditFileAll(string file, string[] newFile)
        {
            File.WriteAllLines(file, newFile);
        }
        public static void FileAddLine(string file, string newLine)
        {
            string[] arrLine = File.ReadAllLines(file);
            arrLine.Append(newLine);
            //Console.WriteLine(arrLine.ToString());
            File.AppendAllText(file, Environment.NewLine + newLine);
        }
    }
}
