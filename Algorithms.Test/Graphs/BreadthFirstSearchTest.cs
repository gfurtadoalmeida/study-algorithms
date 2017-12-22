using System;
using System.Collections.Generic;
using Algorithms.Graphs;
using Xunit;

namespace Algorithms.Test.Graphs
{
    public sealed class BreadthFirstSearchTest
    {
        [Fact]
        public void Test_ConnectedToSourceCount()
        {
            BreadthFirstSearch bfs = this.CreateDFS();

            Assert.True(bfs.IsConnectedToSource(5));
        }

        [Fact]
        public void Test_SourceVertice()
        {
            BreadthFirstSearch bfs = this.CreateDFS();

            Assert.Equal(5, bfs.SourceVertice);
        }

        [Fact]
        public void Test_IsConnectedToSource_True()
        {
            BreadthFirstSearch bfs = this.CreateDFS();

            Assert.True(bfs.IsConnectedToSource(3));
        }

        [Fact]
        public void Test_IsConnectedToSource_False()
        {
            BreadthFirstSearch bfs = this.CreateDFS();

            Assert.False(bfs.IsConnectedToSource(6));
        }

        [Fact]
        public void Test_Path_Iteration()
        {
            BreadthFirstSearch bfs = this.CreateDFS();

            using (IEnumerator<Int32> enumerator = bfs.PathFromSourceTo(3).GetEnumerator())
            {
                enumerator.MoveNext();

                Assert.Equal(5, enumerator.Current);

                enumerator.MoveNext();

                Assert.Equal(3, enumerator.Current);

                Assert.False(enumerator.MoveNext());
            }
        }

        private BreadthFirstSearch CreateDFS()
        {
            Graph graph = new Graph(8);
            graph.AddEdge(0, 2);
            graph.AddEdge(2, 1);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(3, 5);
            graph.AddEdge(6, 7);

            return BreadthFirstSearch.Create(graph, 5);
        }
    }
}
