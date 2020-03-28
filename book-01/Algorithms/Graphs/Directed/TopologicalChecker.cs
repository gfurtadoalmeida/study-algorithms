using System;
using System.Collections.Generic;
using Algorithms.Graphs.Directed.EdgeWeighted;

namespace Algorithms.Graphs.Directed
{
    /// <summary>
    ///  Linear ordering of its vertices such that for every directed 
    ///  edge uv from vertex u to vertex v, u comes before v in the ordering.
    /// </summary>
    public sealed class TopologicalChecker
    {
        private readonly Int32[] _rank; // rank[v] = rank of vertex v in order.

        public IEnumerable<Int32> Order { get; }

        public Boolean IsDAG => this.Order != null;

        public static TopologicalChecker Create(Digraph digraph)
        {
            if (digraph == null)
                throw new ArgumentNullException(nameof(digraph));

            return new TopologicalChecker(digraph);
        }

        public static TopologicalChecker Create(EdgeWeightedDigraph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            return new TopologicalChecker(graph);
        }

        private TopologicalChecker(Digraph digraph)
        {
            CycleChecker checker = CycleChecker.Create(digraph);

            if (!checker.HasCycle)
            {
                DepthFirstOrder dfs = new DepthFirstOrder(digraph);

                this.Order = dfs.ReversePost();

                this._rank = new Int32[digraph.VerticesCount];

                Int32 i = 0;

                foreach (Int32 v in this.Order)
                {
                    this._rank[v] = i++;
                }
            }
        }

        public TopologicalChecker(EdgeWeightedDigraph graph)
        {
            EdgeWeightedCycleChecker checker = EdgeWeightedCycleChecker.Create(graph);

            if (!checker.HasCycle)
            {
                DepthFirstOrder dfs = new DepthFirstOrder(graph);

                this.Order = dfs.ReversePost();
            }
        }

        public Int32 Rank(Int32 vertice)
        {
            if (this.Order != null)
                return this._rank[vertice];

            return -1;
        }
    }
}
