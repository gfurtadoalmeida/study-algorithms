using System;
using System.Collections.Generic;
using AST = Algorithms.Structures;

namespace Algorithms.Graphs.Directed.EdgeWeighted
{
    public sealed class EdgeWeightedCycleChecker
    {
        private readonly Boolean[] _visited;
        private readonly Boolean[] _onStack; // onStack[v] = is vertex on the stack?
        private readonly Edge[] _edgeTo;     // edgeTo[v] = previous edge on path to v.
        private AST.Stack<Edge> _cycle;      // Directed cycle (or null if no such cycle).

        public Boolean HasCycle => this._cycle != null;

        public static EdgeWeightedCycleChecker Create(EdgeWeightedDigraph graph)
        {
            return new EdgeWeightedCycleChecker(graph);
        }

        public EdgeWeightedCycleChecker(EdgeWeightedDigraph graph)
        {
            this._visited = new Boolean[graph.VerticesCount];
            this._onStack = new Boolean[graph.VerticesCount];
            this._edgeTo = new Edge[graph.VerticesCount];

            for (int i = 0; i < graph.VerticesCount; i++)
            {
                if (!this._visited[i])
                {
                    this.DFS(graph, i);
                }
            }
        }

        public IEnumerable<Edge> GetCycle()
        {
            return this._cycle;
        }

        private void DFS(EdgeWeightedDigraph graph, Int32 vertice)
        {
            this._onStack[vertice] = true;
            this._visited[vertice] = true;

            foreach (Edge edge in graph.GetAdjacentVertices(vertice))
            {
                Int32 targetVertice = edge.Target;

                // Short circuit if directed cycle found.
                if (this._cycle != null)
                    return;

                // Found new vertex, so recur.
                if (!this._visited[targetVertice])
                {
                    this._edgeTo[targetVertice] = edge;

                    this.DFS(graph, targetVertice);
                }

                // trace back directed cycle
                else if (this._onStack[targetVertice])
                {
                    this._cycle = new AST.Stack<Edge>();

                    Edge tempEdge = edge;

                    while (tempEdge.Source != targetVertice)
                    {
                        this._cycle.Push(tempEdge);

                        tempEdge = this._edgeTo[tempEdge.Source];
                    }

                    this._cycle.Push(tempEdge);

                    return;
                }
            }

            this._onStack[vertice] = false;
        }
    }
}
