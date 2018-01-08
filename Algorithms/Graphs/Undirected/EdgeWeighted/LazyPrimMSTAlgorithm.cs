using System;
using System.Collections.Generic;
using Algorithms.Structures.PriorityQueue;
using AST = Algorithms.Structures;

namespace Algorithms.Graphs.Undirected.EdgeWeighted
{
    /// <summary>
    /// Computes a minimum spanning tree in an edge-weighted graph.
    /// Uses a lazy version of Prim's algorithm.
    /// Lazy because we leave ineligible edges in the priority queue.
    /// Return edges ordered by vertice and weigth.
    /// </summary>
    public sealed class LazyPrimMSTAlgorithm : IMinimumSpanningTreeAlgorithm
    {
        private Boolean[] _visited; // visited[v] = true if v on tree, false otherwise.
        private AST.Queue<Edge> _minimunSpanningTreeEdges;
        private MinPQ<Edge> _crossingEdgesByWeight;

        public Double Weight { get; private set; }

        public static LazyPrimMSTAlgorithm Create(EdgeWeightedGraph graph)
        {
            return new LazyPrimMSTAlgorithm(graph);
        }

        private LazyPrimMSTAlgorithm(EdgeWeightedGraph graph)
        {
            this._visited = new Boolean[graph.VerticesCount];
            this._crossingEdgesByWeight = new MinPQ<Edge>(1);
            this._minimunSpanningTreeEdges = new AST.Queue<Edge>();

            // Run Prim from all vertices to get a minimum spanning forest.
            for (int i = 0; i < graph.VerticesCount; i++)
            {
                if (!this._visited[i])
                    this.Prim(graph, i);
            }
        }

        private void Prim(EdgeWeightedGraph graph, Int32 vertice)
        {
            this.Visit(graph, vertice); // Assumes graph is connected.

            while (!this._crossingEdgesByWeight.IsEmpty)
            {
                Edge crossingEdge = this._crossingEdgesByWeight.DeleteMin(); // Get the lowest-weight.
                Int32 sourceVertice = crossingEdge.Source;
                Int32 targetVertice = crossingEdge.Target;

                // Lazy, both sourceVertice and targetVertice already scanned.
                if (this._visited[sourceVertice] && this._visited[targetVertice])
                    continue;

                // Add edge to tree.
                this._minimunSpanningTreeEdges.Enqueue(crossingEdge);

                this.Weight += crossingEdge.Weight;

                // Add vertex to tree.
                if (!this._visited[sourceVertice])
                    this.Visit(graph, sourceVertice);

                // Add vertex to tree.
                if (!this._visited[targetVertice])
                    this.Visit(graph, targetVertice);
            }
        }

        public IEnumerable<Edge> GetEdges()
        {
            return this._minimunSpanningTreeEdges;
        }

        private void Visit(EdgeWeightedGraph graph, Int32 vertice)
        {
            // Mark vertice and add to pq all edges from vertice to unmarked vertices.
            this._visited[vertice] = true;

            foreach (Edge edge in graph.GetAdjacentVertices(vertice))
            {
                if (!this._visited[edge.Other(vertice)])
                    this._crossingEdgesByWeight.Add(edge);
            }
        }
    }
}
