using System;
using DataStructures;

namespace Algorithms.Strings
{
    /// <summary>
    /// String sorting where all the strings have the same length.
    /// Uses key-indexed counting sort reading from right to left.
    /// </summary>
    public static class LeastSignificantDigitFirstSort
    {
        public static void Sort(String[] array, Int32 stringLength, Alphabet alphabet)
        {
            String[] aux = new String[array.Length];
            CompactBoundedArray<Int32> count = new CompactBoundedArray<Int32>(alphabet.MinChar, alphabet.MaxChar);

            // Sort by key-indexed counting on dth character.
            // Same logic of KeyIndexedCountingSort.
            // Reading right to left.

            for (int c = stringLength - 1; c >= 0; c--)
            {
                // 1 - Compute frequency counts.

                for (int i = 0; i < array.Length; i++)
                {
                    count[array[i][c] + 1]++;
                }

                // 2 - Compute cumulates.

                for (int i = count.LowerBound; i < count.UpperBound; i++)
                {
                    count[i + 1] += count[i];
                }

                // 3 - Move data.

                for (int i = 0; i < array.Length; i++)
                {
                    aux[count[array[i][c]]++] = array[i];
                }

                // 4 - Copy data back.

                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = aux[i];
                }

                // Cleaning for the next round.

                for (int i = count.LowerBound; i < count.UpperBound; i++)
                {
                    count[i] = 0;
                }
            }
        }
    }
}
