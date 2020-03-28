using System;

namespace Algorithms.Sorting
{
    public sealed class InsertionSort<T> : AbstractSort<T> where T : IComparable<T>
    {
        public override void Sort(T[] input)
        {
            // Start at index 1.
            // While all the items below index 1 are bigger than item at index 1:
            //     Move all items after index 1 to the right one position.
            //     Insert item at index 1 on the empty room.
            // Increment index
            // ...
            // Repeat until end

            for (int i = 1; i < input.Length; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    // Moving itens to the right is accomplished by swapping the itens 
                    // one by one until the target is in the correct position.

                    if (base.IsLess(input[j], input[j - 1]))
                    {
                        base.Exchange(input, j, j - 1);
                    }
                    else
                    {
                        // As the itens on the left are already sorted, if the item
                        // is bigger or equal to a position, we're finished.
                        break;
                    }
                }
            }
        }
    }
}
