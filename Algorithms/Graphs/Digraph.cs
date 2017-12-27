using System;
using System.Collections.Generic;
using Algorithms.Structures;

namespace Algorithms.Graphs
{
    /// <summary>
    /// Used on reachability problems.
    /// v -> w
    /// </summary>
    public sealed class Digraph
    {
        private Bag<Int32>[] _adjacencyVertices;

        public int VerticesCount { get; private set; }

        public int EdgesCount { get; private set; }

        public Digraph(Int32 verticesCount)
        {
            this.VerticesCount = verticesCount;
            this.EdgesCount = 0;

            this._adjacencyVertices = new Bag<Int32>[verticesCount];

            for (int i = 0; i < verticesCount; i++)
                this._adjacencyVertices[i] = new Bag<Int32>();
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

        public IEnumerator<Int32> GetAdjacentVertices(Int32 verticeIndex)
        {
            this.ThrowIfVerticeIndexOutOfRange(nameof(verticeIndex), verticeIndex);

            return this._adjacencyVertices[verticeIndex].GetEnumerator();
        }

        public Digraph Reverse()
        {
            Digraph digraph = new Digraph(this.VerticesCount);

            for (int i = 0; i < this.VerticesCount; i++)
            {
                using (var adjacents = this.GetAdjacentVertices(i))
                    while (adjacents.MoveNext())
                        digraph.AddEdge(adjacents.Current, i);
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