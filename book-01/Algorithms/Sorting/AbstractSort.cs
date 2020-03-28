using System;

namespace Algorithms.Sorting
{
    public abstract class AbstractSort<T> where T : IComparable<T>
    {
        protected Boolean IsLess(T a, T b)
        {
            return a.CompareTo(b) == -1;
        }

        protected void Exchange(T[] input, Int32 indexTarget, Int32 indexSource)
        {
            T temp = input[indexTarget];

            input[indexTarget] = input[indexSource];

            input[indexSource] = temp;
        }

        public abstract void Sort(T[] input);
    }
}
