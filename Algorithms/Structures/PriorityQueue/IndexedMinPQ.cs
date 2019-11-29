using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms.Structures.PriorityQueue
{
    public sealed class IndexedMinPQ<T> : IEnumerable<Int32> where T : IComparable<T>
    {
        private readonly Int32[] _binHeap;        // Binary heap using 1-based indexing.
        private readonly Int32[] _indexList;      // List containing all the indexes.
        private readonly IComparable<T>[] _itens; // _itens[index] = priority of index

        public Boolean IsEmpty => this.Count == 0;

        public Int32 Count { get; private set; }

        public IndexedMinPQ(Int32 capacity)
        {
            this.Count = 0;

            this._itens = new IComparable<T>[capacity + 1];
            this._binHeap = new Int32[capacity + 1];
            this._indexList = new Int32[capacity + 1];

            for (int i = 0; i <= capacity; i++)
            {
                this._indexList[i] = -1;
            }
        }

        public void Add(Int32 index, T item)
        {
            if (this.Contains(index))
                throw new Exception("Index is already in the priority queue.");

            this.Count++;

            this._indexList[index] = this.Count;
            this._binHeap[this.Count] = index;
            this._itens[index] = item;

            this.Swim(this.Count);
        }

        public Boolean Contains(Int32 index)
        {
            return this._indexList[index] != -1;
        }

        public void Delete(Int32 index)
        {
            if (!this.Contains(index))
                throw new Exception("Index is not in the priority queue.");

            Int32 i = this._indexList[index];

            this.Exchange(i, this.Count--);
            this.Swim(i);
            this.Sink(i);

            this._itens[i] = null;
            this._indexList[i] = -1;
        }

        public Int32 DeleteMin()
        {
            if (this.Count == 0)
                throw new InvalidOperationException();

            Int32 min = this._binHeap[1];

            this.Exchange(1, this.Count--);
            this.Sink(1);

            this._indexList[min] = -1;
            this._itens[min] = null;
            this._binHeap[this.Count + 1] = -1;

            return min;
        }

        public T ItemAt(Int32 index)
        {
            if (!this.Contains(index))
                throw new Exception("Index is not in the priority queue.");

            return (T)this._itens[index];
        }

        public void Change(Int32 index, T item)
        {
            if (!this.Contains(index))
                throw new Exception("Index is not in the priority queue.");

            this._itens[index] = item;

            this.Swim(this._indexList[index]);
            this.Sink(this._indexList[index]);
        }

        public void IncreaseItem(Int32 index, T item)
        {
            if (!this.Contains(index))
                throw new Exception("Index is not in the priority queue.");

            if (this._itens[index].CompareTo(item) >= 0)
                throw new Exception("Calling IncreaseItem() with given argument would not strictly increase the item.");

            this._itens[index] = item;

            this.Sink(this._indexList[index]);
        }

        public void DecreaseItem(Int32 index, T item)
        {
            if (!this.Contains(index))
                throw new Exception("Index is not in the priority queue.");

            if (this._itens[index].CompareTo(item) <= 0)
                throw new InvalidOperationException("Calling DecreaseItem() with given argument would not strictly decrease the item.");

            this._itens[index] = item;

            this.Swim(this._indexList[index]);
        }

        public Int32 MinIndex()
        {
            if (this.Count == 0)
                throw new InvalidOperationException();

            return this._binHeap[1];
        }

        public T MinItem()
        {
            if (this.Count == 0)
                throw new InvalidOperationException();

            return (T)this._itens[this._binHeap[1]];
        }

        public IEnumerator<Int32> GetEnumerator()
        {
            IndexedMinPQ<T> copy = new IndexedMinPQ<T>(this._binHeap.Length - 1);

            for (int i = 1; i <= this.Count; i++)
            {
                copy.Add(this._binHeap[i], (T)this._itens[this._binHeap[i]]);
            }

            while (!copy.IsEmpty)
                yield return copy.DeleteMin();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private Boolean IsGreater(Int32 i, Int32 j)
        {
            return this._itens[this._binHeap[i]].CompareTo((T)this._itens[this._binHeap[j]]) > 0;
        }

        private void Exchange(Int32 i, Int32 j)
        {
            Int32 swap = this._binHeap[i];

            this._binHeap[i] = this._binHeap[j];
            this._binHeap[j] = swap;
            this._indexList[this._binHeap[i]] = i;
            this._indexList[this._binHeap[j]] = j;
        }

        private void Swim(Int32 index)
        {
            while (index > 1 && this.IsGreater(index / 2, index))
            {
                this.Exchange(index, index / 2);

                index /= 2;
            }
        }

        private void Sink(Int32 index)
        {
            while (2 * index <= this.Count)
            {
                Int32 i = 2 * index;

                if (i < this.Count && this.IsGreater(i, i + 1))
                {
                    i++;
                }

                if (!this.IsGreater(index, i))
                    break;

                this.Exchange(index, i);

                index = i;
            }
        }
    }
}
