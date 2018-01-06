using System;

namespace Algorithms.Graphs.Directed
{
    public sealed class TransitiveClosure
    {
        private DirectedDepthFirstSearch[] _all;

        public static TransitiveClosure Create(Digraph digraph)
        {
            if (digraph == null)
                throw new ArgumentNullException(nameof(digraph));

            return new TransitiveClosure(digraph);
        }

        private TransitiveClosure(Digraph digraph)
        {
            this._all = new DirectedDepthFirstSearch[digraph.VerticesCount];

            for (int i = 0; i < digraph.VerticesCount; i++)
                this._all[i] = new DirectedDepthFirstSearch(digraph, i);
        }

        public Boolean IsReachable(Int32 sourceVertice, Int32 targetVertice)
        {
            return this._all[sourceVertice].HasDirectPathTo(targetVertice);
        }
    }
}
