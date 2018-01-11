using System;
using System.Collections.Generic;
using System.Text;
using AST = Algorithms.Structures;

namespace Algorithms.Strings
{
    public sealed class TernarySearchTrie
    {
        private Node _root;

        public Int32 Count { get; private set; }

        public Boolean IsEmpty => this.Count == 0;

        public Boolean Contains(String key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            return this.Get(this._root, key, 0) != null;
        }

        public Object Get(String key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (key.Length == 0)
                throw new Exception("Key must have length >= 1.");

            Node node = this.Get(this._root, key, 0);

            if (node == null)
                throw new KeyNotFoundException();

            return node.Value;
        }

        private Node Get(Node x, String key, Int32 index)
        {
            if (x == null)
                return null;

            if (key.Length == 0)
                throw new Exception("Key must have length >= 1.");

            Char c = key[index];

            if (c < x.Char)
                return this.Get(x.Left, key, index);
            else if (c > x.Char)
                return this.Get(x.Right, key, index);
            else if (index < key.Length - 1)
                return this.Get(x.Middle, key, index + 1);
            else
                return x;
        }

        public void Add(String key, Object value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (!this.Contains(key))
                this.Count++;

            this._root = this.Add(this._root, key, value, 0);
        }

        private Node Add(Node node, String key, Object value, Int32 index)
        {
            Char c = key[index];

            if (node == null)
            {
                node = new Node();
                node.Char = c;
            }

            if (c < node.Char)
                node.Left = this.Add(node.Left, key, value, index);
            else if (c > node.Char)
                node.Right = this.Add(node.Right, key, value, index);
            else if (index < key.Length - 1)
                node.Middle = this.Add(node.Middle, key, value, index + 1);
            else
                node.Value = value;

            return node;
        }

        public String GetLongestPrefixOf(String query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            if (query.Length == 0)
                return null;

            Int32 length = 0;
            Node node = this._root;
            Int32 i = 0;

            while (node != null && i < query.Length)
            {
                Char c = query[i];

                if (c < node.Char)
                    node = node.Left;
                else if (c > node.Char)
                    node = node.Right;
                else
                {
                    i++;

                    if (node.Value != null)
                        length = i;

                    node = node.Middle;
                }
            }

            return query.Substring(0, length);
        }

        public IEnumerable<String> GetKeys()
        {
            AST.Queue<String> queue = new AST.Queue<String>();

            this.GetKeysWithPrefix(this._root, new StringBuilder(), queue);

            return queue;
        }

        public IEnumerable<String> GetKeysWithPrefix(String prefix)
        {
            if (prefix == null)
                throw new ArgumentNullException(nameof(prefix));

            AST.Queue<String> queue = new AST.Queue<String>();

            Node node = this.Get(this._root, prefix, 0);

            if (node == null)
                return queue;

            if (node.Value != null)
                queue.Enqueue(prefix);

            this.GetKeysWithPrefix(node.Middle, new StringBuilder(prefix), queue);

            return queue;
        }

        private void GetKeysWithPrefix(Node node, StringBuilder prefix, AST.Queue<String> queue)
        {
            if (node == null)
                return;

            this.GetKeysWithPrefix(node.Left, prefix, queue);

            if (node.Value != null)
                queue.Enqueue(prefix.ToString() + node.Char);

            this.GetKeysWithPrefix(node.Middle, prefix.Append(node.Char), queue);

            prefix.Remove(prefix.Length - 1, 1);

            this.GetKeysWithPrefix(node.Right, prefix, queue);
        }

        public IEnumerable<String> GetKeysThatMatch(String pattern)
        {
            AST.Queue<String> queue = new AST.Queue<String>();

            this.GetKeysThatMatch(this._root, new StringBuilder(), 0, pattern, queue);

            return queue;
        }

        private void GetKeysThatMatch(Node node, StringBuilder prefix, Int32 index, String pattern, AST.Queue<String> queue)
        {
            if (node == null)
                return;

            Char c = pattern[index];

            if (c == '.' || c < node.Char)
                this.GetKeysThatMatch(node.Left, prefix, index, pattern, queue);

            if (c == '.' || c == node.Char)
            {
                if (index == pattern.Length - 1 && node.Value != null)
                    queue.Enqueue(prefix.ToString() + node.Char);

                if (index < pattern.Length - 1)
                {
                    this.GetKeysThatMatch(node.Middle, prefix.Append(node.Char), index + 1, pattern, queue);

                    prefix.Remove(prefix.Length - 1, 1);
                }
            }

            if (c == '.' || c > node.Char)
                this.GetKeysThatMatch(node.Right, prefix, index, pattern, queue);
        }

        private sealed class Node
        {
            public Char Char;
            public Node Left;
            public Node Middle;
            public Node Right;
            public Object Value;
        }
    }
}