using System;
using System.Collections.Generic;
using AST = Algorithms.Structures;

namespace Algorithms.Graphs
{
    /// <summary>
    /// Traverses a graph in a breadthward motion. 
    /// Goal: discover the shortest path to a vertice.
    /// </summary>
    public sealed class BreadthFirstSearch
    {
        // For each vertice on the graph, have a boolean
        // to mark if a short path was found.
        private readonly Boolean[] _shortestPathToVerticeMap;

        // For each vertice on the graph, store the last vertice
        // that was used to access the vertice _edgeTo[x]
        private readonly Int32[] _edgeTo;

        public Int32 SourceVertice { get; }

        public static BreadthFirstSearch Create(IGraph graph, Int32 sourceVertice)
        {
            return new BreadthFirstSearch(graph, sourceVertice);
        }

        private BreadthFirstSearch(IGraph graph, Int32 sourceVertice)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            if (sourceVertice < 0 || sourceVertice > graph.VerticesCount - 1)
                throw new ArgumentOutOfRangeException(nameof(sourceVertice),
                                                      "Value must be greater or equal to zero and less than " + graph.VerticesCount);

            this._shortestPathToVerticeMap = new Boolean[graph.VerticesCount];
            this._edgeTo = new int[graph.VerticesCount];

            this.SourceVertice = sourceVertice;

            this.BFS(graph, sourceVertice);
        }

        public Boolean IsConnectedToSource(Int32 vertice)
        {
            if (vertice < 0 || vertice > this._shortestPathToVerticeMap.Length - 1)
                throw new ArgumentOutOfRangeException(nameof(vertice),
                                                      "Value must be greater or equal to zero and less than " + this._shortestPathToVerticeMap.Length);

            return this._shortestPathToVerticeMap[vertice];
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

        private void BFS(IGraph graph, Int32 sourceVertice)
        {
            AST.Queue<Int32> queue = new AST.Queue<Int32>();

            // We start at source, so we're the shortest path to source.
            // Shortest path found.
            this._shortestPathToVerticeMap[sourceVertice] = true;

            queue.Enqueue(sourceVertice);

            while (!queue.IsEmpty)
            {
                Int32 vertice = queue.Dequeue(); // Remove the next vertice from the queue.

                foreach (Int32 adjacentVertice in graph.GetAdjacentVertices(vertice))
                {
                    // For every unmarked adjacent vertice:
                    //   - Save the last vertice that connected to it.
                    //   - Mark the shortest path for the adjacent vertice as found.
                    //   - Add it to the queue to be visied later.
                    // When no more adjacent vertices are found, start dequeuing vertices
                    // so we can visit its adjacent vertices.

                    if (!this._shortestPathToVerticeMap[adjacentVertice])
                    {
                        this._edgeTo[adjacentVertice] = vertice;
                        this._shortestPathToVerticeMap[adjacentVertice] = true;

                        queue.Enqueue(adjacentVertice);
                    }
                }
            }
        }
    }
}