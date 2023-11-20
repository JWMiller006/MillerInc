using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillerInc.Methods.Lists
{

    public class Copy
    {
        public static List<T> CopyFrom<T>(List<T> other)
        {
            List<T> output = new();
            foreach (T obj in other)
            {
                output.Add(obj);
            }
            return output;
        }
    }
}
