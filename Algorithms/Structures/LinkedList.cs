using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms.Structures
{
    public sealed class LinkedList<T> : IEnumerable<T>
    {
        private Node _head;
        private Int32 _itensCount;

        public Boolean IsEmpty => this._head == null;

        public Int32 Count => this._itensCount;

        public void Add(T item)
        {
            if (this.IsEmpty)
                this._head = new Node(item, null);
            else
                this._head = new Node(item, this._head);

            this._itensCount++;
        }

        public T Remove()
        {
            if (this.IsEmpty)
                throw new InvalidOperationException("No more itens to be removed.");

            T item = this._head.Value;

            this._head = this._head.Next;
            this._itensCount--;

            return item;
        }

        public Boolean Contains(T item)
        {
            Boolean contains = false;
            Node current = this._head;

            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    contains = true;
                    break;
                }

                current = current.Next;
            }

            return contains;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = this._head;

            while (current != null)
            {
                yield return current.Value;

                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private sealed class Node
        {
            public T Value;
            public Node Next;

            public Node(T value, Node next)
            {
                this.Value = value;
                this.Next = next;
            }
        }
    }
}
