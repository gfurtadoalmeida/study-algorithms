using System;

namespace Algorithms.Structures
{
    public sealed class Stack<T>
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
    }
}
