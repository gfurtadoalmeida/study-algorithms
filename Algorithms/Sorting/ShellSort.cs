using System;

namespace Algorithms.Sorting
{
    public sealed class ShellSort<T> : AbstractSort<T> where T : IComparable<T>
    {
        public override void Sort(T[] input)
        {
            // Initialize the value of h (interval) to the biggest interval below input size.
            // -
            // Divide the list into smaller sub-list of equal interval "h".
            // Sort these sub-lists using insertion sort.
            // Decrement the value of the interval.
            // Repeat until end.

            // Who is Knuth? https://en.wikipedia.org/wiki/Donald_Knuth
            // Knuth's Formula: h = h * 3 + 1
            // Where: h is the interval

            Int32 count = input.Length;
            Int32 h = 1;

            // Find the biggest interval below input size.
            while (h < count / 3)
                h = h * 3 + 1;

            while (h >= 1)
            {
                // For each interval:
                for (int i = h; i < count; i++)
                {
                    // Sort the itens among each interval a[i-h], a[i-2*h], a[i-3*h]... .
                    for (int j = i; j >= h; j -= h)
                    {
                        if (base.IsLess(input[j], input[j - h]))
                            base.Exchange(input, j, j - h);
                        else
                            break;
                    }
                }

                h = h / 3; // Reducing interval.
            }
        }
    }
}
