using System;
using Algorithms.Graphs.Directed;
using Xunit;

namespace Algorithms.Test.Graphs.Directed
{
    public sealed class DepthFirstSearchOrderTest
    {
        private static readonly Int32[] PRE_ORDER = new Int32[6] { 0, 3, 4, 5, 1, 2 };
        private static readonly Int32[] POST_ORDER = new Int32[6] { 5, 4, 3, 2, 1, 0 };
        private static readonly Int32[] REVERSE_POST_ORDER = new Int32[6] { 0, 1, 2, 3, 4, 5 };

        [Fact]
        public void Test_Pre()
        {
            DepthFirstSearchOrder order = new DepthFirstSearchOrder(this.CreateDigraph(), DepthFirstSearchOrderType.Pre);

            AssertExtensions.Sequence(PRE_ORDER, order.Pre);
        }

        [Fact]
        public void Test_Post()
        {
            DepthFirstSearchOrder order = new DepthFirstSearchOrder(this.CreateDigraph(), DepthFirstSearchOrderType.Post);

            AssertExtensions.Sequence(POST_ORDER, order.Post);
        }

        [Fact]
        public void Test_ReversePost()
        {
            DepthFirstSearchOrder order = new DepthFirstSearchOrder(this.CreateDigraph(), DepthFirstSearchOrderType.ReversePost);

            AssertExtensions.Sequence(REVERSE_POST_ORDER, order.ReversePost);
        }

        private Digraph CreateDigraph()
        {
            Digraph graph = new Digraph(6);

            //        0
            //       / \ 
            //      3   1 
            //     /     \
            //   4        2
            //  /
            // 5

            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(0, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 5);

            return graph;
        }
    }
}
