using System;
using System.Collections.Generic;

namespace Algorithms.Searching
{
    public interface IOrderedSymbolTable<TKey, TValue> where TKey : IComparable<TKey>
    {
        Boolean IsEmpty { get; }

        Int32 Count { get; }

        void Add(TKey key, TValue value);

        TKey Ceiling(TKey key);

        Boolean Contains(TKey key);

        Int32 CountBetween(TKey low, TKey high);

        void Delete(TKey key);

        void DeleteMin();

        void DeleteMax();

        TKey Floor(TKey key);

        TValue Get(TKey key);

        IEnumerable<TKey> Keys();

        IEnumerable<TKey> Keys(TKey low, TKey high);

        TKey Max();

        TKey Min();

        Int32 Rank(TKey key);

        TKey Select(Int32 rank);
    }
}
