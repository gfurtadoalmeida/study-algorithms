using System.Collections.Generic;

namespace DataStructures.LinkedList
{
    public interface ILinkedList<T> : IEnumerable<T>
    {
        bool IsEmpty { get; }

        int Count { get; }

        public void InsertAt(int position, T value);

        T GetAt(int position);

        bool Contains(T value);

        void DeleteAt(int position);
    }
}
