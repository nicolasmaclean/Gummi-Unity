using UnityEngine;

namespace Gummi
{
    public static class IntExtensions
    {
        /// <summary>
        /// Returns the number of digits <paramref name="val"/> has.
        /// Note the negative sign will not be counted as a sign.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int CountDigits(this int val)
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
    }
}