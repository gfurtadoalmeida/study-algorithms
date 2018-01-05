using System;
using System.Collections.Generic;

namespace Algorithms.Graphs.Directed
{
    public sealed class TopologicalChecker
    {
        public IEnumerable<Int32> Order { get; }

        public Boolean IsDAG => this.Order != null;

        public TopologicalChecker(Digraph digraph)
        {
            CycleChecker checker = new CycleChecker(digraph);

            if (!checker.HasCycle)
            {
                DepthFirstSearchOrder dfs = new DepthFirstSearchOrder(digraph, DepthFirstSearchOrderType.ReversePost);

                this.Order = dfs.ReversePost;
            }
        }
    }
}
