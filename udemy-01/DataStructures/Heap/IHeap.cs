using System;

namespace DataStructures.Heap
{
    public interface IHeap<T> where T : IComparable<T>
    {
        bool IsEmpty { get; }
     
        bool IsFull { get; }
        
        int Size { get; }

        void Add(T value);
        
        T ExtractTop();
        
        T PeekTop();
    }
}