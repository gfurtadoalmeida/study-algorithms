using System;

namespace Algorithms.Sorting
{
    public sealed class MergeSort<T> : AbstractSort<T> where T : IComparable<T>
    {
        public enum MergeSortType : byte { TopDown = 1, BottomUp = 2 }

        private Action<T[], Int32, Int32> _method;
        private T[] _auxiliaryArray;

        public MergeSort() : this(MergeSortType.TopDown)
        {
        }

        public MergeSort(MergeSortType mergeSortType)
        {
            switch (mergeSortType)
            {
                case MergeSortType.TopDown:
                    this._method = this.TopDownSort;
                    break;

                case MergeSortType.BottomUp:
                    this._method = this.BottomUpSort;
                    break;

                default:
                    throw new NotImplementedException($"The merge sort type '{mergeSortType}' is not implemented.");
            }
        }

        public override void Sort(T[] input)
        {
            this._auxiliaryArray = new T[input.Length];

            this._method(input, 0, input.Length - 1);
        }

        private void TopDownSort(T[] input, Int32 low, Int32 high)
        {
            if (high <= low) //We have found the minimum size.
                return;

            Int32 mid = low + ((high - low) / 2);

            // Dividing until we find the minimum array size
            // for each side (right and left).

            // Sweep (split-merge) one side first (half-array) and 
            // then the second.

            this.TopDownSort(input, low, mid);
            this.TopDownSort(input, mid + 1, high);

            // Then we merge up (from the smallest array to the bigger one).
            // The merge that really does the trick of sorting.

            this.Merge(input, low, mid, high);
        }

        private void BottomUpSort(T[] input, Int32 low, Int32 high)
        {
            Int32 count = input.Length;
            Int32 setSize = 2;

            // Starts from the left to right doubling the set size on each pass.
            // Full sweep from left to right on each pass.

            for (int mid = 1; mid < count; mid = setSize / 2)
            {
                for (low = 0; low < count - mid; low += setSize)
                    this.Merge(input,
                               low,
                               low + mid - 1,
                               Math.Min(low + setSize - 1, count - 1));

                setSize *= 2;
            }
        }

        private void Merge(T[] input, Int32 low, Int32 mid, Int32 high)
        {
            Int32 idxLow = low;
            Int32 idxHigh = mid + 1;

            for (int k = low; k <= high; k++)
                this._auxiliaryArray[k] = input[k];

            for (int idxCurrent = low; idxCurrent <= high; idxCurrent++)  // Merge back to a[lo..hi].
            {
                if (idxLow > mid) // In case we are out of low get the next one from high.
                    input[idxCurrent] = this._auxiliaryArray[idxHigh++];
                else
                {
                    if (idxHigh > high) // In case we are out of high, get the next one from low.
                        input[idxCurrent] = this._auxiliaryArray[idxLow++];
                    else
                    {
                        if (base.IsLess(_auxiliaryArray[idxHigh], _auxiliaryArray[idxLow])) // If high < low get high.
                            input[idxCurrent] = this._auxiliaryArray[idxHigh++];
                        else
                            input[idxCurrent] = this._auxiliaryArray[idxLow++]; // Else get low.
                    }
                }
            }
        }
    }
}
