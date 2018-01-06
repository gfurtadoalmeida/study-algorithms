using System;
using System.Collections.Generic;
using Algorithms.Structures;

namespace Algorithms.Graphs.EdgeWheighted
{
    /// <summary>
    /// Used on connectivity problems where edges have wheight.
    /// v - w
    /// </summary>
    public sealed class EdgeWheightedGraph
    {
        private readonly Bag<Edge>[] _adjacencyVertices;

        public Int32 VerticesCount { get; private set; }

        public Int32 EdgesCount { get; private set; }

        public EdgeWheightedGraph(Int32 verticesCount)
        {
            this.VerticesCount = verticesCount;
            this.EdgesCount = 0;

            this._adjacencyVertices = new Bag<Edge>[verticesCount];

            for (int i = 0; i < verticesCount; i++)
                this._adjacencyVertices[i] = new Bag<Edge>();
        }

        public void AddEdge(Edge edge)
        {
            this.ThrowIfVerticeIndexOutOfRange(nameof(edge) + "." + nameof(edge.Source), edge.Source);
            this.ThrowIfVerticeIndexOutOfRange(nameof(edge) + "." + nameof(edge.Target), edge.Target);

            this._adjacencyVertices[edge.Source].Add(edge);
            this._adjacencyVertices[edge.Target].Add(edge);

            this.EdgesCount++;
        }

        public IEnumerable<Edge> GetAdjacentVertices(Int32 verticeIndex)
        {
            this.ThrowIfVerticeIndexOutOfRange(nameof(verticeIndex), verticeIndex);

            return this._adjacencyVertices[verticeIndex];
        }

        public IEnumerable<Edge> GetEdges()
        {
            Bag<Edge> bag = new Bag<Edge>();

            for (int i = 0; i < this.VerticesCount; i++)
            {
                foreach (Edge edge in this.GetAdjacentVertices(i))
                {
                    if (edge.Other(i) > i)
                        bag.Add(edge);
                }
            }

            return bag;
        }

        private void ThrowIfVerticeIndexOutOfRange(String parameterName, Int32 verticeIndex)
        {
            if (verticeIndex < 0 || verticeIndex > this._adjacencyVertices.Length - 1)
                throw new ArgumentException(parameterName,
                                            "Value must be greater or equal to zero and less than " + this.VerticesCount);
        }
    }
}
