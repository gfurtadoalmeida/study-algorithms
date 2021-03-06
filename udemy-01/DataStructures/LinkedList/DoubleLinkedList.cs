using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.LinkedList
{
    public class DoubleLinkedList<T> : ILinkedList<T>
    {
        public DoubleNode<T>? Head { get; private set; }

        public DoubleNode<T>? Tail { get; private set; }

        public bool IsEmpty => this.Count == 0;

        public int Count { get; private set; }

        public void InsertAt(int position, T value)
        {
            this.ThrowIfInvalidPosition(position);

            if (position == 0)
            {
                if (this.Head == null)
                {
                    DoubleNode<T> node = new DoubleNode<T>(value, null, null);

                    this.Head = node;
                    this.Tail = node;
                }
                else
                {
                    DoubleNode<T> node = new DoubleNode<T>(value, null, this.Head);

                    this.Head.Previous = node;
                    this.Head = node;
                }
            }
            else if (position >= this.Count - 1)
            {
                DoubleNode<T> node = new DoubleNode<T>(value, this.Tail, null);

                this.Tail!.Next = node;
                this.Tail = node;
            }
            else
            {
                DoubleNode<T> parent = this.Head!;

                for (int i = 1; i < position; i++)
                {
                    parent = parent.Next!;
                }

                DoubleNode<T> node = new DoubleNode<T>(value, parent, parent.Next);
                parent.Next!.Previous = node;
                parent.Next = node;
            }

            this.Count++;
        }

        public T GetAt(int position)
        {
            this.ThrowIfEmpty();
            this.ThrowIfInvalidPosition(position);

            T value;

            if (position == 0)
            {
                value = this.Head!.Value;
            }
            else if (position >= this.Count - 1)
            {
                value = this.Tail!.Value;
            }
            else
            {
                DoubleNode<T> parent = this.Head!;

                for (int i = 1; i <= position; i++)
                {
                    parent = parent.Next!;
                }

                value = parent!.Value;
            }

            return value;
        }

        public bool Contains(T value)
        {
            if (this.IsEmpty)
                return false;

            foreach (T nodeData in this)
            {
                if (nodeData != null && nodeData.Equals(value))
                {
                    return true;
                }
            }

            return false;
        }

        public void DeleteAt(int position)
        {
            this.ThrowIfEmpty();
            this.ThrowIfInvalidPosition(position);

            if (position == 0)
            {
                DoubleNode<T> head = this.Head!;

                if (head!.Next == null)
                {
                    this.Head!.Next = null;
                    this.Head = null;
                    this.Tail!.Previous = null;
                    this.Tail = null;
                }
                else
                {
                    this.Head = head.Next;
                    this.Head.Previous = null;

                    head.Next = null;
                }
            }
            else if (position >= this.Count - 1)
            {
                DoubleNode<T> tailParent = this.Tail!.Previous!;

                tailParent.Next = null;

                this.Tail!.Previous = null;
                this.Tail = tailParent;
            }
            else
            {
                DoubleNode<T> parent = this.Head!;

                for (int i = 1; i < position; i++)
                {
                    parent = parent.Next!;
                }

                DoubleNode<T> nodeToRemove = parent.Next!;

                parent.Next = nodeToRemove.Next;
                parent.Next!.Previous = parent;

                nodeToRemove.Previous = null;
                nodeToRemove.Next = null;
                nodeToRemove.Next = null;
            }

            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            DoubleNode<T> node = this.Head!;

            while (node != null)
            {
                yield return node.Value;

                node = node.Next!;
            }
        }

        public IEnumerator<T> GetReverseEnumerator()
        {
            DoubleNode<T> node = this.Tail!;

            while (node != null)
            {
                yield return node.Value;

                node = node.Previous!;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void ThrowIfEmpty()
        {
            if (this.IsEmpty)
                throw new InvalidOperationException();
        }

        private void ThrowIfInvalidPosition(int position)
        {
            if (position < 0 || (position > 0 && position > this.Count))
                throw new ArgumentOutOfRangeException(nameof(position));
        }
    }
}