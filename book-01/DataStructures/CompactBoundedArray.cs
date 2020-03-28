using System;

namespace DataStructures
{
    /// <summary>
    /// Array like class with custom lower and upper bounds.
    /// Array index: >= lowerBound AND <= upperBound.
    /// Used space: upperBound - lowerBound + 1
    /// </summary>
    /// <typeparam name="T">Type of the array.</typeparam>
    public sealed class CompactBoundedArray<T>
    {
        private readonly T[] _array;

        public Int32 LowerBound { get; }

        public Int32 UpperBound { get; }

        public Int32 Length { get; }

        public CompactBoundedArray(Int32 lowerBound, Int32 upperBound)
        {
            this._array = new T[upperBound - lowerBound + 1];
            this.LowerBound = lowerBound;
            this.UpperBound = upperBound;

            this.Length = this._array.Length - 1;
        }

        public T this[Int32 index]
        {
            get => this._array[index - this.LowerBound];
            set => this._array[index - this.LowerBound] = value;
        }

        public Boolean InRange(Int32 index)
        {
            return index >= this.LowerBound && index <= this.UpperBound;
        }
    }
}