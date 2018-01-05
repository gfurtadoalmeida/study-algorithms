using System;
using System.Collections.Generic;
using AST = Algorithms.Structures;

namespace Algorithms.Graphs.Directed
{
    [Flags]
    public enum DepthFirstSearchOrderType : byte
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
        /// Normal ordering but reading from left to righ.
        /// </summary>
        ReversePost = 1 << 2
    }

    public sealed class DepthFirstSearchOrder
    {
        private readonly Boolean[] _marked;
        private readonly AST.Queue<Int32> _pre;
        private readonly AST.Queue<Int32> _post;
        private readonly AST.Stack<Int32> _reversePost;

        public DepthFirstSearchOrderType OrderType { get; }

        public IEnumerable<Int32> Pre => this._pre;

        public IEnumerable<Int32> Post => this._post;

        public IEnumerable<Int32> ReversePost => this._reversePost;

        public DepthFirstSearchOrder(Digraph digraph, DepthFirstSearchOrderType orderType)
        {
            this.OrderType = orderType;

            this._pre = (orderType & DepthFirstSearchOrderType.Pre) == DepthFirstSearchOrderType.Pre ? new AST.Queue<Int32>() : null;
            this._post = (orderType & DepthFirstSearchOrderType.Post) == DepthFirstSearchOrderType.Post ? new AST.Queue<Int32>() : null;
            this._reversePost = (orderType & DepthFirstSearchOrderType.ReversePost) == DepthFirstSearchOrderType.ReversePost ? new AST.Stack<Int32>() : null;

            this._marked = new Boolean[digraph.VerticesCount];

            for (int i = 0; i < digraph.VerticesCount; i++)
            {
                if (!this._marked[i])
                    this.DFS(digraph, i);
            }
        }

        private void DFS(Digraph digraph, Int32 vertice)
        {
            this._pre?.Enqueue(vertice);

            this._marked[vertice] = true;

            foreach (Int32 w in digraph.GetAdjacentVertices(vertice))
            {
                if (!this._marked[w])
                    this.DFS(digraph, w);
            }

            this._post?.Enqueue(vertice);
            this._reversePost?.Push(vertice);
        }
    }
}
