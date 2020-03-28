using System.Collections.Generic;

namespace DataStructures.Stack
{
    public interface IStack<T> : IEnumerable<T>
    {
        bool IsEmpty { get; }

        void Push(T value);

        T Pop();

        T Peek(int position);
    }
}
