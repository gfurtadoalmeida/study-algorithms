using DataStructures.Union;
using Xunit;

namespace DataStructures.Test.Union
{
    public sealed class UnionFinderTest
    {
        [Fact]
        public void Test_Find()
        {
            UnionFinder uf = new UnionFinder(3);
            uf.Union(0, 1);
            uf.Union(0, 2);

            Assert.Equal(0, uf.Find(1));
        }

        [Fact]
        public void Test_IsConnected()
        {
            UnionFinder uf = new UnionFinder(3);
            uf.Union(0, 1);
            uf.Union(0, 2);

            Assert.True(uf.IsConnected(0, 1));
        }

        [Fact]
        public void Test_NotConnected()
        {
            UnionFinder uf = new UnionFinder(3);
            uf.Union(0, 1);

            Assert.False(uf.IsConnected(0, 2));
        }

        [Fact]
        public void Test_Count()
        {
            UnionFinder uf = new UnionFinder(5);
            //Component 1
            uf.Union(0, 1);
            uf.Union(0, 2);

            //COmponent 2
            uf.Union(3, 4);

            Assert.Equal(2, uf.Count);
        }
    }
}
