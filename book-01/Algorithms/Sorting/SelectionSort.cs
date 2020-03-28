using System;

namespace Algorithms.Sorting
{
    public sealed class SelectionSort<T> : AbstractSort<T> where T : IComparable<T>
    {
        public override void Sort(T[] input)
        { 
            // Start at index 0.
            // Find the smallest item and exchange it with the item on index 0.
            // Increment index.
            // ...
            // Repeat until end.

            Int32 count = input.Length;

            for (int i = 0; i < count; i++)
            {
                Int32 indexMin = i;

                // Find the index of the last smallest item.
                for (int j = i + 1; j < count; j++)
                {
                    if (base.IsLess(input[j], input[indexMin]))
                    {
                        indexMin = j;
                    }
                }

                base.Exchange(input, i, indexMin);
            }
        }
    }
}
