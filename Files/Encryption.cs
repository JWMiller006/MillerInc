using System;
using System.Collections.Generic;
using System.Text;
using System.IO; 

namespace MillerInc.Files
{
    public class Encryption
    {
        public static List<string> ReadDecrypted(string file)
        {
            var output = new List<string> { };
            File.Decrypt(file);
            output.AddRange(Convert.Files.FileToList.FileRead(file));
            File.Encrypt(file);
            return output;
        }
    }
}
