using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Stack
{
    public class ArrayBasedStack<T> : IStack<T>
    {
        private readonly T[] _array;

        private int _position;

        public bool IsEmpty => this._position == -1;

        public bool IsFull => this._position == this._array.Length - 1;

        public ArrayBasedStack(int size)
        {
            if (size < 1)
                throw new ArgumentOutOfRangeException(nameof(size));

            this._array = new T[size];
            this._position = -1;
        }

        public void Push(T value)
        {
            if (this.IsFull)
                throw new InvalidOperationException();

            this._position++;

            this._array[this._position] = value;
        }

        public T Pop()
        {
            if (this.IsEmpty)
                throw new InvalidOperationException();

            T value = this._array[this._position];

            this._array[this._position] = default!;

            this._position--;

            return value;
        }

        public T Peek(int position)
        {
            if (position > this._position)
                throw new ArgumentOutOfRangeException(nameof(position));

            return this._array[this._position - position];
        }

        public IEnumerator<T> GetEnumerator()
        {
            int counter = 0;

            while (counter <= this._position)
            {
                yield return this.Peek(counter);

                counter++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
