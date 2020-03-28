using System;
using System.Collections.Generic;

namespace Algorithms.Searching
{
    public interface ISymbolTable<TKey, TValue> where TKey : IComparable<TKey>
    {
        Int32 Count { get; }

        Boolean IsEmpty { get; }

        void Add(TKey key, TValue value);

        TValue Get(TKey key);

        Boolean Contains(TKey key);

        void Delete(TKey key);

        IEnumerable<TKey> Keys();
    }
}
