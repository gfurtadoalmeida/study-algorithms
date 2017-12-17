using System;
using System.Collections.Generic;
using Algorithms.Searching;
using Xunit;

namespace Algorithms.Test.Searching
{
    public sealed class BinarySearchTest
    {
        [Fact]
        public void Test_IsEmpty_Count_OnCreation()
        {
            var bin = new BinarySearch<Byte, Char>();

            Assert.True(bin.IsEmpty);
            Assert.Equal(0, bin.Count);
        }

        [Fact]
        public void Test_IsEmpty_Count_WhenCleaning()
        {
            var bin = this.CreateFullBinarySearch();
            bin.Delete(11); // GAP

            Int32 count = bin.Count;

            for (byte i = 1; i <= count; i++)
                bin.Delete(i);

            Assert.True(bin.IsEmpty);
            Assert.Equal(0, bin.Count);
        }

        [Fact]
        public void Test_NotEmpty_WhenAdding()
        {
            var bin = this.CreateFullBinarySearch();

            Assert.False(bin.IsEmpty);
        }

        [Fact]
        public void Test_Add_Get()
        {
            var bin = new BinarySearch<Byte, Char>();

            bin.Add(1, 'A');

            Assert.Equal('A', bin.Get(1));
        }

        [Fact]
        public void Test_Get_KeyNotFoundException()
        {
            var bin = new BinarySearch<Byte, Char>();

            Assert.Throws(typeof(KeyNotFoundException), () => bin.Get(100));
        }

        [Fact]
        public void Test_Delete()
        {
            var bin = new BinarySearch<Byte, Char>();

            bin.Add(1, 'A');
            bin.Delete(1);

            Assert.False(bin.Contains(1));
        }

        [Fact]
        public void Test_Contains()
        {
            var bin = new BinarySearch<Byte, Char>();

            bin.Add(1, 'A');
            bin.Add(2, 'B');

            Assert.True(bin.Contains(1));
            Assert.True(bin.Contains(2));
        }

        [Fact]
        public void Test_Min()
        {
            var bin = this.CreateFullBinarySearch();

            Assert.Equal(1, bin.Min());
        }

        [Fact]
        public void Test_Max()
        {
            var bin = this.CreateFullBinarySearch();

            Assert.Equal(11, bin.Max());
        }

        [Fact]
        public void Test_Floor()
        {
            var bin = this.CreateFullBinarySearch();

            Assert.Equal(9, bin.Floor(10));
        }

        [Fact]
        public void Test_Ceiling()
        {
            var bin = this.CreateFullBinarySearch();

            Assert.Equal(11, bin.Ceiling(10));
        }

        [Fact]
        public void Test_Rank_Root()
        {
            var bin = this.CreateFullBinarySearch();

            Assert.Equal(5, bin.Rank(6));
        }

        [Fact]
        public void Test_Rank_Left()
        {
            var bin = this.CreateFullBinarySearch();

            Assert.Equal(4, bin.Rank(5));
        }

        [Fact]
        public void Test_Rank_Right()
        {
            var bin = this.CreateFullBinarySearch();

            Assert.Equal(9, bin.Rank(11));
        }

        [Fact]
        public void Test_Select()
        {
            var bin = this.CreateFullBinarySearch();

            Assert.Equal(9, bin.Select(8));
        }

        [Fact]
        public void Test_Select_KeyNotFoundException()
        {
            var bin = this.CreateFullBinarySearch();

            Assert.Throws(typeof(KeyNotFoundException), () => bin.Select(100));
        }

        [Fact]
        private void Test_DeleteMin()
        {
            var bin = this.CreateFullBinarySearch();

            bin.DeleteMin();

            Assert.False(bin.Contains(1));
        }

        [Fact]
        private void Test_DeleteMax()
        {
            var bin = this.CreateFullBinarySearch();

            bin.DeleteMax();

            Assert.False(bin.Contains(11));
        }

        [Fact]
        public void Test_CountBetween()
        {
            var bin = this.CreateFullBinarySearch();

            // You expected 9 right?
            // INternally it's an array. The element 11 (a.k.a GAP)
            // made the creation of an element of index 10 necessary.
            Assert.Equal(10, bin.CountBetween(1, 11));
        }

        [Fact]
        public void Test_Keys()
        {
            var bin = this.CreateFullBinarySearch();
            bin.Delete(11); // GAP

            var keys = bin.Keys().GetEnumerator();

            Byte count = 1;

            while (keys.MoveNext())
                Assert.Equal(count++, keys.Current);
        }

        [Fact]
        public void Test_Keys_Range()
        {
            var bin = this.CreateFullBinarySearch();
            var keys = bin.Keys(5, 9).GetEnumerator();

            Byte count = 5;

            while (keys.MoveNext())
                Assert.Equal(count++, keys.Current);
        }

        private BinarySearch<Byte, Char> CreateFullBinarySearch()
        {
            BinarySearch<Byte, Char> bin = new BinarySearch<Byte, Char>();

            bin.Add(6, 'F');
            bin.Add(4, 'D');
            bin.Add(7, 'G');
            bin.Add(2, 'B');
            bin.Add(11, 'K');
            bin.Add(9, 'I');
            bin.Add(5, 'E');
            bin.Add(8, 'H');
            bin.Add(3, 'C');
            bin.Add(1, 'A');

            return bin;
        }
    }
}
