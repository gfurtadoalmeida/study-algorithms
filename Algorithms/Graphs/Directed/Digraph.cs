using System;
using System.Collections.Generic;
using Algorithms.Structures;

namespace Algorithms.Graphs.Directed
{
    /// <summary>
    /// Used on reachability problems.
    /// v -> w
    /// </summary>
    public sealed class Digraph : IGraph
    {
        private readonly Bag<Int32>[] _adjacencyVertices;

        public Int32 VerticesCount { get; private set; }

        public Int32 EdgesCount { get; private set; }

        public Digraph(Int32 verticesCount)
        {
            this.VerticesCount = verticesCount;
            this.EdgesCount = 0;

            this._adjacencyVertices = new Bag<Int32>[verticesCount];

            for (int i = 0; i < verticesCount; i++)
            {
                this._adjacencyVertices[i] = new Bag<Int32>();
            }
        }

        public void AddEdge(Int32 verticeIndex, Int32 adjacentVerticeIndex)
        {
            this.ThrowIfVerticeIndexOutOfRange(nameof(verticeIndex), verticeIndex);
            this.ThrowIfVerticeIndexOutOfRange(nameof(adjacentVerticeIndex), adjacentVerticeIndex);

            // Here lies the difference between Graph and Digraph.
            // On graph we add the connections to both vertices to answer connectivity questions.
            // On digraph we add the connection just to the "verticeIndex" to answer connectivity
            // questions.
            // The relation is v -> w where on a graph is v - w.

            this._adjacencyVertices[verticeIndex].Add(adjacentVerticeIndex);

            this.EdgesCount++;
        }

        public IEnumerable<Int32> GetAdjacentVertices(Int32 verticeIndex)
        {
            this.ThrowIfVerticeIndexOutOfRange(nameof(verticeIndex), verticeIndex);

            return this._adjacencyVertices[verticeIndex];
        }

        public Digraph Reverse()
        {
            Digraph digraph = new Digraph(this.VerticesCount);

            for (int i = 0; i < this.VerticesCount; i++)
            {
                foreach (Int32 adjacentVertice in this.GetAdjacentVertices(i))
                {
                    digraph.AddEdge(adjacentVertice, i);
                }
            }

            return digraph;
        }

        private void ThrowIfVerticeIndexOutOfRange(String parameterName, Int32 verticeIndex)
        {
            if (verticeIndex < 0 || verticeIndex > this._adjacencyVertices.Length - 1)
                throw new ArgumentOutOfRangeException(parameterName,
                                                      "Value must be greater or equal to zero and less than " + this.VerticesCount);
        }
    }
}