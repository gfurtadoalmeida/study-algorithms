using System;

namespace Algorithms.Sorting
{
    public sealed class BubbleSort<T> : AbstractSort<T> where T : IComparable<T>
    {
        public override void Sort(T[] input)
        {
            Boolean swapped = false;

            // Loop through all numbers, except the last one in the
            // logic that follows we check current and current + 1.
            for (int i = 0; i < input.Length - 1; i++)
            {
                // Loop through numbers ahead
                for (int j = 0; j < input.Length - 1 - i; j++)
                {
                    // If the number is smaller than current, bubble up the highest number (current).
                    if (this.IsLess(input[j + 1], input[j]))
                    {
                        this.Exchange(input, j, j + 1);

                        swapped = true;
                    }
                }

                // If no number was swapped then the array is sorted.
                if (!swapped)
                    break;
            }
        }
    }
}
