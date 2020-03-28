using System.Diagnostics;

namespace DataStructures.LinkedList
{
    [DebuggerDisplay("{Value}")]
    public class Node<T>
    {
        public static readonly Node<T> Empty = new Node<T>(default!, null);

        public Node<T>? Next { get; set; }

        public T Value { get; set; }

        public Node(T value, Node<T>? next)
        {
            this.Value = value;
            this.Next = next!;
        }

        public override string ToString()
        {
            if (this.Value != null)
            {
                return this.Value.ToString();
            }

            return string.Empty;
        }
    }
}