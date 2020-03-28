using Algorithms.Graphs.Undirected;
using Xunit;

namespace Algorithms.Test.Graphs.Undirected
{
    public sealed class ConnectedComponentsTest
    {
        [Fact]
        public void Test_Count()
        {
            ConnectedComponents cc = this.CreateCC();

            Assert.Equal(2, cc.Count);
        }

        [Fact]
        public void Test_IsConnected()
        {
            ConnectedComponents cc = this.CreateCC();

            Assert.True(cc.IsConnected(0,3));
        }

        [Fact]
        public void Test_NotConnected()
        {
            ConnectedComponents cc = this.CreateCC();

            Assert.False(cc.IsConnected(2, 5));
        }

        [Fact]
        public void Test_Id()
        {
            ConnectedComponents cc = this.CreateCC();

            Assert.Equal(0, cc.Id(3));
            Assert.Equal(1, cc.Id(5));
        }

        private ConnectedComponents CreateCC()
        {
            Graph graph = new Graph(6);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(4, 5);

            return ConnectedComponents.Create(graph);
        }
    }
}
