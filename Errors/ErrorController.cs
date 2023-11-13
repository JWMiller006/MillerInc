using MillerInc.Convert.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace MillerInc.Errors
{
    /// <summary>
    /// A controller that is able to control the output of errors that relate to MillerInc projects; must have the error document installed to run properly; 
    /// Not implemented in most new MillerInc Projects  
    /// </summary>
    public class ErrorController
    {
        public ErrorController()
        {

        }
        /// <summary>
        /// Creates a new error with index given
        /// </summary>
        /// <param name="errorNum"></param>
        public ErrorController(int errorNum)
        {
            ErrorNum = errorNum;
            Message = GetError();
        }
        /// <summary>
        /// Creates a new error with index given from file given
        /// </summary>
        /// <param name="errorNum"></param>
        /// <param name="errorFile"></param>
        public ErrorController(int errorNum, string errorFile)
        {
            ErrorNum = errorNum;
            FilePath = errorFile;
            Message = GetError();
        }
        public string Message { get; set; } = "Unknown Error";
        public int ErrorNum { get; set; } = 0;
        public string FilePath { get; set; } = "C:\\Program Files (x86)\\Miller Inc\\Network App Data\\Error Document.txt";
        public string GetError()
        {
            string error;
            List<string> errorList = FileToList.FileRead(FilePath);
            try
            {
                error = errorList[ErrorNum - 1];
            }
            catch
            {
                error = "Unknown Error Occured: Try Again Later";
            }
            return error;
        }
        public void NewError(int errorNum)
        {
            ErrorNum = errorNum;
            Message = GetError();
        }
    }
}
