using System;
using System.Collections.Generic;

namespace Algorithms.Graphs.Directed.EdgeWeighted
{
    public interface IShortestPathAlgorithm
    {
        Double DistanceTo(Int32 vertice);

        Boolean HasPathTo(Int32 vertice);

        IEnumerable<Edge> PathTo(Int32 vertice);
    }
}