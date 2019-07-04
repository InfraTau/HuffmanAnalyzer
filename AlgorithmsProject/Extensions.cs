using System;

namespace AlgorithmsProject
{
    public static class Extensions
    {
        public static int BinaryStringToInt32(this string Value)
        {
            int iBinaryStringToInt32 = 0;

            for (int i = (Value.Length - 1), j = 0; i >= 0; i--, j++)
            {
                iBinaryStringToInt32 += ((Value[j] == '0' ? 0 : 1) * (int)(Math.Pow(2, i)));
            }

            return iBinaryStringToInt32;
        }
    }
}
