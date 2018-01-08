using System;
using System.Collections.Generic;

namespace Algorithms.Graphs.EdgeWeighted
{
    public interface IMinimumSpanningTreeAlgorithm
    {
        Double Weight { get; }

        IEnumerable<Edge> GetEdges();
    }
}
