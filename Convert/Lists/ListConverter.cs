using MillerInc.UI.OutputFile;
using System;
using System.Collections.Generic;
using System.Text;

namespace MillerInc.Convert.Lists
{

    /// <summary>
    /// Converts Lists from One Type to another type
    /// </summary>
    public class ListConverter
    {
        /// <summary>
        /// Main Function that returns a list of objects.
        /// Outdated
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="temp"></param>
        /// <returns>A list of objects</returns>
        public static List<TOut> ConvertTo<T, TOut>(List<T> temp)
        {
            List<TOut> outList = new();
            foreach (T i in temp)
            {
                outList.Add(ConEle<T, TOut>(i));
            }
            return outList;
        }
        public static string TtoStr<T>(T temp)
        {
            return temp.ToString();
        }
        public static int TtoInt<T>(T temp)
        {
            try
            {
                return int.Parse(temp.ToString());
            }
            catch
            {
                return -1;
            }
        }
        public static bool TtoBool<T>(T temp)
        {
            try
            {
                return bool.Parse(temp.ToString());
            }
            catch
            {
                return false;
            }
        }
        public static double TtoDoub<T>(T temp)
        {
            try
            {
                return double.Parse(temp.ToString());
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Converts a single element to another element of a different type
        /// </summary>
        /// <typeparam name="T">the input type</typeparam>
        /// <typeparam name="TOut">the desired output type</typeparam>
        /// <param name="temp"></param>
        /// <param name="returnType"></param>
        /// <returns></returns>
        public static TOut ConEle<T, TOut>(T temp)
        {
            /*
            TOut t = default;
            int INT = default;
            double DOUBLE = default;
            float FLOAT = default;
            bool BOOL = default;
            string STR = default;
            char CHARA = default;*/
            Type returnType = typeof(TOut);
            return (TOut)System.Convert.ChangeType(temp, returnType);
            /*
            if (Type.Equals(t, DOUBLE))
            {
                try
                {
                    return double.Parse(temp.ToString());
                }
                catch
                {
                    return default;
                }
            }
            else if (Type.Equals(t, INT))
            {
                try
                {
                    return int.Parse(temp.ToString());
                }
                catch
                {
                    return default;
                }
            }
            else if (Type.Equals(t, FLOAT))
            {
                try
                {
                    return float.Parse(temp.ToString());
                }
                catch
                {
                    return default;
                }
            }
            else if (Type.Equals(t, STR))
            {
                try
                {
                    return temp.ToString();
                }
                catch
                {
                    return default;
                }
            }
            else if (Type.Equals(t, BOOL))
            {
                try
                {
                    return bool.Parse(temp.ToString());
                }
                catch
                {
                    return default;
                }
            }
            else if (Type.Equals(t, CHARA))
            {
                try
                {
                    return char.Parse(temp.ToString());
                }
                catch
                {
                    return default;
                }
            }
            else
            {
                return default;
            } */
        }

        /// <summary>
        /// This is the main conversion method
        /// </summary>
        /// <param name="inputList">This is the list of the input type</param>
        /// <param name="conversionFunc">This is the individual conversion method for the type</param>
        /// <returns>The list in new type</returns>
        public List<TOutput> ConvertTo<TInput, TOutput>(List<TInput> inputList, Func<TInput, TOutput> conversionFunc)
        {
            List<TOutput> outputList = new();
            foreach (TInput input in inputList)
            {
                TOutput output = conversionFunc(input);
                outputList.Add(output);
            }
            return outputList;
        }
    }

}
