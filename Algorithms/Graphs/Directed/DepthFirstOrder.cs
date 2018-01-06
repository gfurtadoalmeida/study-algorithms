using System;
using System.Collections.Generic;
using AST = Algorithms.Structures;

namespace Algorithms.Graphs.Directed
{
    [Flags]
    public enum DepthFirstOrderType : byte
    {
        /// <summary>
        /// Normal ordering.
        /// </summary>
        Pre = 1,

        /// <summary>
        /// Reverse ordering.
        /// </summary>
        Post = 1 << 1,

        /// <summary>
        /// Normal ordering but reading from right to left.
        /// </summary>
        ReversePost = 1 << 2
    }

    public sealed class DepthFirstOrder
    {
        private readonly Boolean[] _visited;
        private readonly AST.Queue<Int32> _pre;
        private readonly AST.Queue<Int32> _post;
        private readonly AST.Stack<Int32> _reversePost;

        public DepthFirstOrderType OrderType { get; }

        public IEnumerable<Int32> Pre => this._pre;

        public IEnumerable<Int32> Post => this._post;

        public IEnumerable<Int32> ReversePost => this._reversePost;

        public DepthFirstOrder(Digraph digraph, DepthFirstOrderType orderType)
        {
            this.OrderType = orderType;

            this._pre = (orderType & DepthFirstOrderType.Pre) == DepthFirstOrderType.Pre ? new AST.Queue<Int32>() : null;
            this._post = (orderType & DepthFirstOrderType.Post) == DepthFirstOrderType.Post ? new AST.Queue<Int32>() : null;
            this._reversePost = (orderType & DepthFirstOrderType.ReversePost) == DepthFirstOrderType.ReversePost ? new AST.Stack<Int32>() : null;

            this._visited = new Boolean[digraph.VerticesCount];

            for (int i = 0; i < digraph.VerticesCount; i++)
            {
                if (!this._visited[i])
                    this.DFS(digraph, i);
            }
        }

        private void DFS(Digraph digraph, Int32 vertice)
        {
            this._pre?.Enqueue(vertice);

            this._visited[vertice] = true;

            foreach (Int32 w in digraph.GetAdjacentVertices(vertice))
            {
                if (!this._visited[w])
                    this.DFS(digraph, w);
            }

            this._post?.Enqueue(vertice);
            this._reversePost?.Push(vertice);
        }
    }
}
