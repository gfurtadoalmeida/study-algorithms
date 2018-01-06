using System;
using System.Collections.Generic;

namespace Algorithms.Graphs.Directed
{
    public sealed class TopologicalChecker
    {
        public IEnumerable<Int32> Order { get; }

        public Boolean IsDAG => this.Order != null;

        public static TopologicalChecker Create(Digraph digraph)
        {
            if (digraph == null)
                throw new ArgumentNullException(nameof(digraph));

            return new TopologicalChecker(digraph);
        }

        private TopologicalChecker(Digraph digraph)
        {
            CycleChecker checker = CycleChecker.Create(digraph);

            if (!checker.HasCycle)
            {
                DepthFirstOrder dfs = new DepthFirstOrder(digraph, DepthFirstOrderType.ReversePost);

                this.Order = dfs.ReversePost;
            }
        }
    }
}
