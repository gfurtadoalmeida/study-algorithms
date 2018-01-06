using System;
using Algorithms.Graphs.EdgeWeighted;
using Xunit;

namespace Algorithms.Test.Graphs.EdgeWeighted
{
    public sealed class EdgeTest
    {
        [Fact]
        public void Test_Equal()
        {
            Edge e1 = new Edge(0, 1, 5);
            Edge e2 = new Edge(0, 1, 5);

            Assert.Equal(0, e1.CompareTo(e2));
        }

        [Fact]
        public void Test_LessThan()
        {
            Edge e1 = new Edge(0, 1, 2);
            Edge e2 = new Edge(0, 1, 5);

            Assert.Equal(-1, e1.CompareTo(e2));
        }

        [Fact]
        public void Test_BiggerThan()
        {
            Edge e1 = new Edge(0, 1, 5);
            Edge e2 = new Edge(0, 1, 2);

            Assert.Equal(1, e1.CompareTo(e2));
        }

        [Fact]
        public void Test_Other()
        {
            Edge e1 = new Edge(0, 1, 5);

            Assert.Equal(1, e1.Other(0));
            Assert.Equal(0, e1.Other(1));
        }

        [Fact]
        public void Test_Other_Exception()
        {
            Edge e1 = new Edge(0, 1, 5);

            Assert.Throws<Exception>(() => e1.Other(2));
        }
    }
}
