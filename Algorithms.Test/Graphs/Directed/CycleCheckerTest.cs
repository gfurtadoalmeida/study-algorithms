using System;
using Algorithms.Graphs.Directed;
using Xunit;

namespace Algorithms.Test.Graphs.Directed
{
    public sealed class CycleCheckerTest
    {
        [Fact]
        public void Test_NoCycle()
        {
            Digraph graph = new Digraph(3);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);

            CycleChecker checker = CycleChecker.Create(graph);

            Assert.False(checker.HasCycle);
        }

        [Fact]
        public void Test_HasCycle()
        {
            Digraph graph = new Digraph(3);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);

            CycleChecker checker = CycleChecker.Create(graph);

            Assert.True(checker.HasCycle);
        }

        [Fact]
        public void Test_GetCycle()
        {
            Digraph graph = new Digraph(3);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);

            CycleChecker checker = CycleChecker.Create(graph);

            AssertUtilities.Sequence(new Int32[4] { 2, 0, 1, 2 }, checker.GetCycle());
        }
    }
}
