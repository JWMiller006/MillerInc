using MillerInc.Convert.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace MillerInc.Errors
{
    public class ErrorController
    {
        public ErrorController()
        {

        }
        public ErrorController(int errorNum)
        {
            ErrorNum = errorNum;
            Message = GetError();
        }
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
