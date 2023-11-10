using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MillerInc.Convert.Strings
{
    /// <summary>
    /// Currently the main algorithm for converting user input to boolean
    /// To call basic use FullStrBool.GetBool
    /// </summary>
    public class StringToBoolean
    {

        /// <summary>
        /// Takes a user input and converts it to a boolean 
        /// </summary>
        /// <param name="input">The user input</param>
        /// <returns>The converted boolean</returns>
        public static bool GetBool(string input)
        {
            bool check = true;

            // Strings that are considered true 
            var yea = BoolLists.Yea;

            // Strings that are considered false
            List<string> ney = BoolLists.Ney;
            while (check)
            {
                input = input.ToLower();
                for (var i = 0; i < yea.Count(); i++)
                {
                    if (input == yea[i])
                    {
                        return true;
                    }
                }
                for (var i = 0; i < ney.Count(); i++)
                {
                    if (input == ney[i])
                    {
                        return false;
                    }
                }
                Console.WriteLine("Your input was not understood, try again...");
                input = Console.ReadLine();
            }
            return false;
        }

        /// <summary>
        /// Similar to TryParse, but instead of returning if whether it can be parsed, but what it can be parsed to
        /// </summary>
        /// <param name="input">User input</param>
        /// <returns></returns>
        public static bool TryBool(string input)
        {
            List<string> tr = BoolLists.Yea;
            List<string> fa = BoolLists.Ney;
            input = input.ToLower();
            for (var i = 0; i < tr.Count; i++)
            {
                if (input == tr[i])
                {
                    return true;
                }
            }
            for (var i = 0; i < fa.Count(); i++)
            {
                if (input == fa[i])
                {
                    return false;
                }
            }
            return false;
        }
    }
}
