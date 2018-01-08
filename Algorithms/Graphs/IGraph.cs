using System;
using System.Collections.Generic;

namespace Algorithms.Graphs
{
    public interface IGraph
    {
        Int32 EdgesCount { get; }

        Int32 VerticesCount { get; }

        IEnumerable<Int32> GetAdjacentVertices(Int32 verticeIndex);
    }
}