using System;

namespace Algorithms.Sorting
{
    public sealed class QuickSort<T> : AbstractSort<T> where T : IComparable<T>
    {
        public override void Sort(T[] input)
        {
            this.Sort(input, 0, input.Length - 1);
        }

        private void Sort(T[] input, Int32 low, Int32 high)
        {
            // Sort using E. W. Dijkstra 3-way partition.
            // Every item encountered leads to an exchange except for 
            // those items with keys equal to the partitioning item’s key.
            // This 3-way partitioning is useful for inputs with lots of equalt values.

            if (high > low)
            {
                Int32 idxLessThanPartition = low;     // input[low..idxLessThanPartition-1] < partition
                Int32 idxEqualtToPartition = low + 1; // input[idxLessThanPartition..idxEqualToPartition] == partition
                Int32 idxGreaterThanPartition = high; // input[low..idxGreaterThanPartition] > partition
                T partition = input[low];

                while (idxEqualtToPartition <= idxGreaterThanPartition)
                {
                    Int32 cmp = input[idxEqualtToPartition].CompareTo(partition);

                    if (cmp < 0)
                    {
                        this.Exchange(input, idxLessThanPartition++, idxEqualtToPartition++);
                    }
                    else
                    {
                        if (cmp > 0)
                            this.Exchange(input, idxEqualtToPartition, idxGreaterThanPartition--);
                        else
                            idxEqualtToPartition++;
                    }
                }

                // |--------------------------------------------------------|
                // |    less than     |     equal to     |   greater than   |
                // |--------------------------------------------------------|
                //        sort ^                                sort ^

                this.Sort(input, low, idxLessThanPartition - 1);
                this.Sort(input, idxGreaterThanPartition + 1, high);
            }
        }
    }
}
