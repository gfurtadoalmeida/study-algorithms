using System;
using System.Collections.Generic;

namespace DataStructures.Trie
{
    public class Node
    {
        private readonly Dictionary<char, Node> _map;

        public bool IsEnd { get; set; }

        public int Size => this._map.Count;

        public Node()
        {
            this._map = new Dictionary<char, Node>();
        }

        public Node Add(char character)
        {
            if (this.Exists(character))
                throw new InvalidOperationException();

            Node childNode = new Node();

            this._map.Add(character, childNode);

            return childNode;
        }

        public Node Get(char character)
        {
            this.ThrowIfNotFound(character);

            return this._map[character];
        }

        public bool Exists(char character)
        {
            return this._map.ContainsKey(character);
        }

        public void Delete(char character)
        {
            this.ThrowIfNotFound(character);

            this._map.Remove(character);
        }

        private void ThrowIfNotFound(char character)
        {
            if (!this.Exists(character))
                throw new InvalidOperationException();
        }
    }
}
