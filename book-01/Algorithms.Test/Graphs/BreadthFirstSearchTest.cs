﻿using System;
using Algorithms.Graphs;
using Algorithms.Graphs.Undirected;
using Xunit;

namespace Algorithms.Test.Graphs
{
    public sealed class BreadthFirstSearchTest
    {
        [Fact]
        public void Test_ConnectedToSourceCount()
        {
            BreadthFirstSearch bfs = this.CreateBFS();

            Assert.True(bfs.IsConnectedToSource(5));
        }

        [Fact]
        public void Test_SourceVertice()
        {
            BreadthFirstSearch bfs = this.CreateBFS();

            Assert.Equal(5, bfs.SourceVertice);
        }

        [Fact]
        public void Test_IsConnectedToSource_True()
        {
            BreadthFirstSearch bfs = this.CreateBFS();

            Assert.True(bfs.IsConnectedToSource(3));
        }

        [Fact]
        public void Test_IsConnectedToSource_False()
        {
            BreadthFirstSearch bfs = this.CreateBFS();

            Assert.False(bfs.IsConnectedToSource(6));
        }

        [Fact]
        public void Test_Path_Iteration()
        {
            BreadthFirstSearch bfs = this.CreateBFS();

            AssertUtilities.Sequence(new Int32[2] { 5, 3 }, bfs.PathFromSourceTo(3));
        }

        private BreadthFirstSearch CreateBFS()
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
