using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO; 

namespace MillerInc.Files
{
    /// <summary>
    /// Contains logic to edit files by lines
    /// </summary>
    public class EditFile
    {
        /// <summary>
        /// Edit a single line that is specified
        /// </summary>
        /// <param name="file">the path to the file to edit</param>
        /// <param name="lineNum">the number of the line to edit</param>
        /// <param name="newLine">the string for the new line</param>
        public static void EditFileLine(string file, int lineNum, string newLine)
        {
            string[] arrLine = File.ReadAllLines(file);
            arrLine[lineNum] = newLine;
            File.WriteAllLines(file, arrLine);
            return;
        }

        /// <summary>
        /// Overwrites the file with the given strings 
        /// </summary>
        /// <param name="file">the path to the file in question</param>
        /// <param name="newFile">the strings for the new file</param>
        public static void EditFileAll(string file, string[] newFile)
        {
            File.WriteAllLines(file, newFile);
        }

        /// <summary>
        /// Overwrites the file with the given strings 
        /// </summary>
        /// <param name="file">the path to the file in question</param>
        /// <param name="newFile">the strings for the new file</param>
        public static void EditFileAll(string file, List<string> newFile)
        {
            File.WriteAllLines(file, newFile);
        }
        
        /// <summary>
        /// Adds the given line to the file
        /// </summary>
        /// <param name="file">path to the file to edit</param>
        /// <param name="newLine">the line to add at the end of the file</param>
        public static void FileAddLine(string file, string newLine)
        {
            string[] arrLine = File.ReadAllLines(file);
            arrLine.Append(newLine);
            //Console.WriteLine(arrLine.ToString());
            File.AppendAllText(file, Environment.NewLine + newLine);
        }
    }
}
