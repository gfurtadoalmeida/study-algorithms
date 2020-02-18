using System;

namespace DataStructures.PriorityQueue
{
    public sealed class MaxPQ<T> where T : IComparable<T>
    {
        private readonly Heap<T> _heap;

        public Boolean IsEmpty => this._heap.IsEmpty;

        public Int32 Count => this._heap.Count;

        public MaxPQ(Int32 capacity)
        {
            this._heap = new Heap<T>(HeapType.Max, capacity);
        }

        public void Add(T item)
        {
            this._heap.Insert(item);
        }

        public T DeleteMax()
        {
            if (this.IsEmpty)
                throw new InvalidOperationException();

            return this._heap.RemoveTop();
        }

        public T Max()
        {
            if (this.IsEmpty)
                throw new InvalidOperationException();

            return this._heap.Top();
        }
    }
}
