using System;

namespace Algorithms.Sorting
{
    public sealed class SelectionSort<T> : AbstractSort<T> where T : IComparable<T>
    {
        public override void Sort(T[] input)
        {
            // Find the smallest item and exchange it with the first item.
            // Goes to next item.
            // Find the smallest item and exchange it with the second item.
            // ...
            // Repeat until end.

            Int32 N = input.Length;

            for (int i = 0; i < N; i++)
            {
                Int32 indexMin = i;

                for (int j = i + 1; j < N; j++)
                {
                    if (base.IsLess(input[j], input[indexMin]))
                        indexMin = j;
                }

                base.Exchange(input, i, indexMin);
            }
        }
    }
}
