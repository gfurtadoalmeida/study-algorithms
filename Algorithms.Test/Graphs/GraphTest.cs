using System;
using System.Collections.Generic;
using Algorithms.Graphs;
using Xunit;

namespace Algorithms.Test.Graphs
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

            using (IEnumerator<Int32> enumerator = graph.GetAdjacentVertices(0))
            {
                enumerator.MoveNext();

                Assert.Equal(2, enumerator.Current);

                enumerator.MoveNext();

                Assert.Equal(1, enumerator.Current);
            }
        }
    }
}
