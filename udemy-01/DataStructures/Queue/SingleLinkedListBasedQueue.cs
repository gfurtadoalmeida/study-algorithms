using System;
using System.Collections;
using System.Collections.Generic;
using DataStructures.LinkedList;

namespace DataStructures.Queue
{
    public class SingleLinkedListBasedQueue<T> : IQueue<T>
    {
        private readonly SingleLinkedList<T> _linkedList;

        public bool IsEmpty => this._linkedList.IsEmpty;

        public SingleLinkedListBasedQueue()
        {
            this._linkedList = new SingleLinkedList<T>();
        }

        public void Enqueue(T value)
        {
            this._linkedList.InsertAt(this._linkedList.Count, value);
        }

        public T Dequeue()
        {
            if (this.IsEmpty)
                throw new InvalidOperationException();

            T value = this._linkedList.GetAt(0);

            this._linkedList.DeleteAt(0);

            return value;
        }

        public T Peek(int position)
        {
            if (position >= this._linkedList.Count)
                throw new ArgumentOutOfRangeException(nameof(position));

            return this._linkedList.GetAt(position);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this._linkedList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
