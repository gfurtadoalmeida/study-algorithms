using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Queue
{
    public class ArrayBasedQueue<T> : IQueue<T>
    {
        private readonly T[] _array;

        private int _endOfQueue;
        private int _startOfQueue;
        private int _countItens;

        public bool IsEmpty => this._countItens == 0;

        public bool IsFull => this._countItens == this._array.Length;

        public ArrayBasedQueue(int size)
        {
            if (size < 1)
                throw new ArgumentOutOfRangeException(nameof(size));

            this._array = new T[size];
            this._endOfQueue = -1;
            this._startOfQueue = 0;
        }

        public void Enqueue(T value)
        {
            if (this.IsFull)
                throw new InvalidOperationException();

            this._endOfQueue++;
            this._countItens++;

            if (this._endOfQueue == this._array.Length)
            {
                this._endOfQueue = 0;
            }

            this._array[this._endOfQueue] = value;
        }

        public T Dequeue()
        {
            if (this.IsEmpty)
                throw new InvalidOperationException();

            this._countItens--;

            T value = this._array[this._startOfQueue];

            if (this.IsEmpty)
            {
                this._startOfQueue = -1;
                this._endOfQueue = 0;
            }
            else
            {
                this._startOfQueue++;

                if (this._startOfQueue == this._array.Length)
                {
                    this._startOfQueue = 0;
                }
            }

            return value;
        }

        public T Peek(int position)
        {
            if (position >= this._countItens)
                throw new ArgumentOutOfRangeException(nameof(position));

            position += this._startOfQueue;

            if (position >= this._array.Length)
            {
                position -= this._array.Length;
            }

            return this._array[position];
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this._countItens; i++)
            {
                yield return this.Peek(i);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
