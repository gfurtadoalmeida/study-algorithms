using System;
using Xunit;

namespace DataStructures.Test
{
    public sealed class CompactBoundedArrayTest
    {
        [Fact]
        public void Test_LowerBound()
        {
            var cba = new CompactBoundedArray<Int32>(10, 20);

            Assert.Equal(10, cba.LowerBound);
        }

        [Fact]
        public void Test_UpperBound()
        {
            var cba = new CompactBoundedArray<Int32>(10, 20);

            Assert.Equal(20, cba.UpperBound);
        }

        [Fact]
        public void Test_Lenght()
        {
            var cba = new CompactBoundedArray<Int32>(5, 15);

            Assert.Equal(10, cba.Length);
        }

        [Fact]
        public void Test_InRange()
        {
            var cba = new CompactBoundedArray<Int32>(5, 15);

            Assert.True(cba.InRange(5));
        }

        [Fact]
        public void Test_NotInRange()
        {
            var cba = new CompactBoundedArray<Int32>(5, 15);

            Assert.False(cba.InRange(2));
        }

        [Fact]
        public void Test_Set_ValidIndex()
        {
            var cba = new CompactBoundedArray<Int32>(5, 15);

            cba[5] = 1;

            Assert.Equal(1, cba[5]);
        }

        [Fact]
        public void Test_Set_InvalidIndex()
        {
            var cba = new CompactBoundedArray<Int32>(5, 15);

            Assert.Throws<IndexOutOfRangeException>(() => cba[2] = 1);
        }
    }
}
