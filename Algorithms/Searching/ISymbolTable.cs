using System;
using System.Collections.Generic;

namespace Algorithms.Searching
{
    public interface ISymbolTable<TKey, TValue> where TKey : IComparable<TKey>
    {
        Int32 Size { get; }

        Boolean IsEmpty { get; }

        TValue Get(TKey key);

        void Put(TKey key, TValue value);

        void Delete(TKey key);

        Boolean Contains(TKey key);

        IEnumerable<TKey> Keys();
    }
}
