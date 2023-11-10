using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json; 

namespace MillerInc.Convert.Files
{
    /// <summary>
    /// Contains logic to convert .json files to a usable class
    /// </summary>
    public class JSON_Converter
    {
        /// <summary>
        /// Converts a .json file from text to a class
        /// </summary>
        /// <typeparam name="T">The output type that you are wanting</typeparam>
        /// <param name="filePath">The path to the file that 
        /// you are wanting to convert</param>
        /// <returns></returns>
        public static T Deserialize<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);
            if (text == null)
            {
                return (T)(new object());
            }
            var temp = JsonConvert.DeserializeObject<T>(text);
            temp ??= (T)new object();
            return temp;
        }
    }
}
