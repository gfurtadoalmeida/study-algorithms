using System;
using Algorithms.Graphs.Directed;
using Xunit;

namespace Algorithms.Test.Graphs.Directed
{
    public sealed class DepthFirstSearchOrderTest
    {
        [Fact]
        public void Test_Pre()
        {
            DepthFirstOrder order = new DepthFirstOrder(this.CreateDigraph());

            AssertUtilities.Sequence(new Int32[6] { 0, 3, 4, 5, 1, 2 }, order.Pre());
        }

        [Fact]
        public void Test_Post()
        {
            DepthFirstOrder order = new DepthFirstOrder(this.CreateDigraph());

            AssertUtilities.Sequence(new Int32[6] { 5, 4, 3, 2, 1, 0 }, order.Post());
        }

        [Fact]
        public void Test_ReversePost()
        {
            DepthFirstOrder order = new DepthFirstOrder(this.CreateDigraph());

            AssertUtilities.Sequence(new Int32[6] { 0, 1, 2, 3, 4, 5 }, order.ReversePost());
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
