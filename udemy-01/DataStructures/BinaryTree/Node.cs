using System;
using System.Diagnostics;

namespace DataStructures.BinaryTree
{
    [DebuggerDisplay("{Value}")]
    public class Node<T>
    {
        private Node<T>? _left;
        private Node<T>? _right;

        public Node<T>? Parent { get; private set; }

        public Node<T>? Left
        {
            get => this._left;
            set => this.SetNode(ref this._left, value);
        }

        public Node<T>? Right
        {
            get => this._right;
            set => this.SetNode(ref this._right, value);
        }

        public T Value { get; set; }

        public bool HasChildren => this.Left != null || this.Right != null;

        public bool HasAllChildren => this.Left != null && this.Right != null;

        public bool IsLeft => this.Parent != null && this.Parent.Left == this;

        public bool IsRight => this.Parent != null && this.Parent.Right == this;

        public Node(T value)
        {
            this.Value = value;
        }

        public void Detach()
        {
            if (this.Parent != null)
            {
                if (this.IsLeft)
                {
                    this.Parent._left = null;
                }
                else
                {
                    this.Parent._right = null;
                }

                this.Parent = null;
            }
        }

        public override string ToString()
        {
            if (this.Value != null)
            {
                return this.Value.ToString();
            }

            return string.Empty;
        }

        private void SetNode(ref Node<T>? reference, Node<T>? value)
        {
            if (value == null)
            {
                if (reference != null)
                {
                    reference.Detach();
                }

                reference = null;
            }
            else
            {
                value.Detach();
                reference?.Detach();

                reference = value;
                reference.Parent = this;
            }
        }
    }
}
