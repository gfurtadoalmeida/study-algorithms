using System;
using System.Collections.Generic;
using Algorithms.Structures;

namespace Algorithms.Graphs.Undirected
{
    /// <summary>
    /// Used on connectivity problems.
    /// v - w
    /// </summary>
    public sealed class Graph : IGraph
    {
        private readonly Bag<Int32>[] _adjacencyVertices;

        public Int32 VerticesCount { get; private set; }

        public Int32 EdgesCount { get; private set; }

        public Graph(Int32 verticesCount)
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

            // Each bag holds all the vertices that connects to the vertice.
            // So we need to add each the vertice to the "verticeIndex" and
            // "adjacentVerticeIndex".

            this._adjacencyVertices[verticeIndex].Add(adjacentVerticeIndex);
            this._adjacencyVertices[adjacentVerticeIndex].Add(verticeIndex);

            this.EdgesCount++;
        }

        public IEnumerable<Int32> GetAdjacentVertices(Int32 verticeIndex)
        {
            this.ThrowIfVerticeIndexOutOfRange(nameof(verticeIndex), verticeIndex);

            return this._adjacencyVertices[verticeIndex];
        }

        private void ThrowIfVerticeIndexOutOfRange(String parameterName, Int32 verticeIndex)
        {
            if (verticeIndex < 0 || verticeIndex > this._adjacencyVertices.Length - 1)
                throw new ArgumentOutOfRangeException(parameterName, 
                                                      "Value must be greater or equal to zero and less than " + this.VerticesCount);
        }
    }
}
