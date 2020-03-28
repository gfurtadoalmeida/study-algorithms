using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    public sealed class Bag<T> : IEnumerable<T>
    {
        private Node _first;

        public Int32 Count { get; private set; }

        public Boolean IsEmpty => this._first == null;

        public void Add(T item)
        {
            Node oldfirst = this._first;

            this._first = new Node
            {
                Item = item,
                Next = oldfirst
            };

            this.Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node node = this._first;

            while (node != null)
            {
                yield return node.Item;

                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private sealed class Node
        {
            internal T Item;
            internal Node Next;
        }
    }
}
