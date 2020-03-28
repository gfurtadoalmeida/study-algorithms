using System.Diagnostics;

namespace DataStructures.LinkedList
{
    [DebuggerDisplay("{Value}")]
    public class DoubleNode<T>
    {
        public static readonly DoubleNode<T> Empty = new DoubleNode<T>(default!, null!, null!);

        public DoubleNode<T>? Next { get; set; }

        public DoubleNode<T>? Previous { get; set; }

        public T Value { get; set; }

        public DoubleNode(T value, DoubleNode<T>? previous, DoubleNode<T>? next)
        {
            this.Next = next!;
            this.Previous = previous!;
            this.Value = value;
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