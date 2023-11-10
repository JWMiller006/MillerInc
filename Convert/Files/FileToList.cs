using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace MillerInc.Convert.Files
{
    public class FileToList
    {
        /// <summary>
        /// New form is File Read 
        /// Outdated since it is made obsolete with new functions
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static List<string> FileRead(string file)
        {
            List<string> output = new();
            foreach (string line in File.ReadLines(@file))
            {
                output.Add(line);
            }
            return output;
        }
        /// <summary>
        /// Not really Useful, considering that the function is just one line.
        /// However, it does get directly the file as a list of strings
        /// </summary>
        /// <param name="file"> File to read</param>
        /// <returns>List of each line of the file</returns>
        public static List<string> FileToListConverter(string file)
        {
            return File.ReadAllLines(file).ToList();
        }
    }
}
