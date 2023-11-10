using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MillerInc.Files
{
    /// <summary>
    /// Includes the generic file control methods
    /// </summary>
    public class FileControl
    {

        /// <summary>
        /// Creates or Opens a new file at the specified path and copies the stream to said path
        /// </summary>
        /// <param name="stream">Stream to copy</param>
        /// <param name="filePath">FilePath to write to</param>
        public static void SaveStreamAsFile(Stream stream, string filePath)
        {
            FileStream fileStream = System.IO.File.OpenWrite(filePath);
            stream.CopyTo(fileStream);
            fileStream.Close();
        }
        public async static Task SaveStreamAsFileAsync(Stream stream, string filePath)
        {
            FileStream fileStream = System.IO.File.OpenWrite(filePath);
            await stream.CopyToAsync(fileStream);
            fileStream.Close();
            stream.Close();
        }

        /// <summary>
        /// Creates or Opens a new file at the specified path and copies the stream to said path
        /// </summary>
        /// <param name="stream">Stream to copy</param>
        /// <param name="filePath">FilePath to write to</param>
        public static void SaveStreamAsFile(FileStream stream, string filePath)
        {
            FileStream fileStream = System.IO.File.OpenWrite(filePath);
            stream.CopyTo(fileStream);
            fileStream.Close();
        }


        /// <summary>
        /// Saves the MemoryStream into a file (Meant for downloading)
        /// </summary>
        /// <param name="stream">Stream to save</param>
        /// <param name="filePath">Path to file</param>
        public static void SaveStreamAsFile(MemoryStream stream, string filePath)
        {
            FileStream file = new(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            stream.CopyTo(file);
            file.Flush();
            file.Close();
        }


        /// <summary>
        /// Saves the MemoryStream into a file (Meant for downloading)
        /// </summary>
        /// <param name="stream">Stream to save</param>
        /// <param name="filePath">Path to file</param>
        public static async Task SaveStreamAsFileAsync(MemoryStream stream, string filePath)
        {
            FileStream file = new(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            await stream.CopyToAsync(file);
            await file.FlushAsync();
            file.Close();
            stream.Close();
        }
    }
}
