using System;
using System.Collections.Generic;
using AST = DataStructures;

namespace Algorithms.Graphs
{
    /// <summary>
    /// Traverses a graph in a depthward motion.
    /// Goal: discover the existence of a connection/reachability.
    /// </summary>
    public sealed class DepthFirstSearch
    {
        // For each vertice on the graph, have a boolean
        // to mark if it connected to the source vertice.
        private readonly Boolean[] _connectedToSourceMap;

        // For each vertice on the graph, store the last vertice
        // that was used to access the vertice _edgeTo[x]
        private readonly Int32[] _edgeTo;

        public Int32 ConnectedToSourceCount { get; private set; }

        public Int32 SourceVertice { get; }

        public static DepthFirstSearch Create(IGraph graph, Int32 sourceVertice)
        {
            return new DepthFirstSearch(graph, sourceVertice);
        }

        private DepthFirstSearch(IGraph graph, Int32 sourceVertice)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            if (sourceVertice < 0 || sourceVertice > graph.VerticesCount - 1)
                throw new ArgumentOutOfRangeException(nameof(sourceVertice),
                                                      "Value must be greater or equal to zero and less than " + graph.VerticesCount);

            this._connectedToSourceMap = new Boolean[graph.VerticesCount];
            this._edgeTo = new Int32[graph.VerticesCount];

            this.SourceVertice = sourceVertice;

            this.DFS(graph, sourceVertice);
        }

        public Boolean IsConnectedToSource(Int32 vertice)
        {
            if (vertice < 0 || vertice > this._connectedToSourceMap.Length - 1)
                throw new ArgumentOutOfRangeException(nameof(vertice),
                                                      "Value must be greater or equal to zero and less than " + this._connectedToSourceMap.Length);

            return this._connectedToSourceMap[vertice];
        }

        public IEnumerable<Int32> PathFromSourceTo(Int32 vertice)
        {
            if (!this.IsConnectedToSource(vertice))
                return null;

            AST.Stack<Int32> path = new AST.Stack<Int32>();

            for (int i = vertice; i != this.SourceVertice; i = this._edgeTo[i])
            {
                path.Push(i);
            }

            path.Push(this.SourceVertice);

            return path;
        }

        private void DFS(IGraph graph, Int32 vertice)
        {
            // DFS operates on a recursive way, calling itself for each non-marked
            // adjacent vertice of the current vertice.
            // It will not find the shortest path, but will answer if there is or not a
            // connection to some vertice.

            // Mark it connected because this function will be called only for
            // vertices connected to source.
            this._connectedToSourceMap[vertice] = true;

            this.ConnectedToSourceCount++;

            // For each adjacent vertice connected to "vertice" and not marked:
            //   - Set the last vertice connected to the adjacent vertice to "vertice".
            //   - Recursive call DSF to the adjacent vertices.

            foreach (Int32 adjacentVertice in graph.GetAdjacentVertices(vertice))
            {
                if (!this._connectedToSourceMap[adjacentVertice])
                {
                    this._edgeTo[adjacentVertice] = vertice;

                    this.DFS(graph, adjacentVertice);
                }
            }
        }
    }
}
