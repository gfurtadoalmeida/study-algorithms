using System;
using System.Collections.Generic;
using AST = Algorithms.Structures;

namespace Algorithms.Graphs.Directed.EdgeWeighted
{
    /// <summary>
    /// Solves the single-source shortest-paths problem in edge-weighted digraphs with no negative cycles. 
    /// The edge weights can be positive, negative, or zero.
    /// </summary>
    public sealed class BellmanFordShortestPathAlgorithm
    {
        private Double[] _distTo;         // distTo[v] = distance of shortest s->v path.
        private Edge[] _edgeTo;           // edgeTo[v] = last edge on shortest s->v path.
        private Boolean[] _onQueue;       // onQueue[v] = is v currently on the queue?
        private AST.Queue<Int32> _queue;  // queue of vertices to relax.
        private Int32 _cost;              // number of calls to Relax().
        private IEnumerable<Edge> _cycle; // negative cycle (or null if no such cycle).

        public Boolean HasNegativeCycle => this._cycle != null;

        public BellmanFordShortestPathAlgorithm(EdgeWeightedDigraph graph, Int32 vertice)
        {
            this._distTo = new double[graph.VerticesCount];
            this._edgeTo = new Edge[graph.VerticesCount];
            this._onQueue = new Boolean[graph.VerticesCount];

            for (int v = 0; v < graph.VerticesCount; v++)
                this._distTo[v] = Double.PositiveInfinity;

            this._distTo[vertice] = 0.0;

            this._queue = new AST.Queue<Int32>();
            this._queue.Enqueue(vertice);
            this._onQueue[vertice] = true;

            while (!this._queue.IsEmpty && !this.HasNegativeCycle)
            {
                Int32 v = this._queue.Dequeue();

                this._onQueue[v] = false;

                this.Relax(graph, v);
            }
        }

        public IEnumerable<Edge> GetNegativeCycle()
        {
            return this._cycle;
        }

        public Double DistanceTo(Int32 vertice)
        {
            if (this.HasNegativeCycle)
                throw new InvalidOperationException("Negative cost cycle exists.");

            return this._distTo[vertice];
        }

        public Boolean HasPathTo(Int32 vertice)
        {
            return this._distTo[vertice] < Double.PositiveInfinity;
        }

        public IEnumerable<Edge> PathTo(Int32 vertice)
        {
            if (this.HasNegativeCycle)
                throw new InvalidOperationException("Negative cost cycle exists.");

            if (!this.HasPathTo(vertice))
                return null;

            AST.Stack<Edge> path = new AST.Stack<Edge>();

            for (Edge e = this._edgeTo[vertice]; e != null; e = this._edgeTo[e.Source])
            {
                path.Push(e);
            }

            return path;
        }

        private void FindNegativeCycle() // by finding a cycle in predecessor graph.
        {
            Int32 vertice = this._edgeTo.Length;
            EdgeWeightedDigraph graph = new EdgeWeightedDigraph(vertice);

            for (int i = 0; i < vertice; i++)
            {
                if (this._edgeTo[i] != null)
                    graph.AddEdge(this._edgeTo[i]);
            }

            EdgeWeightedCycleChecker checker = EdgeWeightedCycleChecker.Create(graph);

            this._cycle = checker.GetCycle();
        }

        private void Relax(EdgeWeightedDigraph graph, Int32 vertice)
        {
            foreach (Edge edge in graph.GetAdjacentVertices(vertice))
            {
                Int32 target = edge.Target;

                if (this._distTo[target] > this._distTo[vertice] + edge.Weight)
                {
                    this._distTo[target] = this._distTo[vertice] + edge.Weight;
                    this._edgeTo[target] = edge;

                    if (!this._onQueue[target])
                    {
                        this._queue.Enqueue(target);
                        this._onQueue[target] = true;
                    }
                }

                if (this._cost++ % graph.VerticesCount == 0)
                {
                    this.FindNegativeCycle();

                    if (this.HasNegativeCycle)
                        return;  // Found a negative cycle.
                }
            }
        }
    }
}
