using System;
using Algorithms.Graphs;
using Algorithms.Graphs.Undirected;
using Xunit;

namespace Algorithms.Test.Graphs
{
    public sealed class DepthFirstSearchTest
    {
        [Fact]
        public void Test_ConnectedToSourceCount()
        {
            DepthFirstSearch dfs = this.CreateDFS();

            Assert.True(dfs.IsConnectedToSource(5));
        }

        [Fact]
        public void Test_SourceVertice()
        {
            DepthFirstSearch dfs = this.CreateDFS();

            Assert.Equal(5, dfs.SourceVertice);
        }

        [Fact]
        public void Test_IsConnectedToSource_True()
        {
            DepthFirstSearch dfs = this.CreateDFS();

            Assert.True(dfs.IsConnectedToSource(3));
        }

        [Fact]
        public void Test_IsConnectedToSource_False()
        {
            DepthFirstSearch dfs = this.CreateDFS();

            Assert.False(dfs.IsConnectedToSource(6));
        }

        [Fact]
        public void Test_Path_Iteration()
        {
            DepthFirstSearch dfs = this.CreateDFS();

            AssertUtilities.Sequence(new Int32[2] { 5, 3 }, dfs.PathFromSourceTo(3));
        }

        private DepthFirstSearch CreateDFS()
        {
            Graph graph = new Graph(8);
            graph.AddEdge(0, 2);
            graph.AddEdge(2, 1);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(3, 5);
            graph.AddEdge(6, 7);

            return DepthFirstSearch.Create(graph, 5);
        }
    }
}
