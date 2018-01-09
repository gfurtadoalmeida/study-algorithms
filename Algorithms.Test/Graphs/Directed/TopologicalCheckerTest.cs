using System;
using Algorithms.Graphs.Directed;
using Algorithms.Graphs.Directed.EdgeWeighted;
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

            TopologicalChecker checker = TopologicalChecker.Create(graph);

            Assert.False(checker.IsDAG);
        }

        [Fact]
        public void Test_IsDAG()
        {
            Digraph graph = new Digraph(3);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);

            TopologicalChecker checker = TopologicalChecker.Create(graph);

            Assert.True(checker.IsDAG);
        }

        [Fact]
        public void Test_IsNotDAG_EdgeWeighted()
        {
            EdgeWeightedDigraph graph = new EdgeWeightedDigraph(3);
            graph.AddEdge(new Edge(0, 1, 0.5));
            graph.AddEdge(new Edge(1, 2, 0.3));
            graph.AddEdge(new Edge(2, 0, 2.3));

            TopologicalChecker checker = TopologicalChecker.Create(graph);

            Assert.False(checker.IsDAG);
        }

        [Fact]
        public void Test_IsDAG_EdgeWeighted()
        {
            EdgeWeightedDigraph graph = new EdgeWeightedDigraph(3);
            graph.AddEdge(new Edge(0, 1, 0.2));
            graph.AddEdge(new Edge(0, 2, 0.3));

            TopologicalChecker checker = TopologicalChecker.Create(graph);

            Assert.True(checker.IsDAG);
        }

        [Fact]
        public void Test_Order()
        {
            Digraph graph = new Digraph(4);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(2, 3);

            TopologicalChecker checker = TopologicalChecker.Create(graph);

            AssertUtilities.Sequence(new Int32[4] { 0, 1, 2, 3 }, checker.Order);
        }

        [Fact]
        public void Test_Rank()
        {
            Digraph graph = new Digraph(4);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(2, 3);

            TopologicalChecker checker = TopologicalChecker.Create(graph);

            Assert.Equal(1, checker.Rank(1));
        }
    }
}
