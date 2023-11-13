using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MillerInc.Convert.Strings
{
    /// <summary>
    /// This is a class of the lists used to determine things as true or false, change wisely
    /// </summary>
    public class BoolLists
    {
        /// <summary>
        /// Strings that relate to true
        /// </summary>
        public static List<string> Yea { get; set; } = new() 
        { "yes", "yeah", "true", "y", "e", "s", "Yea", "ok", "okay", "duh", "", " " };

        /// <summary>
        /// Strings that relate to false
        /// </summary>
        public static List<string> Ney { get; set; } = new() 
        { "no", "not", "false", "n", "o", "nah", "nope", "not today", "what do you think?", "exit", "e" };
    }
}
