using System;
using System.Runtime.CompilerServices;
using DataStructures.Hash.Functions;

namespace DataStructures.Hash
{
    public abstract class OpenAddressingHashTable<T> : IHashTable<T> where T : class
    {
        private const double LOAD_FACTOR = 0.75;
        private const double UNLOAD_FACTOR = 1.0 - LOAD_FACTOR;
        private T[] _array;

        protected IHashFunction HashFunction { get; private set; }

        public int Size => this._array.Length;

        public int Count { get; private set; }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        protected OpenAddressingHashTable(int size, IHashFunction hashFunction)
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
            // Disable CS8618  because it was complaining about 
            // "_array" not being assigned even though it is assigned
            // in the "Resize" method.

            if (size < 1)
                throw new ArgumentOutOfRangeException(nameof(size));

            // Even numbers works best with the resize logic.
            if (size % 2 != 0)
                throw new ArgumentException(nameof(size));

            this.HashFunction = hashFunction ?? throw new ArgumentNullException(nameof(hashFunction));

            this.Resize(size);
        }

        protected abstract int FindIndexFor(T value, T[] array);

        protected abstract int FindIndexOf(T value, T[] array);

        public void Add(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (this.MustIncreaseArray())
            {
                this.Resize(this.Size * 2);
            }

            this._array[this.FindIndexFor(value, this._array)] = value;

            this.Count++;
        }

        public bool Contains(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return this.FindIndexOf(value, this._array) > -1;
        }

        public void Delete(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            int index = this.FindIndexOf(value, this._array);

            if (index > -1)
            {
                this._array[index] = null!;

                this.Count--;
            }

            if (this.MustDecreaseArray())
            {
                this.Resize(this.Size / 2);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool MustIncreaseArray()
        {
            return (this.Count * 1.0) / this.Size >= LOAD_FACTOR;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool MustDecreaseArray()
        {
            return (this.Count * 1.0) / this.Size <= UNLOAD_FACTOR;
        }

        private void Resize(int newSize)
        {
            if (this._array == null)
            {
                this._array = new T[newSize];
            }
            else
            {
                T[] newArray = new T[newSize];

                foreach (T value in this._array)
                {
                    if (value != null)
                    {
                        newArray[this.FindIndexFor(value, newArray)] = value;
                    }
                }

                this._array = newArray;
            }
        }
    }
}
