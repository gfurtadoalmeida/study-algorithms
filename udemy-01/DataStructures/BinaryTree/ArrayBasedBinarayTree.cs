using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using QE = DataStructures.Queue;
using ST = DataStructures.Stack;

namespace DataStructures.BinaryTree
{
    public class ArrayBasedBinarayTree<T> : IBinaryTree<T>
    {
        private static readonly IEqualityComparer<T> COMPARER = EqualityComparer<T>.Default;

        private readonly T[] _array;

        private int _lastUsedIndex;

        public bool IsEmpty => this._lastUsedIndex == 0;

        public bool IsFull => this._lastUsedIndex == this._array.Length - 1;

        public ArrayBasedBinarayTree(int size)
        {
            if (size < 1)
                throw new ArgumentOutOfRangeException(nameof(size));

            // +1 because index zero is never used.
            this._array = new T[size + 1];
        }

        public void Add(T value)
        {
            if (this.IsFull)
                throw new InvalidOperationException();

            this._lastUsedIndex++;

            this._array[this._lastUsedIndex] = value;
        }

        public bool Contains(T value)
        {
            this.ThrowIfEmpty();

            return this.FindValuePosition(value) > 0;
        }

        public void Delete(T value)
        {
            this.ThrowIfEmpty();

            int position = this.FindValuePosition(value);

            if (position < 1)
                throw new InvalidOperationException();

            this._array[position] = this._array[this._lastUsedIndex];

            this._array[this._lastUsedIndex] = default!;

            this._lastUsedIndex--;
        }

        public IEnumerator<T> GetEnumeratorStack(EnumerationMode enumerationMode)
        {
            if (this.IsEmpty)
                throw new InvalidOperationException();

            return enumerationMode switch
            {
                EnumerationMode.PreOrder => this.GetEnumeratorPreOrderStack(),
                EnumerationMode.InOrder => this.GetEnumeratorInOrderStack(),
                EnumerationMode.PostOrder => this.GetEnumeratorPostOrderStack(),

                // Can't do it recursively.
                EnumerationMode.LevelOrder => this.GetEnumeratorLevelOrder(),

                _ => throw new ArgumentException(nameof(enumerationMode)),
            };
        }

        public IEnumerator<T> GetEnumeratorRecursive(EnumerationMode enumerationMode)
        {
            return enumerationMode switch
            {
                EnumerationMode.PreOrder => this.GetEnumeratorPreOrderRecursive(),
                EnumerationMode.InOrder => this.GetEnumeratorInOrderRecursive(),
                EnumerationMode.PostOrder => this.GetEnumeratorPostOrderRecursive(),

                // Can't do it recursively.
                EnumerationMode.LevelOrder => this.GetEnumeratorLevelOrder(),

                _ => throw new ArgumentException(nameof(enumerationMode)),
            };
        }

        private IEnumerator<T> GetEnumeratorPreOrderStack()
        {
            if (this._lastUsedIndex == 1)
            {
                yield return this._array[this._lastUsedIndex];
            }
            else
            {
                ST.SingleLinkedListBasedStack<int> stack = new ST.SingleLinkedListBasedStack<int>();

                stack.Push(1);

                while (!stack.IsEmpty)
                {
                    int position = stack.Pop();

                    yield return this._array[position];

                    int leftPosition = CalculateLeftPosition(position);
                    int rightPosition = CalculateRightPosition(position);

                    if (rightPosition <= this._lastUsedIndex)
                    {
                        stack.Push(rightPosition);
                    }

                    if (leftPosition <= this._lastUsedIndex)
                    {
                        stack.Push(leftPosition);
                    }
                }
            }
        }

        private IEnumerator<T> GetEnumeratorInOrderStack()
        {
            if (this._lastUsedIndex == 1)
            {
                yield return this._array[this._lastUsedIndex];
            }
            else
            {
                ST.SingleLinkedListBasedStack<int> stack = new ST.SingleLinkedListBasedStack<int>();

                int position = 1;

                while (position > 0 && !stack.IsEmpty)
                {
                    while (position <= this._lastUsedIndex)
                    {
                        stack.Push(position);

                        position = CalculateLeftPosition(position);
                    }

                    position = stack.Pop();

                    yield return this._array[position];

                    position = CalculateRightPosition(position);
                }
            }
        }

        private IEnumerator<T> GetEnumeratorPostOrderStack()
        {
            if (this._lastUsedIndex == 1)
            {
                yield return this._array[this._lastUsedIndex];
            }
            else
            {
                ST.SingleLinkedListBasedStack<int> stack = new ST.SingleLinkedListBasedStack<int>();

                int position = 1;

                while (position >= 0 && !stack.IsEmpty)
                {
                    int rightPosition;

                    while (position > 0 && position <= this._lastUsedIndex)
                    {
                        rightPosition = CalculateRightPosition(position);

                        if (rightPosition <= this._lastUsedIndex)
                        {
                            stack.Push(rightPosition);
                        }

                        stack.Push(position);

                        position = CalculateLeftPosition(position);
                    }

                    position = stack.Pop();

                    rightPosition = CalculateRightPosition(position);

                    if (!stack.IsEmpty && rightPosition == stack.Peek(0))
                    {
                        stack.Pop();

                        stack.Push(position);

                        position = rightPosition;
                    }
                    else
                    {
                        yield return this._array[position];

                        position = 0;
                    }
                }
            }
        }

        private IEnumerator<T> GetEnumeratorPreOrderRecursive()
        {
            QE.ArrayBasedQueue<T> queue = new QE.ArrayBasedQueue<T>(this._array.Length);

            Traverse(1);

            return queue.GetEnumerator();

            void Traverse(int position)
            {
                if (position > this._lastUsedIndex)
                    return;

                queue.Enqueue(this._array[position]);

                Traverse(position * 2);
                Traverse((position * 2) + 1);
            }
        }

        private IEnumerator<T> GetEnumeratorInOrderRecursive()
        {
            QE.ArrayBasedQueue<T> queue = new QE.ArrayBasedQueue<T>(this._array.Length);

            Traverse(1);

            return queue.GetEnumerator();

            void Traverse(int position)
            {
                if (position > this._lastUsedIndex)
                    return;

                Traverse(position * 2);

                queue.Enqueue(this._array[position]);

                Traverse((position * 2) + 1);
            }
        }

        private IEnumerator<T> GetEnumeratorPostOrderRecursive()
        {
            QE.ArrayBasedQueue<T> queue = new QE.ArrayBasedQueue<T>(this._array.Length);

            Traverse(1);

            return queue.GetEnumerator();

            void Traverse(int position)
            {
                if (position > this._lastUsedIndex)
                    return;

                Traverse(position * 2);

                Traverse((position * 2) + 1);

                queue.Enqueue(this._array[position]);
            }
        }

        private IEnumerator<T> GetEnumeratorLevelOrder()
        {
            for (int i = 1; i <= this._lastUsedIndex; i++)
            {
                yield return this._array[i];
            }
        }

        private void ThrowIfEmpty()
        {
            if (this.IsEmpty)
                throw new InvalidOperationException();
        }

        private int FindValuePosition(T value)
        {
            for (int i = 0; i <= this._lastUsedIndex; i++)
            {
                if (COMPARER.Equals(this._array[i], value))
                {
                    return i;
                }
            }

            return -1;
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
    }
}
