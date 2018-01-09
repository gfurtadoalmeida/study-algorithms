using System;
using System.Collections.Generic;
using Algorithms.Graphs.Directed.EdgeWeighted;
using AST = Algorithms.Structures;

namespace Algorithms.Graphs.Directed
{
    public sealed class DepthFirstOrder
    {
        private Boolean[] _visited;
        private Int32[] _pre;                 // pre[v] = preOrder number of v.
        private Int32[] _post;                // post[v] = postOrder number of v.
        private AST.Queue<Int32> _preOrder;   // vertices in preOrder.
        private AST.Queue<Int32> _postOrder;  // vertices in postOrder.
        private Int32 _preCounter;            // counter or preorder numbering.
        private Int32 _postCounter;           // counter for postorder numbering.

        public DepthFirstOrder(Digraph digraph)
        {
            this._pre = new Int32[digraph.VerticesCount];
            this._post = new Int32[digraph.VerticesCount];
            this._postOrder = new AST.Queue<Int32>();
            this._preOrder = new AST.Queue<Int32>();
            this._visited = new Boolean[digraph.VerticesCount];

            for (int i = 0; i < digraph.VerticesCount; i++)
            {
                if (!this._visited[i])
                    this.DFS(digraph, i);
            }
        }

        public DepthFirstOrder(EdgeWeightedDigraph weightedGraph)
        {
            this._pre = new Int32[weightedGraph.VerticesCount];
            this._post = new Int32[weightedGraph.VerticesCount];
            this._postOrder = new AST.Queue<Int32>();
            this._preOrder = new AST.Queue<Int32>();
            this._visited = new Boolean[weightedGraph.VerticesCount];

            for (int i = 0; i < weightedGraph.VerticesCount; i++)
            {
                if (!this._visited[i])
                    this.DFS(weightedGraph, i);
            }
        }

        private void DFS(Digraph graph, Int32 vertice)
        {
            this._visited[vertice] = true;
            this._pre[vertice] = this._preCounter++;
            this._preOrder.Enqueue(vertice);

            foreach (Int32 adjacent in graph.GetAdjacentVertices(vertice))
            {
                if (!this._visited[adjacent])
                {
                    this.DFS(graph, adjacent);
                }
            }

            this._postOrder.Enqueue(vertice);
            this._post[vertice] = this._postCounter++;
        }

        private void DFS(EdgeWeightedDigraph graph, Int32 vertice)
        {
            this._visited[vertice] = true;
            this._pre[vertice] = this._preCounter++;
            this._preOrder.Enqueue(vertice);

            foreach (Edge edge in graph.GetAdjacentVertices(vertice))
            {
                if (!this._visited[edge.Target])
                {
                    this.DFS(graph, edge.Target);
                }
            }

            this._postOrder.Enqueue(vertice);
            this._post[vertice] = this._postCounter++;
        }

        public Int32 Pre(Int32 vertice)
        {
            return this._pre[vertice];
        }

        public Int32 Post(Int32 vertice)
        {
            return this._post[vertice];
        }

        public IEnumerable<Int32> Post()
        {
            return this._postOrder;
        }

        public IEnumerable<Int32> Pre()
        {
            return this._preOrder;
        }

        public IEnumerable<Int32> ReversePost()
        {
            AST.Stack<Int32> reverse = new AST.Stack<Int32>();

            foreach (Int32 vertice in this._postOrder)
                reverse.Push(vertice);

            return reverse;
        }
    }
}
