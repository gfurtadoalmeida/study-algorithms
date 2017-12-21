using System;
using System.Collections.Generic;

namespace Algorithms.Graphs
{
    // Traverses a graph in a depthward motion
    public sealed class DepthFirstSearch
    {
        // For each vertice on the graph, have a boolean
        // to mark if it connected to the source vertice.
        private Boolean[] _connectedToSourceMap;

        // For each vertice on the graph, store the last vertice
        // that was used to access the vertice _edgesTo[x]
        private Int32[] _edgesTo;

        public Int32 ConnectedToSourceCount { get; private set; }

        public Int32 SourceVertice { get; }

        public static DepthFirstSearch Create(Graph graph, Int32 sourceVertice)
        {
            return new DepthFirstSearch(graph, sourceVertice);
        }

        private DepthFirstSearch(Graph graph, Int32 sourceVertice)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            if (sourceVertice < 0 || sourceVertice > graph.VerticesCount - 1)
                throw new ArgumentOutOfRangeException(nameof(sourceVertice),
                                                      "Value must be greater or equal to zero and less than " + graph.VerticesCount);

            this._connectedToSourceMap = new Boolean[graph.VerticesCount];
            this._edgesTo = new Int32[graph.VerticesCount];

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

            Stack<Int32> path = new Stack<Int32>();

            for (int i = vertice; i != this.SourceVertice; i = this._edgesTo[i])
                path.Push(i);

            path.Push(this.SourceVertice);

            return path;
        }

        private void DFS(Graph graph, Int32 vertice)
        {
            this._connectedToSourceMap[vertice] = true;

            this.ConnectedToSourceCount++;

            using (IEnumerator<Int32> connections = graph.EnumerateConnections(vertice))
            {
                while (connections.MoveNext())
                    if (!this._connectedToSourceMap[connections.Current])
                    {
                        this._edgesTo[connections.Current] = vertice;

                        this.DFS(graph, connections.Current);
                    }
            }
        }
    }
}
