using System;
using Algorithms.Graphs.Undirected;
using Xunit;

namespace Algorithms.Test.Graphs.Undirected
{
    public sealed class GraphTest
    {
        [Fact]
        public void Test_EdgesCount_OnCreation()
        {
            Graph graph = new Graph(1);

            Assert.Equal(0, graph.EdgesCount);
        }

        [Fact]
        public void Test_VerticesCount_OnCreation()
        {
            Graph graph = new Graph(1);

            Assert.Equal(1, graph.VerticesCount);
        }

        [Fact]
        public void Test_Counts_WhenAdding()
        {
            Graph graph = new Graph(3);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);

            Assert.Equal(3, graph.EdgesCount);
            Assert.Equal(3, graph.VerticesCount);
        }

        [Fact]
        public void Test_Iteration()
        {
            Graph graph = new Graph(3);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);

            AssertUtilities.Sequence(new Int32[2] { 2, 1 }, graph.GetAdjacentVertices(0));
        }
    }
}
