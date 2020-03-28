using System;

namespace Algorithms.Test.Sorting
{
    public static class SortData
    {
        private static readonly Int32[] ARRAY_UNSORTED = new Int32[10] { 55, 32, 84, 53, 12, 9, 96, 101, 18, 3 };
        private static readonly Int32[] ARRAY_SORTED = new Int32[10] { 3, 9, 12, 18, 32, 53, 55, 84, 96, 101 };

        public static Int32[] CreateUnsortedArray()
        {
            Int32[] array = new Int32[ARRAY_UNSORTED.Length];

            Array.Copy(ARRAY_UNSORTED, array, ARRAY_UNSORTED.Length);

            return array;
        }

        public static Boolean VerifyArrayIsSorted(Int32[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length != ARRAY_UNSORTED.Length)
                return false;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != ARRAY_SORTED[i])
                    return false;
            }

            return true;
        }
    }
}
