using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MillerInc.Methods.Lists
{
    /// <summary>
    /// Contains the logic to covnert multi-dimensional lists to arrays and arrays to lists
    /// </summary>
    public class MultiDimensionalArrayConverter
    {
        /// <summary>
        /// Converts a three-dimensional array to a three-dimensional list
        /// </summary>
        /// <typeparam name="T">the type of the array</typeparam>
        /// <param name="arr">the array to convert</param>
        /// <returns>the converted list</returns>
        public List<List<List<T>>> Convert3DArr<T>(T[][][] arr)
        {
            List<List<List<T>>> list = new()
            {
                // Set the capacity of the list to be the same as the array
                Capacity = arr.Length
            }; 

            // For each T[][] in T[][][]
            for (int i = 0;  i < arr.Length; i++)
            {
                list.Add(new());
                list[i].Capacity = arr[i].Length; 

                // For each T[] in T[][]
                for (int j = 0; j < arr[i].Length; j++)
                {
                    list[i].Add(new());
                    list[i][j] = arr[i][j].ToList(); 
                }
            }

            return list; 
        }

        /// <summary>
        /// Converts a three-dimensional list to a three-dimensional array
        /// </summary>
        /// <typeparam name="T">type of the list</typeparam>
        /// <param name="list">the list to convert</param>
        /// <returns>the converted array</returns>
        public T[][][] Convert3DList<T>(List<List<List<T>>> list)
        {
            T[][][] arr = new T[list.Count()][][]; 

            // For each List<List<T>> in List<List<List<T>>>
            for (int i = 0; i < list.Count(); i++)
            {
                arr[i] = new T[list[i].Count()][]; 

                // For each List<T> in List<List<T>>
                for (int j = 0; j < list[i].Count(); j++)
                {
                    arr[i][j] = list[i][j].ToArray(); 
                }
            }

            return arr; 
        }
    }
}
