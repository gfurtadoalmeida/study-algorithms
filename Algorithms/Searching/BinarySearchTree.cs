using System;
using System.Collections.Generic;

namespace Algorithms.Searching
{
    /// <summary>
    /// Use: randomly ordered keys.
    /// </summary>
    public sealed class BinarySearchTree<TKey, TValue> : IOrderedSymbolTable<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Node _root;

        public Int32 Count => this.GetNodeSize(this._root);

        public Boolean IsEmpty => this.GetNodeSize(this._root) == 0;

        public void Add(TKey key, TValue value)
        {
            this._root = this.Put(this._root, key, value);
        }

        public TKey Ceiling(TKey key)
        {
            Node node = this.Ceiling(this._root, key);

            if (node != null)
                return node.Key;

            throw new KeyNotFoundException();
        }

        public Boolean Contains(TKey key)
        {
            return this.TryGet(this._root, key, out _);
        }

        public Int32 CountBetween(TKey low, TKey high)
        {
            return this.Rank(high) - this.Rank(low);
        }

        public void Delete(TKey key)
        {
            this._root = this.Delete(this._root, key);
        }

        public void DeleteMax()
        {
            this._root = this.DeleteMax(this._root);
        }

        public void DeleteMin()
        {
            this._root = this.DeleteMin(this._root);
        }

        public TKey Floor(TKey key)
        {
            Node node = this.Floor(this._root, key);

            if (node != null)
                return node.Key;

            throw new KeyNotFoundException();
        }

        public TValue Get(TKey key)
        {
            if (this.TryGet(this._root, key, out TValue value))
                return value;

            throw new KeyNotFoundException();
        }

        public IEnumerable<TKey> Keys()
        {
            Queue<TKey> queue = new Queue<TKey>(this.Count);

            this.Keys(this._root, this.Min(), this.Max(), queue);

            return queue;
        }

        public IEnumerable<TKey> Keys(TKey low, TKey high)
        {
            Queue<TKey> queue = new Queue<TKey>();

            this.Keys(this._root, low, high, queue);

            return queue;
        }

        public TKey Max()
        {
            return this.Max(this._root).Key;
        }

        public TKey Min()
        {
            return this.Min(this._root).Key;
        }

        public Int32 Rank(TKey key)
        {
            return this.Rank(this._root, key);
        }

        public TKey Select(Int32 rank)
        {
            Node node = this.Select(this._root, rank);

            if (node != null)
                return node.Key;

            throw new KeyNotFoundException();
        }

        private Int32 GetNodeSize(Node node)
        {
            return (node == null) ? 0 : node.Count;
        }

        private Boolean TryGet(Node node, TKey key, out TValue value)
        {            
            if (node != null)
            {
                Int32 cmp = key.CompareTo(node.Key);

                if (cmp < 0)
                    return this.TryGet(node.Left, key, out value);
                else if (cmp > 0)
                    return this.TryGet(node.Right, key, out value);
                else
                {
                    value = node.Value;

                    return true;
                }
            }

            value = default(TValue);

            return false;
        }

        private Node Put(Node node, TKey key, TValue value)
        {
            // Change key's value to value if key in the subtree rooted at node "node".
            // Otherwise, add new node to subtree associating key with val.
            if (node == null)
                return new Node(key, value, 1);

            Int32 cmp = key.CompareTo(node.Key);

            if (cmp < 0)
                node.Left = this.Put(node.Left, key, value);
            else if (cmp > 0)
                node.Right = this.Put(node.Right, key, value);
            else
                node.Value = value;

            node.Count = this.GetNodeSize(node.Left) + this.GetNodeSize(node.Right) + 1;

            return node;
        }

        private Node Min(Node node)
        {
            return node.Left == null ? node : this.Min(node.Left);
        }

        private Node Floor(Node node, TKey key)
        {
            if (node == null)
                return null;

            Int32 cmp = key.CompareTo(node.Key);

            if (cmp == 0)
                return node;

            if (cmp < 0)
                return this.Floor(node.Left, key);
            else
            {
                Node right = this.Floor(node.Right, key);

                if (right != null)
                    return right;
                else
                    return node;
            }
        }

        private Node Max(Node node)
        {
            return node.Right == null ? node : this.Max(node.Right);
        }

        private Node Ceiling(Node node, TKey key)
        {
            if (node == null)
                return null;

            Int32 cmp = key.CompareTo(node.Key);

            if (cmp == 0)
                return node;

            if (cmp > 0)
                return this.Ceiling(node.Right, key);
            else
            {
                Node left = this.Ceiling(node.Left, key);

                if (left != null)
                    return left;
                else
                    return node;
            }
        }

        private Node Select(Node node, Int32 rank)
        {
            // Return node containing key of rank "rank".
            if (node == null)
                return null;

            Int32 countOnLeft = this.GetNodeSize(node.Left);

            if (rank < countOnLeft)
                return this.Select(node.Left, rank);
            else if (rank > countOnLeft)
                return this.Select(node.Right, rank - countOnLeft - 1);
            else
                return node;
        }

        private Int32 Rank(Node node, TKey key)
        {
            // Return number of keys less than node.Key in the subtree rooted at "node".
            if (node == null)
                return 0;

            Int32 cmp = key.CompareTo(node.Key);

            if (cmp < 0)
                return this.Rank(node.Left, key);
            else if (cmp > 0)
                return 1 + this.GetNodeSize(node.Left) + this.Rank(node.Right, key);
            else
                return this.GetNodeSize(node.Left);
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
                return node.Right;

            node.Left = this.DeleteMin(node.Left);
            node.Count = this.GetNodeSize(node.Left) + this.GetNodeSize(node.Right) + 1;

            return node;
        }

        private Node DeleteMax(Node node)
        {
            if (node.Right == null)
                return node.Left;

            node.Right = this.DeleteMax(node.Right);
            node.Count = this.GetNodeSize(node.Right) + this.GetNodeSize(node.Left) + 1;

            return node;
        }

        private Node Delete(Node node, TKey key)
        {
            if (node == null)
                return null;

            Int32 cmp = key.CompareTo(node.Key);

            if (cmp < 0)
                node.Left = this.Delete(node.Left, key);
            else if (cmp > 0)
                node.Right = this.Delete(node.Right, key);
            else
            {
                if (node.Right == null)
                    return node.Left;

                if (node.Left == null)
                    return node.Right;

                Node temp = node;
                node = this.Min(temp.Right);
                node.Right = this.DeleteMin(temp.Right);
                node.Left = temp.Left;
            }

            node.Count = this.GetNodeSize(node.Left) + GetNodeSize(node.Right) + 1;

            return node;
        }

        private void Keys(Node node, TKey low, TKey high, Queue<TKey> queue)
        {
            if (node == null)
                return;

            Int32 cmpLow = low.CompareTo(node.Key);
            Int32 cmpHigh = high.CompareTo(node.Key);

            if (cmpLow < 0)
                this.Keys(node.Left, low, high, queue);

            if (cmpLow <= 0 && cmpHigh >= 0)
                queue.Enqueue(node.Key);

            if (cmpHigh > 0)
                this.Keys(node.Right, low, high, queue);
        }

        private sealed class Node
        {
            internal readonly TKey Key;

            internal TValue Value;
            internal Int32 Count;
            internal Node Left;
            internal Node Right;

            public Node(TKey key, TValue val, Int32 count)
            {
                this.Key = key;
                this.Value = val;
                this.Count = count;
            }
        }
    }
}
