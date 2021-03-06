using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.LinkedList
{
    public class CircularSingleLinkedList<T> : ILinkedList<T>
    {
        public Node<T>? Head { get; private set; }

        public Node<T>? Tail { get; private set; }

        public bool IsEmpty => this.Count == 0;

        public int Count { get; private set; }

        public void InsertAt(int position, T value)
        {
            this.ThrowIfInvalidPosition(position);

            if (position == 0)
            {
                this.Head = new Node<T>(value, this.Head);

                if (this.Tail == null)
                {
                    this.Tail = this.Head;
                }

                this.Tail!.Next = this.Head!;
            }
            else if (position >= this.Count - 1)
            {
                Node<T> node = new Node<T>(value, null);

                this.Tail!.Next = node;
                this.Tail = node;
                this.Tail.Next = this.Head;
            }
            else
            {
                Node<T> parent = this.Head!;

                for (int i = 1; i < position; i++)
                {
                    parent = parent.Next!;
                }

                parent!.Next = new Node<T>(value, parent.Next);
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
                Node<T> parent = this.Head!;

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
                if (this.Head!.Next == this.Head)
                {
                    this.Head = null;
                    this.Tail!.Next = null;
                    this.Tail = null;
                }
                else
                {
                    Node<T> head = this.Head!;

                    this.Head = head.Next;

                    head.Next = null;

                    this.Tail!.Next = this.Head;
                }
            }
            else if (position >= this.Count - 1)
            {
                Node<T> tailParent = this.Head!;

                for (int i = 1; i < this.Count - 1; i++)
                {
                    tailParent = tailParent.Next!;
                }

                tailParent.Next!.Next = null;
                tailParent.Next = null;

                this.Tail = tailParent;
                this.Tail.Next = this.Head;
            }
            else
            {
                Node<T> parent = this.Head!;

                for (int i = 1; i < position; i++)
                {
                    parent = parent.Next!;
                }

                Node<T> nodeToRemove = parent.Next!;

                parent.Next = nodeToRemove.Next;

                nodeToRemove.Next = null;
            }

            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> node = this.Head!;

            while (node != this.Tail)
            {
                yield return node.Value;

                node = node.Next!;
            }

            yield return this.Tail.Value;
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