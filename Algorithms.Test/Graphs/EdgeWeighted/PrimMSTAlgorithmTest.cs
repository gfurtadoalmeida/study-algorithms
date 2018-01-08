using Algorithms.Graphs.EdgeWeighted;
using Xunit;

namespace Algorithms.Test.Graphs.EdgeWheighted
{
    public sealed class PrimMSTAlgorithmTest
    {
        [Fact]
        public void Test_Weigth()
        {
            EdgeWeightedGraph graph = this.CreateGraph();
            PrimMSTAlgorithm msta = PrimMSTAlgorithm.Create(graph);

            Assert.Equal(1.6, msta.Weight);
        }

        [Fact]
        public void Test_GetEdges_Iteration()
        {
            EdgeWeightedGraph graph = this.CreateGraph();
            PrimMSTAlgorithm msta = PrimMSTAlgorithm.Create(graph);

            AssertUtilities.Sequence(new Edge[4]
            {
                new Edge(0, 1, .5),
                new Edge(1, 2, .3),
                new Edge(1, 3, .6),
                new Edge(0, 4, .2)
            },
            msta.GetEdges());
        }

        private EdgeWeightedGraph CreateGraph()
        {
            // Graph
            //      4 -[.8]- 2
            //     /        /
            //   [.2]     [.3]
            //   /        /
            // 0  -[.5]- 1  
            //            \
            //            [.6]
            //               \
            //                3
            //-----------------
            // MST:
            //      4        2
            //     /        /
            //   [.2]     [.3]
            //   /        /
            // 0  -[.5]- 1  
            //            \
            //            [.6]
            //               \
            //                3
            // Weight: 1.6

            EdgeWeightedGraph graph = new EdgeWeightedGraph(5);

            graph.AddEdge(new Edge(0, 1, .5));
            graph.AddEdge(new Edge(0, 4, .2));
            graph.AddEdge(new Edge(1, 3, .6));
            graph.AddEdge(new Edge(1, 2, .3));
            graph.AddEdge(new Edge(4, 2, .8));

            return graph;
        }
    }
}
