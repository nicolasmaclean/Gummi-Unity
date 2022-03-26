using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gummi.Core.Logger
{
    public static class StringUtils
    {
        /// <summary>
        /// Returns the number of digits <paramref name="val"/> has.
        /// Note the negative sign will not be counted as a sign.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int CountDigits(int val)
        {
            if (val == 0)
            {
                return 1;
            }
            else
            {
                return Mathf.FloorToInt(Mathf.Log10(Mathf.Abs(val))) + 1;
            }
        }

        /// <summary>
        /// Prints <paramref name="list"/> in a human-readable format.
        /// It prints with each element on its own line as "## : item"
        /// such that ## is the index, zero padded, and item is item.ToString().
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string PrettyPrint(IList list)
        {
            if (list == null)
            {
                return "null";
            }

            string output = "";

            int indexLength = CountDigits(list.Count - 1) + 1;
            for (int i = 0; i < list.Count; i++)
            {
                output += $"{i.ToString("D" + indexLength)} : {list[i]}\n";
            }

            return output;
        }
    }
}