using System;
using System.Collections.Generic;

namespace Algorithms.Graphs
{
    public interface IGraph
    {
        Int32 EdgesCount { get; }

        Int32 VerticesCount { get; }

        void AddEdge(Int32 verticeIndex, Int32 adjacentVerticeIndex);

        IEnumerable<Int32> GetAdjacentVertices(Int32 verticeIndex);
    }
}