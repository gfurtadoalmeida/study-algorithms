using System;
using System.Collections.Generic;
using System.Text;
using AST = Algorithms.Structures;

namespace Algorithms.Strings
{
    /// <summary>
    /// Use: Short keys with small alphabets.
    /// </summary>
    public sealed class Trie
    {
        private Node _root;

        /// <summary>
        /// Number of keys.
        /// </summary>
        public Int32 Count { get; private set; }

        public Alphabet Alphabet { get; }

        public Boolean IsEmpty => this.Count == 0;

        public Trie(Alphabet alphabet)
        {
            if (alphabet == null)
                throw new ArgumentNullException(nameof(alphabet));

            this.Alphabet = alphabet;
        }

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

            Node node = this.Get(this._root, key, 0);

            if (node == null)
                throw new KeyNotFoundException();

            return node.Value;
        }

        private Node Get(Node node, String key, Int32 index)
        {
            if (node == null)
                return null;

            if (index == key.Length)
                return node;

            Char c = key[index];

            return this.Get(node.Next[c], key, index + 1);
        }

        public void Add(String key, Object value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (value == null)
                this.Delete(key);
            else
                this._root = this.Add(this._root, key, value, 0);
        }

        private Node Add(Node node, String key, Object value, Int32 index)
        {
            if (node == null)
                node = new Node(this.Alphabet, null);

            if (index == key.Length)
            {
                if (node.Value == null)
                    this.Count++;

                node.Value = value;

                return node;
            }

            Char c = key[index];

            node.Next[c] = this.Add(node.Next[c], key, value, index + 1);

            return node;
        }

        public IEnumerable<String> GetKeys()
        {
            return this.GetKeysWithPrefix(String.Empty);
        }

        public IEnumerable<String> GetKeysWithPrefix(String prefix)
        {
            AST.Queue<String> results = new AST.Queue<String>();

            Node node = this.Get(this._root, prefix, 0);

            this.GetKeysWithPrefix(node, new StringBuilder(prefix), results);

            return results;
        }

        private void GetKeysWithPrefix(Node node, StringBuilder prefix, AST.Queue<String> results)
        {
            if (node == null)
                return;

            if (node.Value != null)
                results.Enqueue(prefix.ToString());

            for (char c = this.Alphabet.MinChar; c <= this.Alphabet.MaxChar; c++)
            {
                prefix.Append(c);

                this.GetKeysWithPrefix(node.Next[c], prefix, results);

                prefix.Remove(prefix.Length - 1, 1);
            }
        }

        public IEnumerable<String> GetKeysThatMatch(String pattern)
        {
            AST.Queue<String> results = new AST.Queue<String>();

            this.GetKeysThatMatch(this._root, new StringBuilder(), pattern, results);

            return results;
        }

        private void GetKeysThatMatch(Node node, StringBuilder prefix, String pattern, AST.Queue<String> results)
        {
            if (node == null)
                return;

            Int32 index = prefix.Length;

            if (index == pattern.Length && node.Value != null)
                results.Enqueue(prefix.ToString());

            if (index == pattern.Length)
                return;

            Char c = pattern[index];

            if (c == '.')
            {
                for (char ch = this.Alphabet.MinChar; ch <= this.Alphabet.MaxChar; ch++)
                {
                    prefix.Append(ch);

                    this.GetKeysThatMatch(node.Next[ch], prefix, pattern, results);

                    prefix.Remove(prefix.Length - 1, 1);
                }
            }
            else
            {
                prefix.Append(c);

                this.GetKeysThatMatch(node.Next[c], prefix, pattern, results);

                prefix.Remove(prefix.Length - 1, 1);
            }
        }

        public String GetLongestPrefixOf(String query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            Int32 length = this.GetLongestPrefixOf(this._root, query, 0, -1);

            if (length == -1)
                return null;
            else
                return query.Substring(0, length);
        }

        private Int32 GetLongestPrefixOf(Node node, String query, Int32 index, Int32 length)
        {
            if (node == null)
                return length;

            if (node.Value != null)
                length = index;

            if (index == query.Length)
                return length;

            Char c = query[index];

            return this.GetLongestPrefixOf(node.Next[c], query, index + 1, length);
        }

        public void Delete(String key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            this._root = this.Delete(this._root, key, 0);
        }

        private Node Delete(Node node, String key, Int32 index)
        {
            if (node == null)
                return null;

            if (index == key.Length)
            {
                if (node.Value != null)
                    this.Count--;

                node.Value = null;
            }
            else
            {
                Char c = key[index];

                node.Next[c] = this.Delete(node.Next[c], key, index + 1);
            }

            // Remove subtrie rooted at x if it is completely empty.
            if (node.Value != null)
                return node;

            for (int c = this.Alphabet.MinChar; c <= this.Alphabet.MaxChar; c++)
            {
                if (node.Next[c] != null)
                    return node;
            }

            return null;
        }

        private sealed class Node
        {
            public readonly AST.CompactBoundedArray<Node> Next;

            public Object Value;

            public Node(Alphabet alphabet, Object value)
            {
                this.Next = new AST.CompactBoundedArray<Node>(alphabet.MinChar, alphabet.MaxChar);
                this.Value = value;
            }
        }
    }
}
