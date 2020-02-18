using System;
using System.Collections.Generic;
using AST = DataStructures;

namespace Algorithms.Graphs.Directed.EdgeWeighted
{
    /// <summary>
    /// Solves the single-source longest-paths problem in edge-weighted directed acyclic graphs(DAGs).
    /// The edge weights can be positive, negative, or zero.
    /// </summary>
    public sealed class AcyclicLongestPath
    {
        private readonly Double[] _distTo; // distTo[v] = distance  of longest s->v path.
        private readonly Edge[] _edgeTo;   // edgeTo[v] = last edge on longest s->v path.

        public AcyclicLongestPath(EdgeWeightedDigraph graph, Int32 vertice)
        {
            this._distTo = new Double[graph.VerticesCount];
            this._edgeTo = new Edge[graph.VerticesCount];

            for (int i = 0; i < graph.VerticesCount; i++)
            {
                this._distTo[i] = Double.NegativeInfinity;
            }

            this._distTo[vertice] = 0.0;

            TopologicalChecker topological = TopologicalChecker.Create(graph);

            if (topological.Order == null)
                throw new InvalidOperationException("Digraph is not acyclic.");

            foreach (Int32 v in topological.Order)
            {
                foreach (Edge edge in graph.GetAdjacentVertices(v))
                {
                    this.Relax(edge);
                }
            }
        }

        public Double DistanceTo(Int32 vertice)
        {
            return this._distTo[vertice];
        }

        public Boolean HasPathTo(Int32 vertice)
        {
            return this._distTo[vertice] > Double.NegativeInfinity;
        }

        public IEnumerable<Edge> PathTo(Int32 vertice)
        {
            if (!this.HasPathTo(vertice))
                return null;

            AST.Stack<Edge> path = new AST.Stack<Edge>();

            for (Edge edge = this._edgeTo[vertice]; edge != null; edge = this._edgeTo[edge.Source])
            {
                path.Push(edge);
            }

            return path;
        }

        private void Relax(Edge edge)
        {
            Int32 sourceVertice = edge.Source;
            Int32 targetVertice = edge.Target;

            if (this._distTo[targetVertice] < this._distTo[sourceVertice] + edge.Weight)
            {
                this._distTo[targetVertice] = this._distTo[sourceVertice] + edge.Weight;
                this._edgeTo[targetVertice] = edge;
            }
        }
    }
}
