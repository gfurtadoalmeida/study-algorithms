using Algorithms.Graphs.Undirected;
using Xunit;

namespace Algorithms.Test.Graphs.Undirected
{
    public sealed class BiPartiteCheckerTest
    {
        [Fact]
        public void Test_IsBiPartite()
        {
            Graph graph = new Graph(4);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 3);
            graph.AddEdge(2, 1);
            graph.AddEdge(2, 3);

            Assert.True(BiPartiteChecker.IsBiPartite(graph));
        }

        [Fact]
        public void Test_NotBiPartite()
        {
            Graph graph = new Graph(4);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 3);
            graph.AddEdge(2, 1);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 1);

            Assert.False(BiPartiteChecker.IsBiPartite(graph));
        }
    }
}
