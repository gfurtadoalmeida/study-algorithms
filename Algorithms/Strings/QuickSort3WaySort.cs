using System;

namespace Algorithms.Strings
{
    /// <summary>
    /// String sorting where the strings have different lengths and may:
    /// - Have equal strings. 
    /// - Have strings keys with long common prefixes.
    /// - Have strings that fall into a small range.
    /// - Be a small array.
    /// </summary>
    public static class QuickSort3WaySort
    {
        private const Int32 CUT_OFF = 15; // Cutoff for small subarrays.

        public static void Sort(String[] array)
        {
            Sort(array, 0, array.Length - 1, 0);
        }

        private static void Sort(String[] array, Int32 low, Int32 high, Int32 index)
        {
            if (high <= low + CUT_OFF)
            {
                InsertionSort(array, low, high, index);

                return;
            }

            Int32 partition = CharAt(array[low], index);
            Int32 idxLessThanPartition = low;     // input[low..idxLessThanPartition-1] < partition
            Int32 idxEqualToPartition = low + 1;  // input[idxLessThanPartition..idxEqualToPartition] == partition
            Int32 idxGreaterThanPartition = high; // input[low..idxGreaterThanPartition] > partition

            while (idxEqualToPartition <= idxGreaterThanPartition)
            {
                Int32 t = CharAt(array[idxEqualToPartition], index);

                if (t < partition)
                {
                    Exchange(array, idxLessThanPartition++, idxEqualToPartition++);
                }
                else if (t > partition)
                {
                    Exchange(array, idxEqualToPartition, idxGreaterThanPartition--);
                }
                else
                {
                    idxEqualToPartition++;
                }
            }

            Sort(array, low, idxLessThanPartition - 1, index);

            if (partition >= 0)
            {
                Sort(array, idxLessThanPartition, idxGreaterThanPartition, index + 1);
            }

            Sort(array, idxGreaterThanPartition + 1, high, index);

            // |--------------------------------------------------------|
            // |    less than     |     equal to     |   greater than   |
            // |--------------------------------------------------------|
            //        sort ^            sort ^              sort ^
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
