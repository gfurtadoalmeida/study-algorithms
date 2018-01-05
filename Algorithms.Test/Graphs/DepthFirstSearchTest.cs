using System;
using System.Collections.Generic;
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

            using (IEnumerator<Int32> enumerator = dfs.PathFromSourceTo(3).GetEnumerator())
            {
                enumerator.MoveNext();

                Assert.Equal(5, enumerator.Current);

                enumerator.MoveNext();

                Assert.Equal(3, enumerator.Current);

                Assert.False(enumerator.MoveNext());
            }
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
