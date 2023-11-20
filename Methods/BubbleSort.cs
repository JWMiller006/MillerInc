using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillerInc.Methods
{
    /// <summary>
    /// Class that contains the logic for sorting different forms of arrays from least to greatest
    /// </summary>
    public class BubbleSort
    {
        /// <summary>
        /// Sorts a list of integers 
        /// </summary>
        /// <param name="arr">the array to sort</param>
        /// <returns>the sorted array</returns>
        public static int[] OptimizedBubbleSort(int[] arr)
        {
            var n = arr.Length;
            bool swapRequired;

            for (int i = 0; i < n - 1; i++)
            {
                swapRequired = false;

                for (int j = 0; j < n - i - 1; j++)
                    if (arr[j] > arr[j + 1])
                    {
                        (arr[j + 1], arr[j]) = (arr[j], arr[j + 1]);
                        swapRequired = true;
                    }
                if (swapRequired == false)
                    break;
            }

            return arr;
        }

        /// <summary>
        /// If an object is comparable, it can be sorted, this algorithm sorts the list from least to greatest
        /// </summary>
        /// <param name="list">IComparable List that can be sorted</param>
        /// <returns>sorted list</returns>
        public static List<IComparable> OptimizedBubbleSort(List<IComparable> list)
        {
            var n = list.Count;
            bool swapRequired;

            for (int i = 0; i < n - 1; i++)
            {
                swapRequired = false;

                for (int j = 0; j < n - i - 1; j++)
                {
                    if (list[j].CompareTo(list[j + 1]) > 0)
                    {
                        (list[j + 1], list[j]) = (list[j], list[j + 1]);
                        swapRequired = true;
                    }
                }
                if (!swapRequired)
                {
                    break; 
                }
            }
            return list; 
        }

    }
}
