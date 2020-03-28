using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DataStructures.Heap
{
    public class IndexedHeap<T> : IHeap<T> where T : IComparable<T>
    {
        private static readonly IComparer<T> COMPARER = Comparer<T>.Default;

        private readonly T[] _array;
        private readonly Dictionary<T, int> _index;

        private readonly Action<int> heapifyBottomToTopMethod;
        private readonly Action<int> heapifyTopToBottomMethod;

        public bool IsEmpty => this.Size == 0;

        public bool IsFull => this.Size == this._array.Length - 1;

        public int Size { get; private set; }

        public IndexedHeap(HeapMode mode, int size)
        {
            if (size < 1)
                throw new ArgumentOutOfRangeException(nameof(size));

            switch (mode)
            {
                case HeapMode.MinHeap:
                    this.heapifyBottomToTopMethod = this.HeapifyMinBottomToTop;
                    this.heapifyTopToBottomMethod = this.HeapifyMinTopToBottom;
                    break;
                case HeapMode.MaxHeap:
                    this.heapifyBottomToTopMethod = this.HeapifyMaxBottomToTop;
                    this.heapifyTopToBottomMethod = this.HeapifyMaxTopToBottom;
                    break;

                default:
                    throw new ArgumentException(nameof(mode));
            }

            // +1 because index zero is never used.
            this._array = new T[size + 1];
            this._index = new Dictionary<T, int>(size);
        }

        public void Add(T value)
        {
            if (this.IsFull)
                throw new InvalidOperationException();

            this.Size++;

            this._array[this.Size] = value;

            this._index.Add(value, this.Size);

            this.heapifyBottomToTopMethod(this.Size);
        }

        public T PeekTop()
        {
            this.ThrowIfEmpty();

            return this._array[1];
        }

        public T ExtractTop()
        {
            this.ThrowIfEmpty();

            T value = this._array[1];

            this._array[1] = this._array[this.Size];

            this._array[this.Size] = default!;

            this.Size--;

            this._index.Remove(value);

            this.heapifyTopToBottomMethod(1);

            return value;
        }

        public int GetIndex(T value)
        {
            this.ThrowIfNotExists(value);

            return this._index[value];
        }

        public void Update(int index, T value)
        {
            if (index < 1 || index > this.Size)
                throw new ArgumentOutOfRangeException(nameof(index));

            // As there is no reference for value types (obvious),
            // we first need to get the old value using the
            // index and then remove the old value and add
            // the new one.
            if (value is ValueType)
            {
                T oldValue = this._array[index];

                this._index.Remove(oldValue);
                this._index.Add(value, index);
            }
            else
            {
                this.ThrowIfNotExists(value);
            }

            this._array[index] = value;

            this.heapifyTopToBottomMethod(index);
            this.heapifyBottomToTopMethod(index);
        }

        private void ThrowIfEmpty()
        {
            if (this.IsEmpty)
                throw new InvalidOperationException();
        }

        private void ThrowIfNotExists(T value)
        {
            if (!this._index.ContainsKey(value))
                throw new ArgumentException(nameof(value));
        }

        private void HeapifyMinBottomToTop(int index)
        {
            int parentIndex = CalculateParentPosition(index);

            if (index <= 1)
                return;

            T current = this._array[index];
            T parent = this._array[parentIndex];

            if (COMPARER.Compare(this._array[index], this._array[parentIndex]) < 0)
            {
                this._index[current] = parentIndex;
                this._index[parent] = index;

                T temp = this._array[index];

                this._array[index] = this._array[parentIndex];
                this._array[parentIndex] = temp;
            }

            this.HeapifyMinBottomToTop(parentIndex);
        }

        private void HeapifyMaxBottomToTop(int index)
        {
            int parentIndex = CalculateParentPosition(index);

            if (index <= 1)
                return;

            T current = this._array[index];
            T parent = this._array[parentIndex];

            if (COMPARER.Compare(this._array[index], this._array[parentIndex]) > 0)
            {
                this._index[current] = parentIndex;
                this._index[parent] = index;

                T temp = this._array[index];

                this._array[index] = this._array[parentIndex];
                this._array[parentIndex] = temp;
            }

            this.HeapifyMaxBottomToTop(parentIndex);
        }

        private void HeapifyMinTopToBottom(int index)
        {
            int left = CalculateLeftPosition(index);
            int right = CalculateRightPosition(index);
            int smallestChildIndex;

            if (this.Size < left)
                return;

            // There is only left child of this node.
            if (this.Size == left)
            {
                smallestChildIndex = left;
            }
            else // If both children are there.
            {
                if (COMPARER.Compare(this._array[left], this._array[right]) < 0)
                {
                    smallestChildIndex = left;
                }
                else
                {
                    smallestChildIndex = right;
                }
            }

            T current = this._array[index];
            T smallestChild = this._array[smallestChildIndex];

            // If parent is greater than smallest child, then swap.
            if (COMPARER.Compare(this._array[index], this._array[smallestChildIndex]) > 0)
            {
                this._index[current] = smallestChildIndex;
                this._index[smallestChild] = index;

                T temp = this._array[index];

                this._array[index] = this._array[smallestChildIndex];
                this._array[smallestChildIndex] = temp;
            }

            this.HeapifyMinTopToBottom(smallestChildIndex);
        }

        private void HeapifyMaxTopToBottom(int index)
        {
            int left = CalculateLeftPosition(index);
            int right = CalculateRightPosition(index);
            int smallestChildIndex;

            if (this.Size < left)
                return;

            // There is only left child of this node.
            if (this.Size == left)
            {
                smallestChildIndex = left;
            }
            else // If both children are there.
            {
                if (COMPARER.Compare(this._array[left], this._array[right]) < 0)
                {
                    smallestChildIndex = left;
                }
                else
                {
                    smallestChildIndex = right;
                }
            }

            T current = this._array[index];
            T smallestChild = this._array[smallestChildIndex];

            // If parent is greater than smallest child, then swap.
            if (COMPARER.Compare(current, smallestChild) < 0)
            {
                this._index[current] = smallestChildIndex;
                this._index[smallestChild] = index;

                T temp = this._array[index];

                this._array[index] = this._array[smallestChildIndex];
                this._array[smallestChildIndex] = temp;
            }

            this.HeapifyMaxTopToBottom(smallestChildIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CalculateLeftPosition(int parentPosition)
        {
            return parentPosition * 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CalculateRightPosition(int parentPosition)
        {
            return (parentPosition * 2) + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CalculateParentPosition(int index)
        {
            return index / 2;
        }
    }
}
