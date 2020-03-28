using System;

namespace DataStructures.Trie
{
    public class Trie
    {
        private readonly Node _root;

        public Trie()
        {
            this._root = new Node();
        }

        public void Add(string value)
        {
            this.ThrowIfNullOrEmpty(value);

            Node current = this._root;

            foreach (char c in value)
            {
                if (current.Exists(c))
                {
                    current = current.Get(c);
                }
                else
                {
                    current = current.Add(c);
                }
            }

            current.IsEnd = true;
        }

        public void Delete(string value)
        {
            this.ThrowIfNullOrEmpty(value);

            if (this.Contains(value))
            {
                Delete(this._root, value, 0);
            }

            static bool Delete(Node parent, string value, int index)
            {
                bool canDelete = false;
                char c = value[index];

                Node current = parent.Get(c);

                if (current.Size > 1)
                {
                    // Case 1:
                    // If there are more characters in the node
                    // it means that more strings depends on it.
                    // We cannot delete it.

                    Delete(current, value, index + 1);

                    return false;
                }
                else if (index == value.Length - 1)
                {
                    // Case 2:
                    // We reached the end of the string.
                    // If there are more chars, just set IsEnd
                    // to false.
                    // Otherwise it's safe to delete.

                    if (current.Size > 0)
                    {
                        current.IsEnd = false;
                    }
                    else
                    {
                        parent.Delete(c);

                        canDelete = true;
                    }
                }
                else if (current.IsEnd)
                {
                    // Case 3:
                    // We have not reached the end but this node
                    // is the end of another string.
                    // We can't delete it.
                    Delete(current, value, index + 1);
                }
                else if (Delete(current, value, index + 1))
                {
                    parent.Delete(c);

                    canDelete = true;
                }

                return canDelete;
            }
        }

        public bool Contains(string value)
        {
            this.ThrowIfNullOrEmpty(value);

            bool exists = true;
            Node current = this._root;

            foreach (char c in value)
            {
                if (current.Exists(c))
                {
                    current = current.Get(c);
                }
                else
                {
                    exists = false;

                    break;
                }
            }

            return exists && current.IsEnd;
        }

        private void ThrowIfNullOrEmpty(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));
        }
    }
}
