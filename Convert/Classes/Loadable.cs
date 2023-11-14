using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading; 
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace MillerInc.Convert.Classes
{
    /// <summary>
    /// Typicallys an inherited class that is used when the class is suppoed to be loaded back
    /// </summary>
    public class Loadable
    {
        /// <summary>
        /// Saves the current stated of the class into the file specified in json format, 
        /// if the file doesn't exist, it creates it
        /// </summary>
        /// <param name="filepath">path to file to save to (including file name and extention)</param>
        public void SaveTo(string filepath)
        {
            FileInfo info = new(filepath);
            string direcoryTemp = info.DirectoryName;
            if (!Directory.Exists(direcoryTemp))
            {
                Directory.CreateDirectory(direcoryTemp); 
            }
            File.WriteAllText(filepath, JsonConvert.SerializeObject(this));
        }

        /// <summary>
        /// Saves the current state of the class on another thread into the file specified; 
        /// if the file doesn't exist, it creates a new file to store it to in json format
        /// </summary>
        /// <param name="filepath">path to the file to save to (including the file name and extention)</param>
        public void SaveToAsync(string filepath)
        {
            FileInfo info = new(filepath);
            string direcoryTemp = info.DirectoryName;
            if (!Directory.Exists(direcoryTemp))
            {
                Directory.CreateDirectory(direcoryTemp); 
            }
            Thread writer = new(() =>
            {
                File.WriteAllText(filepath, JsonConvert.SerializeObject(this)); 
            });
            writer.Start(); 
        }
    }
}
