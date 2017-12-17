using System;
using System.Collections.Generic;

namespace Algorithms.Searching
{
    public sealed class SeparateChainHash<TKey, TValue> : ISymbolTable<TKey, TValue> where TKey : IComparable<TKey>
    {
        private SequentialSearch<TKey, TValue>[] _sequentialSearches;
        private Boolean _countMayHaveChanged;
        private Int32 _count;

        public Int32 Size
        {
            get
            {
                if (this._countMayHaveChanged)
                {
                    this._countMayHaveChanged = false;

                    this._count = 0;

                    for (int i = 0; i < this._sequentialSearches.Length; i++)
                        this._count += this._sequentialSearches[i].Size;
                }

                return this._count;
            }
        }

        public Boolean IsEmpty => this.Size == 0;

        public SeparateChainHash() : this(10)
        {
        }

        public SeparateChainHash(Int32 capacity)
        {
            this._sequentialSearches = new SequentialSearch<TKey, TValue>[capacity];

            for (int i = 0; i < capacity; i++)
                this._sequentialSearches[i] = new SequentialSearch<TKey, TValue>();
        }

        public void Add(TKey key, TValue value)
        {
            this._sequentialSearches[this.Hash(key)].Add(key, value);

            this._countMayHaveChanged = true;
        }

        public TValue Get(TKey key)
        {
            if (this.TryGet(key, out TValue value))
                return value;

            throw new KeyNotFoundException();
        }

        public Boolean Contains(TKey key)
        {
            return this.TryGet(key, out _);
        }

        public void Delete(TKey key)
        {
            if (!this.IsEmpty)
            {
                this._sequentialSearches[this.Hash(key)].Delete(key);

                this._count--;
            }
        }

        public IEnumerable<TKey> Keys()
        {
            IEnumerator<TKey> enumerator;

            for (int i = 0; i < this._sequentialSearches.Length; i++)
            {
                if (!this._sequentialSearches[i].IsEmpty)
                {
                    enumerator = this._sequentialSearches[i].Keys().GetEnumerator();

                    while (enumerator.MoveNext())
                        yield return enumerator.Current;
                }
            }
        }

        private Int32 Hash(TKey key)
        {
            // 0x7fffffff
            // equals
            // 01111111_11111111_11111111_11111111
            // it will clear the signal bit and use the remaining 31 bits on the calculation.

            return (key.GetHashCode() & 0x7fffffff) % this._sequentialSearches.Length;
        }

        private Boolean TryGet(TKey key, out TValue value)
        {
            try
            {
                value = this._sequentialSearches[this.Hash(key)].Get(key);

                return true;
            }
            catch (KeyNotFoundException)
            {
            }

            value = default(TValue);

            return false;
        }
    }
}
