using System.Collections.Generic;

namespace DataStructures.Queue
{
    public interface IQueue<T> : IEnumerable<T>
    {
        bool IsEmpty { get; }

        void Enqueue(T value);

        T Dequeue();

        T Peek(int position);
    }
}
