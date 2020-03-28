using System;
using System.Collections.Generic;
using QE = DataStructures.Queue;
using ST = DataStructures.Stack;
namespace DataStructures.BinaryTree
{
    public class LinkedListBasedBinaryTree<T> : IBinaryTree<T>
    {
        private static readonly IEqualityComparer<T> COMPARER = EqualityComparer<T>.Default;

        private Node<T>? _root;

        public bool IsEmpty => this._root == null;

        public void Add(T value)
        {
            if (this.IsEmpty)
            {
                this._root = new Node<T>(value);
            }
            else
            {
                Node<T>? parent = this.FindNode(n => n.Left == null || n.Right == null);

                if (parent == null)
                    throw new InvalidOperationException();

                if (parent.Left == null)
                {
                    parent.Left = new Node<T>(value);
                }
                else
                {
                    parent.Right = new Node<T>(value);
                }
            }
        }

        public Node<T>? Get(T value)
        {
            this.ThrowIfEmpty();

            return this.FindNode(n => COMPARER.Equals(n.Value, value));
        }

        public bool Contains(T value)
        {
            this.ThrowIfEmpty();

            return this.Get(value) != null;
        }

        public void Delete(T value)
        {
            this.ThrowIfEmpty();

            Node<T>? node = this.Get(value);

            if (node == null)
                throw new InvalidOperationException();

            Node<T> leaf = this.FindDeepestLeaf();

            if (leaf == this._root)
            {
                this._root = null;
            }
            else
            {
                node!.Value = leaf.Value;

                leaf.Detach();
            }
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

                // In this case we need to use queue.
                EnumerationMode.LevelOrder => this.GetEnumeratorLevelOrderQueue(),

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
                EnumerationMode.LevelOrder => this.GetEnumeratorLevelOrderQueue(),

                _ => throw new ArgumentException(nameof(enumerationMode)),
            };
        }

        private IEnumerator<T> GetEnumeratorPreOrderStack()
        {
            if (!this._root!.HasAllChildren)
            {
                yield return this._root.Value;
            }
            else
            {
                ST.SingleLinkedListBasedStack<Node<T>> stack = new ST.SingleLinkedListBasedStack<Node<T>>();

                stack.Push(this._root!);

                while (!stack.IsEmpty)
                {
                    Node<T> node = stack.Pop();

                    yield return node.Value;

                    if (node.Right != null)
                    {
                        stack.Push(node.Right);
                    }

                    if (node.Left != null)
                    {
                        stack.Push(node.Left);
                    }
                }
            }
        }

        private IEnumerator<T> GetEnumeratorInOrderStack()
        {
            if (!this._root!.HasAllChildren)
            {
                yield return this._root.Value;
            }
            else
            {
                ST.SingleLinkedListBasedStack<Node<T>> stack = new ST.SingleLinkedListBasedStack<Node<T>>();

                Node<T>? current = this._root;

                while (current != null || !stack.IsEmpty)
                {
                    while (current != null)
                    {
                        stack.Push(current);

                        current = current.Left;
                    }

                    current = stack.Pop();

                    yield return current.Value;

                    current = current.Right;
                }
            }
        }

        private IEnumerator<T> GetEnumeratorPostOrderStack()
        {
            if (!this._root!.HasAllChildren)
            {
                yield return this._root.Value;
            }
            else
            {
                ST.SingleLinkedListBasedStack<Node<T>> stack = new ST.SingleLinkedListBasedStack<Node<T>>();

                Node<T>? current = this._root;

                while (current != null || !stack.IsEmpty)
                {
                    while (current != null)
                    {
                        if (current.Right != null)
                        {
                            stack.Push(current.Right);
                        }

                        stack.Push(current);

                        current = current.Left;
                    }

                    current = stack.Pop();

                    if (!stack.IsEmpty && current.Right == stack.Peek(0))
                    {
                        stack.Pop();

                        stack.Push(current);

                        current = current.Right;
                    }
                    else
                    {
                        yield return current.Value;

                        current = null;
                    }
                }
            }
        }

        private IEnumerator<T> GetEnumeratorLevelOrderQueue()
        {
            QE.SingleLinkedListBasedQueue<Node<T>> queue = new QE.SingleLinkedListBasedQueue<Node<T>>();

            queue.Enqueue(this._root!);

            while (!queue.IsEmpty)
            {
                Node<T> node = queue.Dequeue();

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

        private IEnumerator<T> GetEnumeratorPreOrderRecursive()
        {
            QE.SingleLinkedListBasedQueue<T> queue = new QE.SingleLinkedListBasedQueue<T>();

            Traverse(this._root!);

            return queue.GetEnumerator();

            void Traverse(Node<T> node)
            {
                queue.Enqueue(node.Value);

                if (node.Left != null)
                {
                    Traverse(node.Left);
                }

                if (node.Right != null)
                {
                    Traverse(node.Right);
                }
            }
        }

        private IEnumerator<T> GetEnumeratorInOrderRecursive()
        {
            QE.SingleLinkedListBasedQueue<T> queue = new QE.SingleLinkedListBasedQueue<T>();

            Traverse(this._root!);

            return queue.GetEnumerator();

            void Traverse(Node<T> node)
            {
                if (node.Left != null)
                {
                    Traverse(node.Left);
                }

                queue.Enqueue(node.Value);

                if (node.Right != null)
                {
                    Traverse(node.Right);
                }
            }
        }

        private IEnumerator<T> GetEnumeratorPostOrderRecursive()
        {
            QE.SingleLinkedListBasedQueue<T> queue = new QE.SingleLinkedListBasedQueue<T>();

            Traverse(this._root!);

            return queue.GetEnumerator();

            void Traverse(Node<T> node)
            {
                if (node.Left != null)
                {
                    Traverse(node.Left);
                }

                if (node.Right != null)
                {
                    Traverse(node.Right);
                }

                queue.Enqueue(node.Value);
            }
        }

        private void ThrowIfEmpty()
        {
            if (this.IsEmpty)
                throw new InvalidOperationException();
        }

        private Node<T>? FindNode(Func<Node<T>, bool> condition)
        {
            QE.SingleLinkedListBasedQueue<Node<T>> queue = new QE.SingleLinkedListBasedQueue<Node<T>>();

            queue.Enqueue(this._root!);

            Node<T>? value = null;

            do
            {
                Node<T> node = queue.Dequeue();

                if (condition(node))
                {
                    value = node;

                    break;
                }
                else
                {
                    if (node.Left != null)
                    {
                        queue.Enqueue(node.Left);
                    }

                    if (node.Right != null)
                    {
                        queue.Enqueue(node.Right);
                    }
                }

            } while (!queue.IsEmpty);

            return value;
        }

        private Node<T> FindDeepestLeaf()
        {
            QE.SingleLinkedListBasedQueue<Node<T>> queue = new QE.SingleLinkedListBasedQueue<Node<T>>();

            queue.Enqueue(this._root!);

            Node<T>? node = null;

            while (!queue.IsEmpty)
            {
                node = queue.Dequeue();

                if (node.HasChildren)
                {
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

            return node!;
        }
    }
}
