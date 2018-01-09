using System;
using System.Collections.Generic;
using Algorithms.Structures.PriorityQueue;
using AST = Algorithms.Structures;

namespace Algorithms.Graphs.Directed.EdgeWeighted
{
    /// <summary>
    /// Solves the single-source shortest-paths problem in edge-weighted digraphs with no negative weights.
    /// </summary>
    public sealed class DijkstraShortestPathAlgorithm : IShortestPathAlgorithm
    {
        private readonly Double[] _distTo; // distTo[v] = distance of shortest s->v path.
        private readonly Edge[] _edgeTo;   // edgeTo[v] = last edge on shortest s->v path.
        private readonly IndexedMinPQ<Double> _crossingEdgesByWeight;

        public DijkstraShortestPathAlgorithm(EdgeWeightedDigraph graph, Int32 vertice)
        {
            foreach (Edge edge in graph.GetEdges())
            {
                if (edge.Weight < 0)
                    throw new InvalidOperationException("Edge" + edge + " has negative weight.");
            }

            this._distTo = new Double[graph.VerticesCount];
            this._edgeTo = new Edge[graph.VerticesCount];

            for (int i = 0; i < graph.VerticesCount; i++)
                this._distTo[i] = Double.PositiveInfinity;

            this._distTo[vertice] = 0.0;

            // Relax vertices in order of distance from s.
            this._crossingEdgesByWeight = new IndexedMinPQ<Double>(graph.VerticesCount);
            this._crossingEdgesByWeight.Add(vertice, this._distTo[vertice]);

            while (!this._crossingEdgesByWeight.IsEmpty)
            {
                Int32 v = this._crossingEdgesByWeight.DeleteMin();

                foreach (Edge edge in graph.GetAdjacentVertices(v))
                    Relax(edge);
            }
        }

        public Double DistanceTo(Int32 vertice)
        {
            return this._distTo[vertice];
        }

        public Boolean HasPathTo(Int32 vertice)
        {
            return this._distTo[vertice] < Double.PositiveInfinity;
        }

        public IEnumerable<Edge> PathTo(Int32 vertice)
        {
            if (!this.HasPathTo(vertice))
                return null;

            AST.Stack<Edge> path = new AST.Stack<Edge>();

            for (Edge e = this._edgeTo[vertice]; e != null; e = this._edgeTo[e.Source])
            {
                path.Push(e);
            }

            return path;
        }

        private void Relax(Edge edge)
        {
            Int32 source = edge.Source;
            Int32 target = edge.Target;

            if (this._distTo[target] > this._distTo[source] + edge.Weight)
            {
                this._distTo[target] = this._distTo[source] + edge.Weight;
                this._edgeTo[target] = edge;

                if (this._crossingEdgesByWeight.Contains(target))
                    this._crossingEdgesByWeight.DecreaseItem(target, this._distTo[target]);
                else
                    this._crossingEdgesByWeight.Add(target, this._distTo[target]);
            }
        }
    }
}
