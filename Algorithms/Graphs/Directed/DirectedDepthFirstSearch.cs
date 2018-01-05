using System;
using System.Collections.Generic;

namespace Algorithms.Graphs.Directed
{
    public sealed class DirectedDepthFirstSearch
    {
        private readonly Boolean[] _reachableFromSourceMap;

        public DirectedDepthFirstSearch(Digraph digraph, Int32 sourceVertice)
        {
            if (digraph == null)
                throw new ArgumentNullException(nameof(digraph));

            if (sourceVertice < 0 || sourceVertice > digraph.VerticesCount - 1)
                throw new ArgumentOutOfRangeException(nameof(sourceVertice),
                                                      "Value must be greater or equal to zero and less than " + digraph.VerticesCount);

            this._reachableFromSourceMap = new Boolean[digraph.VerticesCount];

            this.DFS(digraph, sourceVertice);
        }

        public DirectedDepthFirstSearch(Digraph digraph, IEnumerable<Int32> sources)
        {
            if (digraph == null)
                throw new ArgumentNullException(nameof(digraph));

            if (sources == null)
                throw new ArgumentNullException(nameof(sources));

            this._reachableFromSourceMap = new Boolean[digraph.VerticesCount];

            foreach (Int32 vertice in sources)
                if (!this._reachableFromSourceMap[vertice])
                    this.DFS(digraph, vertice);
        }

        public Boolean HasDirectPathTo(Int32 vertice)
        {
            if (vertice < 0 || vertice > this._reachableFromSourceMap.Length - 1)
                throw new ArgumentOutOfRangeException(nameof(vertice),
                                                      "Value must be greater or equal to zero and less than " + this._reachableFromSourceMap.Length);

            return this._reachableFromSourceMap[vertice];
        }

        private void DFS(Digraph digraph, Int32 vertice)
        {
            this._reachableFromSourceMap[vertice] = true;

            foreach (Int32 adjacentVertice in digraph.GetAdjacentVertices(vertice))
            {
                if (!this._reachableFromSourceMap[adjacentVertice])
                    this.DFS(digraph, adjacentVertice);
            }
        }
    }
}
