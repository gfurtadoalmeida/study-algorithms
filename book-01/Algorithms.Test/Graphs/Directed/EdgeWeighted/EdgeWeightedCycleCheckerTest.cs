using Algorithms.Graphs.Directed.EdgeWeighted;
using Xunit;

namespace Algorithms.Test.Graphs.Directed.EdgeWeighted
{
    public sealed class EdgeWeightedCycleCheckerTest
    {
        [Fact]
        public void Test_NoCycle()
        {
            EdgeWeightedDigraph graph = new EdgeWeightedDigraph(3);
            graph.AddEdge(new Edge(0, 1, .3));
            graph.AddEdge(new Edge(0, 2, .5));

            EdgeWeightedCycleChecker checker = EdgeWeightedCycleChecker.Create(graph);

            Assert.False(checker.HasCycle);
        }

        [Fact]
        public void Test_HasCycle()
        {
            EdgeWeightedDigraph graph = new EdgeWeightedDigraph(3);
            graph.AddEdge(new Edge(0, 1, .3));
            graph.AddEdge(new Edge(1, 2, .2));
            graph.AddEdge(new Edge(2, 0, .5));

            EdgeWeightedCycleChecker checker = EdgeWeightedCycleChecker.Create(graph);

            Assert.True(checker.HasCycle);
        }

        [Fact]
        public void Test_GetCycle()
        {
            EdgeWeightedDigraph graph = new EdgeWeightedDigraph(3);

            Edge e0 = new Edge(0, 1, .3);
            Edge e1 = new Edge(1, 2, .8);
            Edge e2 = new Edge(2, 0, .5);

            graph.AddEdge(e0);
            graph.AddEdge(e1);
            graph.AddEdge(e2);

            EdgeWeightedCycleChecker checker = EdgeWeightedCycleChecker.Create(graph);

            AssertUtilities.Sequence(new Edge[] { e0, e1, e2 }, checker.GetCycle());
        }
    }
}
