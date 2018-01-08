using Algorithms.Structures.Union;
using Xunit;

namespace Algorithms.Test.Structures.Union
{
    public sealed class WeightedQuickUnionFinderTest
    {
        [Fact]
        public void Test_Find()
        {
            WeightedQuickUnionFinder uf = new WeightedQuickUnionFinder(3);
            uf.Union(0, 1);
            uf.Union(0, 2);

            Assert.Equal(0, uf.Find(1));
        }

        [Fact]
        public void Test_IsConnected()
        {
            WeightedQuickUnionFinder uf = new WeightedQuickUnionFinder(3);
            uf.Union(0, 1);
            uf.Union(0, 2);

            Assert.True(uf.IsConnected(0, 1));
        }

        [Fact]
        public void Test_NotConnected()
        {
            WeightedQuickUnionFinder uf = new WeightedQuickUnionFinder(3);
            uf.Union(0, 1);

            Assert.False(uf.IsConnected(0, 2));
        }

        [Fact]
        public void Test_Count()
        {
            WeightedQuickUnionFinder uf = new WeightedQuickUnionFinder(5);
            //Component 1
            uf.Union(0, 1);
            uf.Union(0, 2);

            //COmponent 2
            uf.Union(3, 4);

            Assert.Equal(2, uf.Count);
        }
    }
}
