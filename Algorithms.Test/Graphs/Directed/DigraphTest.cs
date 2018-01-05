using System;
using System.Collections.Generic;
using Algorithms.Graphs.Directed;
using Xunit;

namespace Algorithms.Test.Graphs.Directed
{
    public sealed class DigraphTest
    {
        [Fact]
        public void Test_EdgesCount_OnCreation()
        {
            Digraph graph = new Digraph(1);

            Assert.Equal(0, graph.EdgesCount);
        }

        [Fact]
        public void Test_VerticesCount_OnCreation()
        {
            Digraph graph = new Digraph(1);

            Assert.Equal(1, graph.VerticesCount);
        }

        [Fact]
        public void Test_Counts_WhenAdding()
        {
            Digraph graph = new Digraph(3);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);

            Assert.Equal(3, graph.EdgesCount);
            Assert.Equal(3, graph.VerticesCount);
        }

        [Fact]
        public void Test_Iteration()
        {
            Digraph graph = new Digraph(3);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);

            using (IEnumerator<Int32> enumerator = graph.GetAdjacentVertices(0).GetEnumerator())
            {
                enumerator.MoveNext();

                Assert.Equal(1, enumerator.Current);
                Assert.False(enumerator.MoveNext());
            }
        }

        [Fact]
        public void Test_Reverse()
        {
            Digraph graph = new Digraph(3);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);

            graph = graph.Reverse();

            AssertUtilities.Sequence(new Int32[1] { 0 }, graph.GetAdjacentVertices(1));
        }
    }
}
