using System;
using System.Collections.Generic;

namespace Algorithms.Searching
{
    /// <summary>
    /// Use: guaranteed performance for time-sensitive applications.
    /// </summary>
    public sealed class RedBlackTree<TKey, TValue> : IOrderedSymbolTable<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Node _root;

        public Int32 Count => this.GetNodeSize(this._root);

        public Boolean IsEmpty => this.GetNodeSize(this._root) == 0;

        public void Add(TKey key, TValue value)
        {
            this._root = this.Put(this._root, key, value);
            this._root.Color = Node.NodeColor.Black;
        }

        public TKey Ceiling(TKey key)
        {
            Node node = this.Ceiling(this._root, key);

            if (node != null)
            {
                return node.Key;
            }

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
            if (!this.IsNodeRed(this._root.Left) && !this.IsNodeRed(this._root.Right))
            {
                this._root.Color = Node.NodeColor.Red;
            }

            this._root = this.Delete(this._root, key);

            if (!this.IsEmpty)
            {
                this._root.Color = Node.NodeColor.Black;
            }
        }

        public void DeleteMax()
        {
            if (!this.IsNodeRed(this._root.Left) && !this.IsNodeRed(this._root.Right))
            {
                this._root.Color = Node.NodeColor.Red;
            }

            this._root = this.DeleteMax(this._root);

            if (!this.IsEmpty)
            {
                this._root.Color = Node.NodeColor.Black;
            }
        }

        public void DeleteMin()
        {
            if (!this.IsNodeRed(this._root.Left) && !this.IsNodeRed(this._root.Right))
            {
                this._root.Color = Node.NodeColor.Red;
            }

            this._root = this.DeleteMin(this._root);

            if (!this.IsEmpty)
            {
                this._root.Color = Node.NodeColor.Black;
            }
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

                if (cmp > 0)
                    return this.TryGet(node.Right, key, out value);

                value = node.Value;

                return true;
            }

            value = default;

            return false;
        }

        private Node Put(Node node, TKey key, TValue value)
        {
            if (node == null)
                return new Node(key, value, 1, Node.NodeColor.Red);

            Int32 cmp = key.CompareTo(node.Key);

            if (cmp < 0)
            {
                node.Left = this.Put(node.Left, key, value);
            }
            else if (cmp > 0)
            {
                node.Right = this.Put(node.Right, key, value);
            }
            else
            {
                node.Value = value;
            }

            if (this.IsNodeRed(node.Right) && !this.IsNodeRed(node.Left))
            {
                node = this.RotateNodeLeft(node);
            }

            if (this.IsNodeRed(node.Left) && this.IsNodeRed(node.Left.Left))
            {
                node = this.RotateNodeRight(node);
            }

            if (this.IsNodeRed(node.Left) && this.IsNodeRed(node.Right))
            {
                this.FlipNodeColors(node);
            }

            node.Count = 1
                         + this.GetNodeSize(node.Left)
                         + this.GetNodeSize(node.Right);

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

            Node Right = this.Floor(node.Right, key);

            if (Right != null)
                return Right;
            else
                return node;
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

            Node Left = this.Ceiling(node.Left, key);

            if (Left != null)
                return Left;
            else
                return node;
        }

        private Node Select(Node node, Int32 rank)
        {
            // Return node containing key of rank "rank".
            if (node == null)
                return null;

            Int32 countOnLeft = this.GetNodeSize(node.Left);

            if (rank < countOnLeft)
                return this.Select(node.Left, rank);

            if (rank > countOnLeft)
                return this.Select(node.Right, rank - countOnLeft - 1);

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

            if (cmp > 0)
                return 1 + this.GetNodeSize(node.Left) + this.Rank(node.Right, key);

            return this.GetNodeSize(node.Left);
        }

        private Node MoveNodeRedLeft(Node node)
        {
            // Assuming that "node" is red and both node.Left and node.Left.Left
            // are black, make node.Left or one of its children red.
            this.FlipNodeColors(node);

            if (this.IsNodeRed(node.Right.Left))
            {
                node.Right = this.RotateNodeRight(node.Right);
                node = this.RotateNodeLeft(node);

                this.FlipNodeColors(node);
            }

            return node;
        }

        private Node BalanceNode(Node node)
        {
            if (this.IsNodeRed(node.Right))
            {
                node = this.RotateNodeLeft(node);
            }

            if (this.IsNodeRed(node.Left) && this.IsNodeRed(node.Left.Left))
            {
                node = this.RotateNodeRight(node);
            }

            if (this.IsNodeRed(node.Left) && this.IsNodeRed(node.Right))
            {
                this.FlipNodeColors(node);
            }

            node.Count = 1
                         + this.GetNodeSize(node.Left)
                         + this.GetNodeSize(node.Right);
            return node;
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
                return null;

            if (!this.IsNodeRed(node.Left) && !this.IsNodeRed(node.Left.Left))
            {
                node = this.MoveNodeRedLeft(node);
            }

            node.Left = this.DeleteMin(node.Left);

            return this.BalanceNode(node);
        }

        private Node MoveNodeRedRight(Node node)
        {
            // Assuming that "node" is red and both node.Right and node.Right.Left
            // are black, make node.Right or one of its children red.
            this.FlipNodeColors(node);

            if (!this.IsNodeRed(node.Left.Left))
            {
                node = this.RotateNodeRight(node);

                this.FlipNodeColors(node);
            }

            return node;
        }

        private Node DeleteMax(Node node)
        {
            if (this.IsNodeRed(node.Left))
            {
                node = this.RotateNodeRight(node);
            }

            if (node.Right == null)
            {
                return null;
            }

            if (!this.IsNodeRed(node.Right) && !this.IsNodeRed(node.Right.Left))
            {
                node = this.MoveNodeRedRight(node);
            }

            node.Right = this.DeleteMax(node.Right);

            return this.BalanceNode(node);
        }

        private Node Delete(Node node, TKey key)
        {
            if (key.CompareTo(node.Key) < 0)
            {
                if (!this.IsNodeRed(node.Left) && !this.IsNodeRed(node.Left.Left))
                {
                    node = this.MoveNodeRedLeft(node);
                }

                node.Left = this.Delete(node.Left, key);
            }
            else
            {
                if (this.IsNodeRed(node.Left))
                {
                    node = this.RotateNodeRight(node);
                }

                if (key.CompareTo(node.Key) == 0 && (node.Right == null))
                    return null;

                if (!this.IsNodeRed(node.Right) && !this.IsNodeRed(node.Right.Left))
                {
                    node = this.MoveNodeRedRight(node);
                }

                if (key.CompareTo(node.Key) == 0)
                {
                    Node x = this.Min(node.Right);

                    node.Key = x.Key;
                    node.Value = x.Value;
                    node.Right = this.DeleteMin(node.Right);
                }
                else
                    node.Right = this.Delete(node.Right, key);
            }

            return this.BalanceNode(node);
        }

        private void Keys(Node node, TKey low, TKey high, Queue<TKey> queue)
        {
            if (node == null)
                return;

            Int32 cmpLow = low.CompareTo(node.Key);
            Int32 cmpHigh = high.CompareTo(node.Key);

            if (cmpLow < 0)
            {
                this.Keys(node.Left, low, high, queue);
            }

            if (cmpLow <= 0 && cmpHigh >= 0)
            {
                queue.Enqueue(node.Key);
            }

            if (cmpHigh > 0)
            {
                this.Keys(node.Right, low, high, queue);
            }
        }

        private Boolean IsNodeRed(Node node)
        {
            return node == null ? false : node.Color == Node.NodeColor.Red;
        }

        private Node RotateNodeRight(Node node)
        {
            Node x = node.Left;
            node.Left = x.Right;
            x.Right = node;
            x.Color = node.Color;
            node.Color = Node.NodeColor.Red;
            x.Count = node.Count;
            node.Count = 1
                         + this.GetNodeSize(node.Left)
                         + this.GetNodeSize(node.Right);
            return x;
        }

        private Node RotateNodeLeft(Node node)
        {
            Node x = node.Right;
            node.Right = x.Left;
            x.Left = node;
            x.Color = node.Color;
            node.Color = Node.NodeColor.Red;
            x.Count = node.Count;
            node.Count = 1
                         + this.GetNodeSize(node.Left)
                         + this.GetNodeSize(node.Right);

            return x;
        }

        private void FlipNodeColors(Node node)
        {
            node.Color = flip(node);
            node.Left.Color = flip(node.Left);
            node.Right.Color = flip(node.Right);

            static Node.NodeColor flip(Node nd)
            {
                return nd.Color == Node.NodeColor.Red ? Node.NodeColor.Black : Node.NodeColor.Red;
            }
        }

        private sealed class Node
        {
            internal enum NodeColor : byte { Black = 0, Red = 1 }

            internal TKey Key;
            internal TValue Value;
            internal Int32 Count;
            internal Node Left;
            internal Node Right;
            internal NodeColor Color;

            public Node(TKey key, TValue val, Int32 count, NodeColor color)
            {
                this.Key = key;
                this.Value = val;
                this.Count = count;
                this.Color = color;
            }
        }
    }
}
