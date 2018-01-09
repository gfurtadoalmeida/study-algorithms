using System;
using System.Collections.Generic;

namespace Algorithms.Sorting
{
    public static class KeyIndexedCountingSort
    {
        /// <summary>
        /// Sorts an array of key-value pair by key, where key is between 0 and radix - 1.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="itens">Array of key-value pair.</param>
        /// <param name="radix">Number of unique digits, including zero.</param>
        public static void Sort<TValue>(KeyValuePair<Int32, TValue>[] itens, Int32 radix)
        {
            KeyValuePair<Int32, TValue>[] aux = new KeyValuePair<Int32, TValue>[itens.Length];
            Int32[] frequency = new Int32[radix + 1];

            // 1 - Count frequencies of each iten using key as index.

            for (int i = 0; i < itens.Length; i++)
                frequency[itens[i].Key + 1]++;

            // 2 - Compute frequency cumulates which specify destinations.

            for (int i = 0; i < radix; i++)
                frequency[i + 1] += frequency[i];

            // 3 - Access cumulates using key as index to move items.

            for (int i = 0; i < itens.Length; i++)
                aux[frequency[itens[i].Key]++] = itens[i];

            // 4 - Copy back into original array.

            for (int i = 0; i < itens.Length; i++)
                itens[i] = aux[i];
        }
    }
}
