using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MillerInc.UI.OutputFile
{
    /// <summary>
    /// Class that contains the logic to output to files like one would a console
    /// </summary>
    public class Output
    {
        /// <summary>
        /// Creates new Output, nothing Pre-Declared
        /// </summary>
        public Output()
        {

        }
        /// <summary>
        /// Creates a new Output with filepath pre-declared
        /// </summary>
        /// <param name="filePath"></param>
        public Output(string filePath)
        {
            FilePath = filePath;
        }

        private string FilePath { get; set; }
        /// <summary>
        /// Changes the file path of the document, if it doesn't exist, creates new file ouput
        /// </summary>
        /// <param name="path"></param>
        public void ChangeFilePath(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.ReadAllText(path);
                    FilePath = path;
                }
                else
                {
                    File.Create(path);
                }
            }
            catch (FileNotFoundException)
            {
                File.Create(path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Basic Functions, Used once Initialized
        /// <summary>
        /// Writes the value into the file, does not return line
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Write(string value)
        {
            if (FilePath != null)
            {
                StreamWriter file = File.AppendText(FilePath);
                file.Write(value);
                file.Close();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        /// <summary>
        /// Writes the entered value into the file and returns a line
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void WriteLine(string value)
        {
            if (FilePath != null)
            {
                StreamWriter file = File.AppendText(FilePath);
                file.WriteLine(value);
                file.Close();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        /// <summary>
        /// Reads the entire Output file
        /// </summary>
        /// <returns></returns>
        public string Read()
        {
            if ((FilePath != null) && (File.Exists(FilePath)))
            {
                string[] contents = File.ReadAllLines(FilePath);
                return contents[contents.Length - 1];
            }
            return "";
        }
        /// <summary>
        /// Reads the specific line in output file, if line entered is negative, reads backwards that many lines
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public string Read(int line)
        {
            try
            {
                if ((FilePath != null) && (File.Exists(FilePath)) && (line < 0))
                {
                    string[] contents = File.ReadAllLines(FilePath);
                    return contents[contents.Length + line];
                }
                if ((FilePath != null) && (File.Exists(FilePath)))
                {
                    string[] contents = File.ReadAllLines(FilePath);
                    return contents[line];
                }
            }
            catch
            {
                return "";
            }
            return "";
        }
        /// <summary>
        /// Reads the entire output file
        /// </summary>
        /// <returns></returns>
        public string[] ReadAll()
        {
            if ((FilePath != null) && (File.Exists(FilePath)))
            {
                string[] contents = File.ReadAllLines(FilePath);
                return contents;
            }
            string[] outPut = { };
            return outPut;
        }
        /// <summary>
        /// Reads Backwards up the file the amount of lines specified
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public string ReadBack(int line)
        {
            if ((FilePath != null) && (File.Exists(FilePath)))
            {
                string[] contents = File.ReadAllLines(FilePath);
                try
                {
                    return contents[contents.Length - line];
                }
                catch (ArgumentOutOfRangeException)
                {
                    return "";
                }
            }
            return "";
        }
        /// <summary>
        /// Clears the Output file at the path, to confirm, requires path and true
        /// </summary>
        /// <param name="path"></param>
        /// <param name="confirm"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="AccessViolationException"></exception>
        public void Clear(string path, bool confirm)
        {
            if (path == FilePath && confirm)
            {
                if (FilePath != null)
                {
                    string[] temp = { };
                    File.WriteAllLines(FilePath, temp);
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            else
            {
                throw new AccessViolationException();
            }
        }
        /// <summary>
        /// Deletes Specific Line in the Output file, it does require the path and confirmation
        /// </summary>
        /// <param name="line"></param>
        /// <param name="confirm"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="AccessViolationException"></exception>
        public void Delete(int line, bool confirm)
        {
            if (confirm)
            {
                if (FilePath != null)
                {
                    List<string> temp = File.ReadAllLines(FilePath).ToList();
                    try
                    {
                        if (line >= 0)
                        {
                            temp.RemoveAt(line);
                        }
                        else
                        {
                            temp.RemoveAt(temp.Count + line);
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {

                    }
                    File.WriteAllLines(FilePath, temp);
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            else
            {
                throw new AccessViolationException();
            }
        }
        /// <summary>
        /// Unnecessary, but it will reset the Output file to null
        /// </summary>
        public void Close()
        {
            FilePath = null;
        }
        #endregion


        #region Static Methods
        /// <summary>
        /// Specific instance of output, where the output is directly written into the file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="value"></param>
        public static void Write(string path, string value)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.ReadAllText(path);
                }
                else
                {
                    File.Create(path);
                }
            }
            catch (FileNotFoundException)
            {
                File.Create(path);
            }
            catch (IOException)
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (value != null)
            {
                StreamWriter file = File.AppendText(path);
                file.Write(value);
                file.Close();
            }
        }
        /// <summary>
        /// Specific instance of output, where the output is directly writing a new line into file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="value"></param>
        public static void WriteLine(string path, string value)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.ReadAllText(path);
                }
                else
                {
                    File.Create(path);
                }
            }
            catch (FileNotFoundException)
            {
                File.Create(path);
            }
            catch (IOException)
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (value != null)
            {
                StreamWriter file = File.AppendText(path);
                file.WriteLine(value);
                file.Close();
            }
        }
        /// <summary>
        /// Converts a char array to a byte array
        /// </summary>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static byte[] ChToBy(Char[] chars)
        {
            List<byte> bytes1 = new();
            foreach (byte b in chars.Select(v => (byte)v))
            {
                bytes1.Append(System.Convert.ToByte(b));
            }
            return bytes1.ToArray();
        }
        #endregion
    }
}
