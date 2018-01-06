using Algorithms.Graphs.Directed;
using Xunit;

namespace Algorithms.Test.Graphs.Directed
{
    public sealed class KosarajuSharirSCCTest
    {
        [Fact]
        public void Test_Count()
        {
            KosarajuSharirSCC scc = this.CreateSCC();

            Assert.Equal(4, scc.Count);
        }

        [Fact]
        public void Test_IsStronglyConnected()
        {
            KosarajuSharirSCC scc = this.CreateSCC();

            Assert.True(scc.IsStronglyConnected(0, 1));
        }

        [Fact]
        public void Test_NotStronglyConnected()
        {
            KosarajuSharirSCC scc = this.CreateSCC();

            Assert.False(scc.IsStronglyConnected(0, 4));
        }

        [Fact]
        public void Test_Id()
        {
            KosarajuSharirSCC scc = this.CreateSCC();

            Assert.Equal(3, scc.Id(0));
            Assert.Equal(1, scc.Id(4));
        }

        private KosarajuSharirSCC CreateSCC()
        {
            Digraph graph = new Digraph(7);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);
            graph.AddEdge(3, 5);
            graph.AddEdge(4, 5);
            graph.AddEdge(6, 4);
            graph.AddEdge(4, 6);

            return KosarajuSharirSCC.Create(graph);
        }
    }
}
