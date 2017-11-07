using System;

namespace Algorithms.Structures
{
    public sealed class Queue<T>
    {
        private Node _head;
        private Node _tail;
        private Int32 _itensCount;

        public Boolean IsEmpty => this._head == null;
        public Int32 Count => this._itensCount;

        public void Enqueue(T item)
        {
            if (this.IsEmpty)
            {
                this._head = new Node(item, null);
                this._tail = this._head;
            }
            else
            {
                Node newHead = new Node(item, null);

                this._head.Previous = newHead;
                this._head = newHead;
            }

            this._itensCount++;
        }

        public T Dequeue()
        {
            if (this.IsEmpty)
                throw new InvalidOperationException("No more itens to be dequeued.");

            T item = this._tail.Value;

            this._tail = this._tail.Previous;

            if (this._tail == null)
                this._head = null;

            this._itensCount--;

            return item;
        }

        private sealed class Node
        {
            public T Value;
            public Node Previous;

            public Node(T value, Node previous)
            {
                this.Value = value;
                this.Previous = previous;
            }
        }
    }
}
