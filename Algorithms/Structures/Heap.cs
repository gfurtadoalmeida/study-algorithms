using System;

namespace Algorithms.Structures
{
    public enum HeapType
    {
        /// <summary>
        /// Biggest item on top.
        /// </summary>
        Max = 1,

        /// <summary>
        /// Smallest item on top.
        /// </summary>
        Min = 2
    }

    public sealed class Heap<T> where T : IComparable<T>
    {
        // Binary heap is a binary tree structure with the following properties:
        //   A - Ordered.
        //   B - The key in each node is larger than or equal to the keys in that node’s two children (if any).
        //   C - On children nodes, the left's value is lower in order than the right's value.
        // Types:
        //   MaxHeap: Biggets item on top.
        //   MinHeap: Lowest item on top.
        //
        // Usefull for creating priority queues.

        // Comparisons:
        // When comparing two itens with IComparable<T>.CompareTo(), we get:
        //   -1: item A is less than item B.
        //    0: item A is equal to item B.
        //    1: item A is greater than item B.
        //
        // Hence the following constants used in the method IsNotInOrder (through _heapOrderComparer field):
        // MAXHEAP_ORDER_COMPARER: in MAX HEAP, an item A is out of order if it's value is lower in order than item B.
        // MINHEAP_ORDER_COMPARER: in MAX HEAP, an item A is out of order if it's value is greater in order than item B.

        private const Int32 MAXHEAP_ORDER_COMPARER = -1;
        private const Int32 MINHEAP_ORDER_COMPARER = 1;
        private const Int32 BINHEAP_START_INDEX = 1;

        // We're using a resizable array', so we'd better keep
        // track of the "true last index".
        private Int32 _lastIndex;
        private IComparable<T>[] _array;
        private readonly Int32 _heapOrderComparer;

        public Boolean IsEmpty => this._lastIndex == 0;

        public Int32 Count => this._lastIndex;

        public Heap(Int32 capacity) : this(HeapType.Max, capacity)
        {
        }

        public Heap(HeapType heapType, Int32 capacity)
        {
            // Always add 1 because a binary heap starts at index 1.
            this._array = new IComparable<T>[capacity + 1];

            this._heapOrderComparer = heapType switch
            {
                HeapType.Max => MAXHEAP_ORDER_COMPARER,
                HeapType.Min => MINHEAP_ORDER_COMPARER,
                _ => throw new ArgumentException($"Heap type '{heapType}' not implemented.", nameof(heapType)),
            };
        }

        public void Insert(T item)
        {
            // When inserting, we always insert at the end of the array
            // and swim the item up.
            this._lastIndex++;

            if (this._lastIndex == this._array.Length)
            {
                this.DoubleArray();
            }

            this._array[this._lastIndex] = item;

            this.Swim(this._lastIndex);
        }

        public T RemoveTop()
        {
            // When removing, as we want the greater in order item:
            // 1 - Remove the first item (greater in order).
            // 2 - Exchange the last item (the lowest in order/hierarchy) with
            //     the position of the first item.
            // 3 - Set the last item to default of the type.
            // 4 - We sink the first item until we find its place.

            T topItem = (T)this._array[BINHEAP_START_INDEX];

            this.Exchange(1, this._lastIndex--);

            this._array[this._lastIndex + 1] = null;

            this.Sink(1);

            if (this._lastIndex > BINHEAP_START_INDEX && this._lastIndex <= this._array.Length / 4)
            {
                this.HalveArray();
            }

            return topItem;
        }

        public T Top()
        {
            if (this.IsEmpty)
                throw new InvalidOperationException();

            return (T)this._array[BINHEAP_START_INDEX];
        }

        private void Swim(Int32 indexItem)
        {
            // Swim: go higher in the hierarchy.

            // On a binary tree, the parent index of an item is:
            // parentIndex = currentIndex / 2

            while (indexItem > BINHEAP_START_INDEX && this.IsNotInOrder(indexItem / 2, indexItem))
            {
                this.Exchange(indexItem / 2, indexItem);

                indexItem /= 2;
            }
        }

        private void Sink(Int32 indexItem)
        {
            // Sink: go lower in the hierarchy.

            // On a binary tree, the indexes of a parent's children are:
            // indexLeftChild = currentIndex * 2
            // indexRightChild = (currentIndex * 2) + 1

            Int32 indexChild = 2 * indexItem;

            while (indexChild <= this._lastIndex)
            {
                // Compare the left and right child.
                // If the left child has a lower order than then right child, we'll use the right child.
                if (indexChild < this._lastIndex && this.IsNotInOrder(indexChild, indexChild + 1))
                {
                    indexChild++;
                }

                // Compare the parent with the child.
                if (this.IsNotInOrder(indexItem, indexChild))
                {
                    this.Exchange(indexItem, indexChild);

                    indexItem = indexChild;

                    indexChild *= 2;
                }
                else
                    break; // We've reached order.
            }
        }

        private Boolean IsNotInOrder(Int32 indexA, Int32 indexB)
        {
            return this._array[indexA].CompareTo((T)this._array[indexB]) == this._heapOrderComparer;
        }

        private void Exchange(Int32 indexTarget, Int32 indexSource)
        {
            T item = (T)this._array[indexTarget];

            this._array[indexTarget] = this._array[indexSource];
            this._array[indexSource] = item;
        }

        private void DoubleArray()
        {
            this.ResizeArray(this._array.Length * 2);
        }

        private void HalveArray()
        {
            this.ResizeArray(this._array.Length / 2);
        }

        private void ResizeArray(Int32 size)
        {
            IComparable<T>[] temp = new IComparable<T>[size];

            Array.ConstrainedCopy(this._array,
                                  0,
                                  temp,
                                  0,
                                  this._array.Length > size ? size : this._array.Length);

            Array.Clear(this._array, 0, this._array.Length);

            this._array = temp;
        }
    }
}
