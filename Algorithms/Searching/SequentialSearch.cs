using System;
using System.Collections.Generic;

namespace Algorithms.Searching
{
    public sealed class SequentialSearch<TKey, TValue> : ISymbolTable<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Node _first;

        public Int32 Count { get; private set; }

        public Boolean IsEmpty => this.Count == 0;

        public void Add(TKey key, TValue value)
        {
            for (Node node = this._first; node != null; node = node.Next)
            {
                if (node.Key.Equals(key))
                {
                    node.Value = value;

                    return;
                }
            }

            this._first = new Node(key, value, this._first);

            this.Count++;
        }

        public Boolean Contains(TKey key)
        {
            return this.TryGet(key, out _);
        }

        public void Delete(TKey key)
        {
            this._first = this.Delete(this._first, key);
        }

        public TValue Get(TKey key)
        {
            if (this.TryGet(key, out TValue value))
                return value;

            throw new KeyNotFoundException();
        }

        public IEnumerable<TKey> Keys()
        {
            Node node = this._first;

            while (node != null)
            {
                yield return node.Key;

                node = node.Next;
            }
        }

        private Boolean TryGet(TKey key, out TValue value)
        {
            for (Node node = this._first; node != null; node = node.Next)
            {
                if (node.Key.Equals(key))
                {
                    value = node.Value;

                    return true;
                }
            }
            value = default;

            return false;
        }

        private Node Delete(Node node, TKey key)
        {
            if (node == null)
                return null;

            if (node.Key.Equals(key))
            {
                this.Count--;

                return node.Next;
            }

            node.Next = this.Delete(node.Next, key);

            return node;
        }

        private sealed class Node
        {
            internal TKey Key { get; }

            internal TValue Value { get; set; }

            internal Node Next { get; set; }

            public Node(TKey key, TValue value, Node next)
            {
                this.Key = key;
                this.Value = value;
                this.Next = next;
            }
        }
    }
}
