using System;
using Algorithms.Graphs.Directed;
using Xunit;

namespace Algorithms.Test.Graphs.Directed
{
    public sealed class TopologicalCheckerTest
    {
        [Fact]
        public void Test_IsNotDAG()
        {
            Digraph graph = new Digraph(3);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);

            TopologicalChecker checker = new TopologicalChecker(graph);

            Assert.False(checker.IsDAG);
        }

        [Fact]
        public void Test_IsDAG()
        {
            Digraph graph = new Digraph(3);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);

            TopologicalChecker checker = new TopologicalChecker(graph);

            Assert.True(checker.IsDAG);
        }

        [Fact]
        public void Test_Order()
        {
            Digraph graph = new Digraph(4);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(2, 3);

            TopologicalChecker checker = new TopologicalChecker(graph);

            AssertUtilities.Sequence(new Int32[4] { 0, 1, 2, 3 }, checker.Order);
        }
    }
}
