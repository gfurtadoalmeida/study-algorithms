using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms.Structures
{
    public sealed class Stack<T> : IEnumerable<T>
    {
        private LinkedList<T> _list = new LinkedList<T>();

        public Boolean IsEmpty => this._list.IsEmpty;

        public Int32 Count => this._list.Count;

        public void Push(T item)
        {
            this._list.Add(item);
        }

        public T Pop()
        {
            return this._list.Remove();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
