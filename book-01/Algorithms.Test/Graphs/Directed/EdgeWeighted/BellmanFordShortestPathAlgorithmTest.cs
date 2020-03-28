using System;
using Algorithms.Graphs.Directed.EdgeWeighted;
using Xunit;

namespace Algorithms.Test.Graphs.Directed.EdgeWeighted
{
    public sealed class BellmanFordShortestPathAlgorithmTest
    {
        [Fact]
        public void Test_DistanceTo()
        {
            BellmanFordShortestPathAlgorithm alg = new BellmanFordShortestPathAlgorithm(this.CreateDigraph(), 0);

            Assert.Equal(1.4, Math.Round(alg.DistanceTo(4),1));
        }

        [Fact]
        public void Test_HasPathTo()
        {
            BellmanFordShortestPathAlgorithm alg = new BellmanFordShortestPathAlgorithm(this.CreateDigraph(), 0);

            Assert.True(alg.HasPathTo(4));
        }

        [Fact]
        public void Test_NotHasPathTo()
        {
            BellmanFordShortestPathAlgorithm alg = new BellmanFordShortestPathAlgorithm(this.CreateDigraph(), 0);

            Assert.False(alg.HasPathTo(7));
        }

        [Fact]
        public void Test_PathTo()
        {
            BellmanFordShortestPathAlgorithm alg = new BellmanFordShortestPathAlgorithm(this.CreateDigraph(), 0);

            AssertUtilities.Sequence(new Edge[4] 
            {
                new Edge(0, 1, .5),
                new Edge(1, 5, .3),
                new Edge(5, 6, .5),
                new Edge(6, 4, .1)
            },
            alg.PathTo(4));
        }

        private EdgeWeightedDigraph CreateDigraph()
        {
            EdgeWeightedDigraph graph = new EdgeWeightedDigraph(9);

            //      2  -[0.3]->  3
            //      ^            ^
            //     /            /    
            //  [0.2]        [0.3]
            //   /            /
            //  0  -[0.5]->  1  -[1.2]->  4
            //                \            ^
            //                 \            \
            //                [0.3]        [0.1]
            //                   \            \
            //                    5  -[0.5]->  6
            //
            //  7  -[0.1]->  8

            graph.AddEdge(new Edge(0, 1, .5));
            graph.AddEdge(new Edge(0, 2, .2));
            graph.AddEdge(new Edge(1, 3, .3));
            graph.AddEdge(new Edge(1, 4, 1.2));
            graph.AddEdge(new Edge(1, 5, .3));
            graph.AddEdge(new Edge(2, 3, .3));
            graph.AddEdge(new Edge(5, 6, .5));
            graph.AddEdge(new Edge(6, 4, .1));
            graph.AddEdge(new Edge(7, 8, .1));

            return graph;
        }
    }
}
