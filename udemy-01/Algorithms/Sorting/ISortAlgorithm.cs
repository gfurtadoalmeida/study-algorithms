using System;

namespace Algorithms.Sorting
{
    public interface ISortAlgorithm
    {
        void Sort<T>(ref T[] array) where T : IComparable<T>;
    }
}
