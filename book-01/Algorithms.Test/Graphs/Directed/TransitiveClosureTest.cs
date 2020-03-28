using Algorithms.Graphs.Directed;
using Xunit;

namespace Algorithms.Test.Graphs.Directed
{
    public sealed class TransitiveClosureTest
    {
        [Fact]
        public void Test_IsReachable()
        {
            Digraph graph = new Digraph(3);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);

            TransitiveClosure closure = TransitiveClosure.Create(graph);

            Assert.True(closure.IsReachable(0,2));
        }

        [Fact]
        public void Test_NotReachable()
        {
            Digraph graph = new Digraph(4);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(0, 3);

            TransitiveClosure closure = TransitiveClosure.Create(graph);

            Assert.False(closure.IsReachable(3, 1));
        }
    }
}
