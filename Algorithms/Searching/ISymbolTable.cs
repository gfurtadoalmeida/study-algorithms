using System;
using System.Collections.Generic;

namespace Algorithms.Searching
{
    public interface ISymbolTable<TKey, TValue> where TKey : IComparable<TKey>
    {
        Int32 Size { get; }

        Boolean IsEmpty { get; }

        void Put(TKey key, TValue value);

        TValue Get(TKey key);

        void Delete(TKey key);

        Boolean Contains(TKey key);

        TKey Min();

        TKey Max();

        TKey Floor(TKey key);

        TKey Ceiling(TKey key);

        Int32 Rank(TKey key);

        TKey Select(Int32 rank);

        void DeleteMin();

        void DeleteMax();

        Int32 Count(TKey low, TKey high);

        IEnumerable<TKey> Keys();

        IEnumerable<TKey> Keys(TKey low, TKey high);
    }
}
