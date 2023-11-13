using System;
using System.Collections.Generic;
using System.Text;

namespace MillerInc.Convert.Lists
{
    public class ListToString
    {
        /// <summary>
        /// Converts the list to a single string with the specified seperators between the elements
        /// </summary>
        /// <typeparam name="T">type of in the list</typeparam>
        /// <param name="inList">the list to convert to a string</param>
        /// <param name="sepA">the seperator between each element of the list except for the last one </param>
        /// <param name="sepB">the final seperator between elements n - 2 and n - 1 </param>
        /// <returns></returns>
        public static string ListString<T>(List<T> inList, string sepA = "", string sepB = "\n")
        {
            string Output = "";
            int count = 0;
            foreach (T obj in inList)
            {
                if (count < inList.Count - 1)
                {
                    Output += obj.ToString();
                    Output += sepA;
                }
                else
                {
                    Output += obj.ToString();
                    Output += sepB;
                }
                count++;
            }
            return Output;
        }
    }
}
