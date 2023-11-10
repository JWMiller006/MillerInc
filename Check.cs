using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MillerInc
{
    /// <summary>
    /// Contains Basic Functions that are found in other languages that I liked
    /// </summary>
    public class Check
    {
        /// <summary>
        /// Checks if the element is listed in the input list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ele">The value to search for</param>
        /// <param name="values">The list of values to search through</param>
        /// <returns>Returns a boolean; If ele is in values, returns true, else returns false</returns>
        public static bool IsIn<T>(T ele, List<T> values)
        {
            foreach (T value in values)
            {
                if (ele.Equals(value)) return true;
            }
            return false;
        }


        /// <summary>
        /// Checks if the element is listed in the input array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ele">The value to search for</param>
        /// <param name="values">The array of values to search through</param>
        /// <returns>Returns a boolean; If ele is in values, returns true, else returns false</returns>
        public static bool IsIn<T>(T ele, T[] values)
        {
            foreach (T value in values)
            {
                if (value.Equals(ele))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Checks if the substring is in the total string
        /// </summary>
        /// <param name="sub">Smaller string that we are checking for</param>
        /// <param name="total">Total string we are checking in</param>
        /// <returns>If sub is in total, then return true</returns>
        public static bool IsIn(string sub, string total)
        {
            if (total.Length >= sub.Length)
            {
                return total.Contains(sub);
            }
            return false;
        }
    }


}
