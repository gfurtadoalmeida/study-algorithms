using System;

namespace Algorithms.Structures.PriorityQueue
{
    public sealed class MinPQ<T> where T : IComparable<T>
    {
        private readonly Heap<T> _heap;

        public Boolean IsEmpty => this._heap.IsEmpty;

        public Int32 Count => this._heap.Count;

        public MinPQ(Int32 capacity)
        {
            this._heap = new Heap<T>(HeapType.Min, capacity);
        }

        public void Add(T item)
        {
            this._heap.Insert(item);
        }

        public T DeleteMin()
        {
            if (this.IsEmpty)
                throw new InvalidOperationException();

            return this._heap.RemoveTop();
        }

        public T Min()
        {
            if (this.IsEmpty)
                throw new InvalidOperationException();

            return this._heap.Top();
        }
    }
}
