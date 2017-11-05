using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms.Structures
{
    public sealed class DoubleLinkedList<T> : IEnumerable<T>
    {
        private Node _head;
        private Int32 _itensCount;

        public Boolean IsEmpty => this._head == null;
        public Int32 Count => this._itensCount;

        public void Add(T item)
        {
            if (this.IsEmpty)
                this._head = new Node(item, null, null);
            else
            {
                this._head = new Node(item, null, this._head);
                this._head.Next.Previous = this._head;
            }

            this._itensCount++;
        }

        public void AddAfter(T parentItem, T childItem)
        {
            Node parentNode = this.Find(parentItem);

            if (parentNode == null)
                throw new Exception("Parent item not found.");

            Node childNode = new Node(childItem, parentNode, parentNode.Next);

            if (parentNode.Next != null)
                parentNode.Next.Previous = childNode;

            parentNode.Next = childNode;

            this._itensCount++;
        }

        public void AddBefore(T parentItem, T grandParentItem)
        {
            Node parentNode = this.Find(parentItem);

            if (parentNode == null)
                throw new Exception("Parent item not found.");

            Node grandParentNode = new Node(grandParentItem, parentNode.Previous, parentNode);

            if (parentNode.Previous != null)
                parentNode.Previous.Next = grandParentNode;

            parentNode.Previous = grandParentNode;

            if (parentNode == this._head)
                this._head = grandParentNode;

            this._itensCount++;
        }

        public T Remove()
        {
            if (this.IsEmpty)
                throw new InvalidOperationException("No more itens to be removed.");

            T item = this._head.Value;

            this._head = this._head.Next;
            this._itensCount--;

            return item;
        }

        public void Remove(T item)
        {
            if (!this.IsEmpty)
            {
                Node node = this.Find(item);

                if (node != null)
                {
                    if (node == this._head)
                    {
                        this._head = this._head.Next;

                        if (this._head != null)
                            this._head.Previous = null;
                    }
                    else
                        node.Previous.Next = node.Next;

                    this._itensCount--;
                }
            }
        }

        public Boolean Contains(T item)
        {
            return this.Find(item) != null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = this._head;

            while (current != null)
            {
                yield return current.Value;

                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private Node Find(T item)
        {
            Node current = this._head;

            while (current != null)
            {
                if (current.Value.Equals(item))
                    break;

                current = current.Next;
            }

            return current;
        }

        private sealed class Node
        {
            public T Value;
            public Node Previous;
            public Node Next;

            public Node()
            {
            }

            public Node(T value, Node previous, Node next)
            {
                this.Value = value;
                this.Previous = previous;
                this.Next = next;
            }
        }
    }
}
