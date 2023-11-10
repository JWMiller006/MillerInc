using System;
using System.Collections.Generic;
using System.Text;

namespace MillerInc.Convert.Lists
{
    public class ListToString
    {
        public static string ListString<T>(List<T> inList, string sepA = "", string sepB = "\n")
        {
            string Output = "";
            int count = 0;
            foreach (T obj in inList)
            {
                if (count < inList.Count - 1)
                {
                    Output += obj;
                    Output += sepA;
                }
                else
                {
                    Output += obj;
                    Output += sepB;
                }
                count++;
            }
            return Output;
        }
    }
}
