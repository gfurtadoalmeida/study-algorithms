using System;

namespace DataStructures.Union
{
    public interface IUnionFinder
    {
        Int32 Count { get; }

        Int32 Find(Int32 id);

        Boolean IsConnected(Int32 idOne, Int32 idTwo);

        void Union(Int32 idOne, Int32 idTwo);
    }
}