using System;
using DataStructures;

namespace Algorithms.Strings
{
    /// <summary>
    /// String sorting where the strings have different lengths and with small number of equal strings.
    /// Uses key-indexed counting sort reading left to right.
    /// Not suitable for:
    /// - Equal strings.
    /// - Strings keys with long common prefixes.
    /// - Strings that fall into a small range.
    /// - Small arrays.
    /// </summary>
    public static class MostSignificantDigitFirstSort
    {
        private const Int32 CUT_OFF = 15; // Cutoff for small subarrays.

        public static void Sort(String[] array, Alphabet alphabet)
        {
            String[] aux = new String[array.Length];

            Sort(array, aux, 0, array.Length - 1, 0, alphabet);
        }

        private static void Sort(String[] array,
            String[] aux,
            Int32 low,
            Int32 high,
            Int32 index,
            Alphabet alphabet)
        {
            if (high <= low + CUT_OFF)
            {
                InsertionSort(array, low, high, index);

                return;
            }

            CompactBoundedArray<Int32> count = new CompactBoundedArray<Int32>(alphabet.MinChar, alphabet.MaxChar + 2);

            // 1 - Compute frequency counts.

            for (int i = low; i <= high; i++)
            {
                count[CharAt(array[i], index) + 2]++;
            }

            // 2 - Transform counts to indices.

            for (int i = 0; i < alphabet.Radix + 1; i++)
            {
                count[i + 1] += count[i];
            }

            // 4 - Distribute.

            for (int i = low; i <= high; i++)
            {
                aux[count[CharAt(array[i], index) + 1]++] = array[i];
            }

            // 5 - Copy back.

            for (int i = low; i <= high; i++)
            {
                array[i] = aux[i - low];
            }

            // 6 - Recursively sort for each character value.

            for (int i = 0; i < alphabet.Radix; i++)
            {
                Sort(array,
                     aux,
                     low + count[i],
                     low + count[i + 1] - 1,
                     index + 1,
                     alphabet);
            }
        }

        private static Int32 CharAt(String text, Int32 index)
        {
            if (index < text.Length)
                return text[index];

            return -1;
        }

        private static void InsertionSort(String[] array, Int32 low, Int32 high, Int32 index)
        {
            for (int i = low; i <= high; i++)
            {
                for (int j = i; j > low && IsLess(array[j], array[j - 1], index); j--)
                {
                    Exchange(array, j, j - 1);
                }
            }
        }

        private static void Exchange(String[] array, Int32 indexSource, Int32 indexTarget)
        {
            String temp = array[indexSource];
            array[indexSource] = array[indexTarget];
            array[indexTarget] = temp;
        }

        private static Boolean IsLess(String source, String target, Int32 index)
        {
            for (int i = index; i < Math.Min(source.Length, target.Length); i++)
            {
                if (source[i] < target[i])
                    return true;

                if (source[i] > target[i])
                    return false;
            }

            return source.Length < target.Length;
        }
    }
}
