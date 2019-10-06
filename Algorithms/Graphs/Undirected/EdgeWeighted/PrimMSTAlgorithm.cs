using System;
using System.Collections.Generic;
using Algorithms.Structures.PriorityQueue;

namespace Algorithms.Graphs.Undirected.EdgeWeighted
{
    /// <summary>
    /// Computes a minimum spanning tree in an edge-weighted graph.
    /// </summary>
    public sealed class PrimMSTAlgorithm : IMinimumSpanningTreeAlgorithm
    {
        private const Double FLOATING_POINT_EPSILON = 1E-12;

        private readonly Edge[] _edgeTo;     // edgeTo[v]  = shortest edge from tree vertex to non-tree vertex.
        private readonly Double[] _distTo;   // distTo[v]  = weight of shortest such edge.
        private readonly Boolean[] _visited; // visited[v] = true if v on tree.
        private readonly IndexedMinPQ<Double> _crossingEdgesByWeight;
        private Lazy<Double> _lazyWeight;

        public Double Weight => _lazyWeight.Value;

        public static PrimMSTAlgorithm Create(EdgeWeightedGraph graph)
        {
            return new PrimMSTAlgorithm(graph);
        }

        private PrimMSTAlgorithm(EdgeWeightedGraph graph)
        {
            this._edgeTo = new Edge[graph.VerticesCount];
            this._distTo = new Double[graph.VerticesCount];
            this._visited = new Boolean[graph.VerticesCount];
            this._crossingEdgesByWeight = new IndexedMinPQ<Double>(graph.VerticesCount);

            for (int i = 0; i < graph.VerticesCount; i++)
                this._distTo[i] = Double.PositiveInfinity;

            for (int i = 0; i < graph.VerticesCount; i++)
            {
                if (!this._visited[i])
                {
                    this.Prim(graph, i);
                }
            }

            this._lazyWeight = new Lazy<Double>(() =>
            {
                Double weight = 0;

                foreach (Edge edge in this.GetEdges())
                {
                    weight += edge.Weight;
                }

                return weight;
            });
        }

        public IEnumerable<Edge> GetEdges()
        {
            Queue<Edge> mst = new Queue<Edge>();

            for (int i = 0; i < this._edgeTo.Length; i++)
            {
                Edge e = this._edgeTo[i];

                if (e != null)
                {
                    mst.Enqueue(e);
                }
            }

            return mst;
        }

        private void Prim(EdgeWeightedGraph graph, Int32 startingVertice)
        {
            this._distTo[startingVertice] = 0.0;
            this._crossingEdgesByWeight.Add(startingVertice, this._distTo[startingVertice]);

            while (!this._crossingEdgesByWeight.IsEmpty)
            {
                this.Visit(graph, this._crossingEdgesByWeight.DeleteMin());
            }
        }

        private void Visit(EdgeWeightedGraph graph, Int32 sourceVertice)
        {
            this._visited[sourceVertice] = true;

            foreach (Edge edge in graph.GetAdjacentVertices(sourceVertice))
            {
                Int32 targetVertice = edge.Other(sourceVertice);

                if (this._visited[targetVertice])
                    continue; // v-w is an obsolete edge.

                if (edge.Weight < this._distTo[targetVertice])
                {
                    this._distTo[targetVertice] = edge.Weight;
                    this._edgeTo[targetVertice] = edge;

                    if (this._crossingEdgesByWeight.Contains(targetVertice))
                    {
                        this._crossingEdgesByWeight.DecreaseItem(targetVertice, this._distTo[targetVertice]);
                    }
                    else
                    {
                        this._crossingEdgesByWeight.Add(targetVertice, this._distTo[targetVertice]);
                    }
                }
            }
        }
    }
}

