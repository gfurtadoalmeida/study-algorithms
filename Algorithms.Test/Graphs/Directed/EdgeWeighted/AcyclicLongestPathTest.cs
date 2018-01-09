using Algorithms.Graphs.Directed.EdgeWeighted;
using Xunit;

namespace Algorithms.Test.Graphs.Directed.EdgeWeighted
{
    public sealed class AcyclicLongestPathTest
    {
        [Fact]
        public void Test_DistanceTo()
        {
            AcyclicLongestPath alp = new AcyclicLongestPath(this.CreateDigraph(), 0);

            Assert.Equal(1.7, alp.DistanceTo(4));
        }

        [Fact]
        public void Test_HasPathTo()
        {
            AcyclicLongestPath alp = new AcyclicLongestPath(this.CreateDigraph(), 0);

            Assert.True(alp.HasPathTo(4));
        }

        [Fact]
        public void Test_NotHasPathTo()
        {
            AcyclicLongestPath alp = new AcyclicLongestPath(this.CreateDigraph(), 0);

            Assert.False(alp.HasPathTo(7));
        }

        [Fact]
        public void Test_PathTo()
        {
            AcyclicLongestPath alp = new AcyclicLongestPath(this.CreateDigraph(), 0);

            AssertUtilities.Sequence(new Edge[2] 
            {
                new Edge(0, 1, .5),
                new Edge(1, 4, 1.2),
            },
            alp.PathTo(4));
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
