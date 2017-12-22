using Algorithms.Graphs;
using Xunit;

namespace Algorithms.Test.Graphs
{
    public sealed class CycleCheckerTest
    {
        [Fact]
        public void Test_Has_SelfLoop()
        {
            Graph graph = new Graph(1);
            graph.AddEdge(0, 0);

            Assert.True(CycleChecker.HasCycle(graph));
        }

        [Fact]
        public void Test_Has_ParallelEdge()
        {
            Graph graph = new Graph(2);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 1);

            Assert.True(CycleChecker.HasCycle(graph));
        }

        [Fact]
        public void Test_NoCycle()
        {
            Graph graph = new Graph(3);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);

            Assert.False(CycleChecker.HasCycle(graph));
        }
    }
}
