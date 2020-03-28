using System;
using System.Collections;
using System.Collections.Generic;
using BT = DataStructures.BinaryTree;
using QE = DataStructures.Queue;

namespace DataStructures.BinarySearchTree
{
    public class BinarySearchTree<T> : IBinarySearchTree<T>
    {
        private static readonly IComparer<T> COMPARER = Comparer<T>.Default;

        private BT.Node<T>? _root;

        public bool IsEmpty => this._root == null;

        public void Add(T value)
        {
            if (this.IsEmpty)
            {
                this._root = new BT.Node<T>(value);
            }
            else
            {
                BT.Node<T> current = this._root!;

                do
                {
                    if (COMPARER.Compare(value, current.Value) < 1)
                    {
                        if (current.Left == null)
                        {
                            current.Left = new BT.Node<T>(value);

                            break;
                        }

                        current = current.Left;
                    }
                    else
                    {
                        if (current.Right == null)
                        {
                            current.Right = new BT.Node<T>(value);

                            break;
                        }

                        current = current.Right;
                    }
                } while (true);
            }
        }

        public BT.Node<T>? Get(T value)
        {
            this.ThrowIfEmpty();

            return this.FindNode(value);
        }

        public bool Contains(T value)
        {
            this.ThrowIfEmpty();

            return this.FindNode(value) != null;
        }

        public void Delete(T value)
        {
            this.ThrowIfEmpty();

            BT.Node<T>? nodeToDelete = this.FindNode(value);

            if (nodeToDelete == null)
                throw new InvalidOperationException();

            this._root = Delete(this._root!, value);

            BT.Node<T>? Delete(BT.Node<T>? current, T value)
            {
                if (current == null)
                    return null;

                int comparison = COMPARER.Compare(value, current.Value);

                if (comparison < 0)
                {
                    current.Left = Delete(current.Left, value);
                }
                else if (comparison > 0)
                {
                    current.Right = Delete(current.Right, value);
                }
                else
                {
                    if (current.HasAllChildren)
                    {
                        BT.Node<T> temp = current;
                        BT.Node<T> minNodeForRight = this.FindMinimumElement(temp.Right!);

                        current.Value = minNodeForRight.Value;

                        current.Right = Delete(current.Right, minNodeForRight.Value);
                    }
                    else if (current.Left != null)
                    {
                        current = current.Left;
                    }
                    else if (current.Right != null)
                    {
                        current = current.Right;
                    }
                    else
                    {
                        current = null;
                    }
                }

                return current;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            QE.SingleLinkedListBasedQueue<BT.Node<T>> queue = new QE.SingleLinkedListBasedQueue<BT.Node<T>>();

            queue.Enqueue(this._root!);

            while (!queue.IsEmpty)
            {
                BT.Node<T> node = queue.Dequeue();

                yield return node.Value;

                if (node.Left != null)
                {
                    queue.Enqueue(node.Left);
                }

                if (node.Right != null)
                {
                    queue.Enqueue(node.Right);
                }
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

        private BT.Node<T>? FindNode(T value)
        {
            BT.Node<T> current = this._root!;

            do
            {
                int comparison = COMPARER.Compare(value, current.Value);

                if (comparison == 0) // value == current.value
                {
                    break;
                }

                if (comparison < 0) // value < current.value
                {
                    current = current.Left!;
                }
                else // value > current.value
                {
                    current = current.Right!;
                }
            } while (current != null);

            return current;
        }

        private BT.Node<T> FindMinimumElement(BT.Node<T> node)
        {
            if (node.Left == null)
                return node;

            return this.FindMinimumElement(node.Left);
        }
    }
}
