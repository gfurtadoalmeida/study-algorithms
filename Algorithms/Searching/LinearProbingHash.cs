using System;
using System.Collections.Generic;

namespace Algorithms.Searching
{
    /// <summary>
    /// Built-in types with hash cached values.
    /// </summary>
    public sealed class LinearProbingHash<TKey, TValue> : ISymbolTable<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Int32 _tableSize;
        private Object[] _keys;
        private TValue[] _values;

        /// <summary>
        /// Number of keys in table.
        /// </summary>
        public Int32 Count { get; private set; }

        public Boolean IsEmpty => this.Count == 0;
        
        public LinearProbingHash() : this(16)
        {
        }

        public LinearProbingHash(Int32 capacity)
        {
            this._keys = new Object[capacity];
            this._values = new TValue[capacity];
            this._tableSize = capacity;
        }

        public void Add(TKey key, TValue value)
        {
            if (this.Count >= this._tableSize / 2)
            {
                this.Resize(2 * this._tableSize);
            }

            Int32 i;

            for (i = this.Hash(key); this._keys[i] != null; i = (i + 1) % this._tableSize)
            {
                if (this._keys[i].Equals(key))
                {
                    this._values[i] = value;

                    return;
                }
            }

            this._keys[i] = key;
            this._values[i] = value;

            this.Count++;
        }

        public Boolean Contains(TKey key)
        {
            return this.TryGet(key, out _);
        }

        public void Delete(TKey key)
        {
            if (!this.IsEmpty && this.Contains(key))
            {
                Int32 hash = this.Hash(key);

                while (!key.Equals(this._keys[hash]))
                {
                    hash = (hash + 1) % this._tableSize;
                }

                this._keys[hash] = null;
                this._values[hash] = default;

                hash = (hash + 1) % this._tableSize;

                while (this._keys[hash] != null)
                {
                    Object keyToRedo = this._keys[hash];
                    TValue valToRedo = this._values[hash];

                    this._keys[hash] = null;
                    this._values[hash] = default;

                    this.Count--;

                    this.Add((TKey)keyToRedo, valToRedo);

                    hash = (hash + 1) % this._tableSize;
                }

                this.Count--;

                if (this.Count > 0 && this.Count == this._tableSize / 8)
                {
                    this.Resize(this._tableSize / 2);
                }
            }
        }

        public TValue Get(TKey key)
        {
            if (this.TryGet(key, out TValue value))
                return value;

            throw new KeyNotFoundException();
        }

        public IEnumerable<TKey> Keys()
        {
            for (int i = 0; i < this._keys.Length; i++)
            {
                if (this._keys[i] != null)
                {
                    yield return (TKey)this._keys[i];
                }
            }
        }

        private Int32 Hash(TKey key)
        {
            // 0x7fffffff
            // equals
            // 01111111_11111111_11111111_11111111
            // it will clear the signal bit and use the remaining 31 bits on the calculation.

            return (key.GetHashCode() & 0x7fffffff) % this._tableSize;
        }

        private void Resize(Int32 size)
        {
            LinearProbingHash<TKey, TValue> temp = new LinearProbingHash<TKey, TValue>(size);

            for (int i = 0; i < this._tableSize; i++)
            {
                if (this._keys[i] != null)
                {
                    temp.Add((TKey)this._keys[i], this._values[i]);
                }
            }

            this._keys = temp._keys;
            this._values = temp._values;
            this._tableSize = temp._tableSize;
        }

        private Boolean TryGet(TKey key, out TValue value)
        {
            for (int i = this.Hash(key); this._keys[i] != null; i = (i + 1) % this._tableSize)
            {
                if (this._keys[i].Equals(key))
                {
                    value = this._values[i];

                    return true;
                }
            }

            value = default;

            return false;
        }
    }
}
