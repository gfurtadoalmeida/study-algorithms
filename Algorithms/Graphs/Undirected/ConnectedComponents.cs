using System;

namespace Algorithms.Graphs.Undirected
{
    public sealed class ConnectedComponents
    {
        private Boolean[] _connected;
        private Int32[] _ids;

        /// <summary>
        /// How many componentes are connected.
        /// Being a component an isolated web of connected vertices.
        /// </summary>
        public Int32 Count { get; private set; }

        public static ConnectedComponents Create(Graph graph)
        {
            return new ConnectedComponents(graph);
        }

        private ConnectedComponents(Graph graph)
        {
            this._connected = new Boolean[graph.VerticesCount];
            this._ids = new Int32[graph.VerticesCount];

            // How does it work?
            // For each vertice:
            //   - If the vertice is not already marked we:
            //     - Set its id to the current count.
            //     - Check recursively all its adjacents vertices setting their
            //       ids to the current count.
            //   - Increment count.

            // Even though the property "Count" count how many connected componentes
            // we have it is used to hold the current id while processing.

            for (int i = 0; i < graph.VerticesCount; i++)
                if (!this._connected[i])
                {
                    this.DFS(graph, i);

                    this.Count++;
                }
        }

        public Boolean IsConnected(Int32 sourceVertice, Int32 targetVertice)
        {
            return this._ids[sourceVertice] == this._ids[targetVertice];
        }

        public Int32 Id(Int32 vertice)
        {
            return this._ids[vertice];
        }

        private void DFS(Graph graph, Int32 vertice)
        {
            this._connected[vertice] = true;
            this._ids[vertice] = this.Count;

            foreach (Int32 adjacentVertice in graph.GetAdjacentVertices(vertice))
            {
                if (!this._connected[adjacentVertice])
                    this.DFS(graph, adjacentVertice);
            }
        }
    }
}
