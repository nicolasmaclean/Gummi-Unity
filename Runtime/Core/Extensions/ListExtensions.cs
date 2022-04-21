using System.Collections;

namespace Gummi
{
    public static class ListExtensions
    {

        /// <summary>
        /// Prints <paramref name="list"/> in a human-readable format.
        /// It prints with each element on its own line as "## : item"
        /// such that ## is the index, zero padded, and item is item.ToString().
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string PrettyPrint(this IList list)
        {
            if (list == null)
            {
                return "null";
            }

            string output = "";

            int indexLength = (list.Count - 1).CountDigits() + 1;
            for (int i = 0; i < list.Count; i++)
            {
                output += $"{i.ToString("D" + indexLength)} : {list[i]}\n";
            }

            return output;
        }
    }
}