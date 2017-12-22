using System;
using System.Collections.Generic;
using Algorithms.Structures;

namespace Algorithms.Graphs
{
    public sealed class Graph
    {
        private Bag<Int32>[] _adjacencyVertices;

        public Int32 VerticesCount { get; private set; }

        public Int32 EdgesCount { get; private set; }

        public Graph(Int32 verticesCount)
        {
            this.VerticesCount = verticesCount;
            this.EdgesCount = 0;

            this._adjacencyVertices = new Bag<Int32>[verticesCount];

            for (int v = 0; v < verticesCount; v++)
                this._adjacencyVertices[v] = new Bag<Int32>();
        }

        public void AddEdge(Int32 verticeIndex, Int32 adjacentVerticeIndex)
        {
            this.ThrowIfVerticeIndexOutOfRange(nameof(verticeIndex), verticeIndex);
            this.ThrowIfVerticeIndexOutOfRange(nameof(adjacentVerticeIndex), adjacentVerticeIndex);

            this._adjacencyVertices[verticeIndex].Add(adjacentVerticeIndex);
            this._adjacencyVertices[adjacentVerticeIndex].Add(verticeIndex);

            this.EdgesCount++;
        }

        public IEnumerator<Int32> GetAdjacentVertices(Int32 verticeIndex)
        {
            this.ThrowIfVerticeIndexOutOfRange(nameof(verticeIndex), verticeIndex);

            return this._adjacencyVertices[verticeIndex].GetEnumerator();
        }

        private void ThrowIfVerticeIndexOutOfRange(String parameterName, Int32 verticeIndex)
        {
            if (verticeIndex < 0 || verticeIndex > this._adjacencyVertices.Length - 1)
                throw new ArgumentOutOfRangeException(parameterName, 
                                                      "Value must be greater or equal to zero and less than " + this.VerticesCount);
        }
    }
}
