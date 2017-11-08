using System;

namespace Algorithms.Sorting
{
    public sealed class BubbleSort<T> : AbstractSort<T> where T : IComparable<T>
    {
        public override void Sort(T[] input)
        {
            Boolean swapped = false;

            // Loop through all numbers 
            for (int i = 0; i < input.Length - 1; i++)
            {
                swapped = false;

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
