using System;
using System.Collections;
using System.Collections.Generic;
using BT = DataStructures.BinaryTree;
using QE = DataStructures.Queue;

namespace DataStructures.BinarySearchTree
{
    public class AvlBinarySearchTree<T> : IBinarySearchTree<T>
    {
        private static readonly IComparer<T> COMPARER = Comparer<T>.Default;

        public const int BALANCE_FACTOR = 1;

        private BT.Node<T>? _root;

        public bool IsEmpty => this._root == null;

        public void Add(T value)
        {
            this._root = Insert(this._root!, value);

            BT.Node<T> Insert(BT.Node<T>? current, T value)
            {
                if (current == null)
                    return new BT.Node<T>(value);

                if (COMPARER.Compare(value, current.Value) < 1)
                {
                    current.Left = Insert(current.Left, value);
                }
                else
                {
                    current.Right = Insert(current.Right, value);
                }

                int balance = this.CheckBalance(current.Left, current.Right);

                if (balance > BALANCE_FACTOR) //Left overloaded
                {
                    if (this.CheckBalance(current.Left!.Left, current.Left.Right) > 0)
                    {
                        current = this.RightRotate(current);
                    }
                    else
                    {
                        current.Left = this.LeftRotate(current.Left);
                        current = this.RightRotate(current);
                    }
                }
                else if (balance < -BALANCE_FACTOR) //Right overloaded
                {
                    if (this.CheckBalance(current.Right!.Right, current.Right.Left) > 0)
                    {
                        current = this.LeftRotate(current);
                    }
                    else
                    {
                        current.Right = this.RightRotate(current.Right);
                        current = this.LeftRotate(current);
                    }
                }

                return current;
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

            if (!this.Contains(value))
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

                    return current;
                }

                int balance = this.CheckBalance(current.Left, current.Right);

                if (balance > BALANCE_FACTOR) //Left overloaded
                {
                    if (this.CheckBalance(current.Left!.Left, current.Left.Right) > 0)
                    {
                        current = this.RightRotate(current);
                    }
                    else
                    {
                        current.Left = this.LeftRotate(current.Left);
                        current = this.RightRotate(current);
                    }
                }
                else if (balance < -BALANCE_FACTOR) //Right overloaded
                {
                    if (this.CheckBalance(current.Right!.Right, current.Right.Left) > 0)
                    {
                        current = this.LeftRotate(current);
                    }
                    else
                    {
                        current.Right = this.RightRotate(current.Right);
                        current = this.LeftRotate(current);
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

        private int CheckBalance(BT.Node<T>? left, BT.Node<T>? right)
        {
            // If current node is a leaf node then no need 
            // to check balance of its children.
            if (left == null && right == null)
                return 0;

            // If left node is not there then simply return right node's
            // height + 1.
            // We need to make it -1 because blank height is considered
            // having height as -1.
            if (left == null)
                return -1 * (this.CalculateHeight(right) + 1);

            if (right == null)
                return this.CalculateHeight(left) + 1;

            // +1 is not required, as both right and left child
            // exits and 1 gets nullified.
            return this.CalculateHeight(left) - this.CalculateHeight(right);
        }

        private BT.Node<T> LeftRotate(BT.Node<T> disbalancedNode)
        {
            BT.Node<T> newParent = disbalancedNode.Right!;

            disbalancedNode.Right = disbalancedNode.Right!.Left;

            newParent.Left = disbalancedNode;

            return newParent;
        }

        private BT.Node<T> RightRotate(BT.Node<T> disbalancedNode)
        {
            BT.Node<T> newParent = disbalancedNode.Left!;

            disbalancedNode.Left = disbalancedNode.Left!.Right;

            newParent.Right = disbalancedNode;

            return newParent;
        }

        private int CalculateHeight(BT.Node<T>? node)
        {
            if (node == null)
                return 0;

            int leftHeight = node.Left == null ? -1 : this.CalculateHeight(node.Left);
            int righHeight = node.Right == null ? -1 : this.CalculateHeight(node.Right);

            return 1 + Math.Max(leftHeight, righHeight);
        }

        private BT.Node<T> FindMinimumElement(BT.Node<T> node)
        {
            if (node.Left == null)
                return node;

            return this.FindMinimumElement(node.Left);
        }
    }
}
