using System;

namespace Algorithms.Sorting
{
    public abstract class SortAlgorithm : ISortAlgorithm
    {
        protected SortAlgorithm()
        {
        }

        protected abstract void OnSort<T>(ref T[] array, int arrayLength) where T : IComparable<T>;

        public void Sort<T>(ref T[] array) where T : IComparable<T>
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length < 2)
                return;

            this.OnSort(ref array, array.Length);
        }
    }
}
