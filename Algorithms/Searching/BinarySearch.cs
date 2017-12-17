using System;
using System.Collections.Generic;

namespace Algorithms.Searching
{
    public sealed class BinarySearch<TKey, TValue> : IOrderedSymbolTable<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Object[] _keys;
        private TValue[] _values;

        public Int32 Count { get; private set; }

        public Boolean IsEmpty => this.Count == 0;

        public BinarySearch() : this(10)
        {
        }

        public BinarySearch(Int32 capacity)
        {
            this._keys = new Object[capacity];
            this._values = new TValue[capacity];
        }

        public void Add(TKey key, TValue value)
        {
            Int32 rank = this.Rank(key);

            if (rank < this.Count && ((TKey)(this._keys[rank])).CompareTo(key) == 0)
            {
                this._values[rank] = value;

                return;
            }

            if (this.Count == this._keys.Length)
                this.Resize(2 * this._keys.Length);

            for (int j = this.Count; j > rank; j--)
            {
                this._keys[j] = this._keys[j - 1];
                this._values[j] = this._values[j - 1];
            }

            this._keys[rank] = key;
            this._values[rank] = value;

            this.Count++;
        }

        public TKey Ceiling(TKey key)
        {
            Int32 rank = this.Rank(key);

            if (rank != this.Count)
                return (TKey)this._keys[rank];

            throw new KeyNotFoundException();
        }

        public Boolean Contains(TKey key)
        {
            return this.TryGet(key, out _);
        }

        public Int32 CountBetween(TKey low, TKey high)
        {
            if (low.CompareTo(high) > 0)
                return 0;

            if (this.Contains(high))
                return this.Rank(high) - this.Rank(low) + 1;
            else
                return this.Rank(high) - this.Rank(low);
        }

        public void Delete(TKey key)
        {
            if (!this.IsEmpty)
            {
                Int32 rank = this.Rank(key);

                if (rank == this.Count || ((TKey)this._keys[rank]).CompareTo(key) != 0)
                    return;

                for (int i = rank; i < this.Count - 1; i++)
                {
                    this._keys[i] = this._keys[i + 1];
                    this._values[i] = this._values[i + 1];
                }

                this.Count--;

                this._keys[this.Count] = null;
                this._values[this.Count] = default(TValue);

                if (this.Count > 0 && this.Count == this._keys.Length / 4)
                    Resize(this._keys.Length / 2);
            }
        }

        public void DeleteMax()
        {
            this.Delete(this.Max());
        }

        public void DeleteMin()
        {
            this.Delete(this.Min());
        }

        public TKey Floor(TKey key)
        {
            Int32 rank = this.Rank(key);

            if (rank < this.Count && ((TKey)key).CompareTo((TKey)this._keys[rank]) == 0)
                return (TKey)this._keys[rank];

            if (rank != 0)
                return (TKey)this._keys[rank - 1];

            throw new KeyNotFoundException();
        }

        public TValue Get(TKey key)
        {
            if (this.TryGet(key, out TValue value))
                return value;

            throw new KeyNotFoundException();
        }

        public IEnumerable<TKey> Keys()
        {
            return this.Keys(this.Min(), this.Max());
        }

        public IEnumerable<TKey> Keys(TKey low, TKey high)
        {
            Queue<TKey> queue = new Queue<TKey>();

            if (low.CompareTo(high) > 0)
                return queue;

            for (int i = this.Rank(low); i < this.Rank(high); i++)
                queue.Enqueue((TKey)this._keys[i]);

            if (this.Contains(high))
                queue.Enqueue((TKey)this._keys[this.Rank(high)]);

            return queue;
        }

        public TKey Max()
        {
            return (TKey)this._keys[this.Count - 1];
        }

        public TKey Min()
        {
            return (TKey)this._keys[0];
        }

        public Int32 Rank(TKey key)
        {
            Int32 low = 0;
            Int32 high = this.Count - 1;

            while (low <= high)
            {
                Int32 mid = low + (high - low) / 2;
                Int32 cmp = key.CompareTo((TKey)this._keys[mid]);

                if (cmp < 0)
                    high = mid - 1;
                else if (cmp > 0)
                    low = mid + 1;
                else
                    return mid;
            }

            return low;
        }

        public TKey Select(Int32 rank)
        {
            if (rank > this._keys.Length - 1)
                throw new KeyNotFoundException();

            return (TKey)this._keys[rank];
        }

        private void Resize(Int32 capacity)
        {
            Object[] tempKeys = new Object[capacity];
            TValue[] tempValues = new TValue[capacity];

            Array.Copy(this._keys, tempKeys, this.Count);
            Array.Copy(this._values, tempValues, this.Count);

            this._keys = tempKeys;
            this._values = tempValues;
        }

        private Boolean TryGet(TKey key, out TValue value)
        {
            Int32 i = this.Rank(key);

            if (i < this.Count && ((TKey)this._keys[i]).CompareTo(key) == 0)
            {
                value = this._values[i];

                return true;
            }
            else
            {
                value = default(TValue);

                return false;
            }
        }
    }
}
