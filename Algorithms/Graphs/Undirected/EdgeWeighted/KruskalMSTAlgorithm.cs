using System;
using System.Collections.Generic;
using Algorithms.Structures.PriorityQueue;
using Algorithms.Structures.Union;

namespace Algorithms.Graphs.Undirected.EdgeWeighted
{
    /// <summary>
    /// Computes a minimum spanning tree in an edge-weighted graph.
    /// Return edges odered by weight.
    /// </summary>
    public sealed class KruskalMSTAlgorithm : IMinimumSpanningTreeAlgorithm
    {
        private readonly Queue<Edge> _mst;

        public Double Weight { get; private set; }

        public static KruskalMSTAlgorithm Create(EdgeWeightedGraph graph)
        {
            return new KruskalMSTAlgorithm(graph);
        }

        private KruskalMSTAlgorithm(EdgeWeightedGraph graph)
        {
            // It is more efficient to build a heap by passing array of edges.
            MinPQ<Edge> crossingEdgesByWeight = new MinPQ<Edge>(1);

            this._mst = new Queue<Edge>();

            foreach (Edge edge in graph.GetEdges())
            {
                crossingEdgesByWeight.Add(edge);
            }

            // Greedy algorithm
            UnionFinder uf = new UnionFinder(graph.VerticesCount);

            while (!crossingEdgesByWeight.IsEmpty && this._mst.Count < graph.VerticesCount - 1)
            {
                Edge edge = crossingEdgesByWeight.DeleteMin();

                Int32 sourceVertice = edge.Source;
                Int32 targetVertice = edge.Target;

                if (!uf.IsConnected(sourceVertice, targetVertice))
                {
                    // sourceVertice - targetVertice does not create a cycle.
                    uf.Union(sourceVertice, targetVertice); // Merge sourcerVertice and targetVertice components.

                    this._mst.Enqueue(edge);  // Add edge to minimum spanning tree.

                    this.Weight += edge.Weight;
                }
            }
        }

        public IEnumerable<Edge> GetEdges()
        {
            return this._mst;
        }
    }
}
