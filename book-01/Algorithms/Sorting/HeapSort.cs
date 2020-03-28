using System;

namespace Algorithms.Sorting
{
    public sealed class HeapSort<T> : AbstractSort<T> where T : IComparable<T>
    {
        // Invented by J. W. J. Williams and refined by R. W. Floyd in 1964.

        public override void Sort(T[] input)
        {
            Int32 maxIndex = input.Length - 1;

            for (int k = (maxIndex / 2) - 1; k > -1; k--)
            {
                this.Sink(input, k, maxIndex);
            }

            while (maxIndex > 0)
            {
                this.Exchange(input, 0, maxIndex--);
                this.Sink(input, 0, maxIndex);
            }
        }

        private void Sink(T[] input, Int32 indexItem, Int32 count)
        {
            // Sink: go lower in the hierarchy.

            // On a binary tree, the indexes of a parent's children are:
            // indexLeftChild = currentIndex * 2
            // indexRightChild = (currentIndex * 2) + 1

            Int32 indexChild = (2 * indexItem) + 1;

            while (indexChild <= count)
            {
                // Compare the left and right child.
                // If the left child has a lower order than then right child, we'll use the right child.
                if (indexChild < count && this.IsLess(input[indexChild], input[indexChild + 1]))
                {
                    indexChild++;
                }

                // Compare the parent with the child.
                if (this.IsLess(input[indexItem], input[indexChild]))
                {
                    this.Exchange(input, indexItem, indexChild);

                    indexItem = indexChild;

                    indexChild = (indexChild * 2) + 1;
                }
                else
                    break; // We've reached order.
            }
        }
    }
}
