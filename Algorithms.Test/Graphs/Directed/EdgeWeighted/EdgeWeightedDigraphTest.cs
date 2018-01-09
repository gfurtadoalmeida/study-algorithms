using Algorithms.Graphs.Directed.EdgeWeighted;
using Xunit;

namespace Algorithms.Test.Graphs.Directed.EdgeWeighted
{
    public sealed class EdgeWeightedDigraphTest
    {
        [Fact]
        public void Test_EdgesCount_OnCreation()
        {
            EdgeWeightedDigraph graph = new EdgeWeightedDigraph (1);

            Assert.Equal(0, graph.EdgesCount);
        }

        [Fact]
        public void Test_VerticesCount_OnCreation()
        {
            EdgeWeightedDigraph graph = new EdgeWeightedDigraph(1);

            Assert.Equal(1, graph.VerticesCount);
        }

        [Fact]
        public void Test_Counts_WhenAdding()
        {
            EdgeWeightedDigraph graph = new EdgeWeightedDigraph(3);
            graph.AddEdge(new Edge(0, 1, 0.5));
            graph.AddEdge(new Edge(1, 2, 0.2));
            graph.AddEdge(new Edge(2, 0, 0.2));

            Assert.Equal(3, graph.EdgesCount);
            Assert.Equal(3, graph.VerticesCount);
        }

        [Fact]
        public void Test_Iteration()
        {
            EdgeWeightedDigraph  graph = new EdgeWeightedDigraph(3);

            Edge e0 = new Edge(0, 1, 0.5);

            graph.AddEdge(e0);
            graph.AddEdge(new Edge(1, 2, 0.2));
            graph.AddEdge(new Edge(2, 0, 0.1));

            AssertUtilities.Sequence(new Edge[1] { e0 }, graph.GetAdjacentVertices(0));
        }

        [Fact]
        public void Test_GetEdges_Iteration()
        {
            EdgeWeightedDigraph graph = new EdgeWeightedDigraph(3);

            Edge e0 = new Edge(0, 1, .5);
            Edge e1 = new Edge(1, 2, 0.2);
            Edge e2 = new Edge(2, 0, 0.1);

            graph.AddEdge(e0);
            graph.AddEdge(e1);
            graph.AddEdge(e2);

            AssertUtilities.Sequence(new Edge[3] { e2, e1, e0 }, graph.GetEdges());
        }
    }
}
