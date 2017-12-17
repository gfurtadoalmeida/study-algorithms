using System;
using System.Collections.Generic;
using Algorithms.Searching;
using Xunit;

namespace Algorithms.Test.Searching
{
    public sealed class BinarySearchTreeTest
    {
        [Fact]
        public void Test_IsEmpty_Count_OnCreation()
        {
            var bst = new BinarySearchTree<Byte, Char>();

            Assert.True(bst.IsEmpty);
            Assert.Equal(0, bst.Count);
        }

        [Fact]
        public void Test_IsEmpty_Count_WhenCleaning()
        {
            var bst = this.CreateFullBST();
            bst.Delete(11); // GAP

            Int32 count = bst.Count;

            for (byte i = 1; i <= count; i++)
                bst.Delete(i);

            Assert.True(bst.IsEmpty);
            Assert.Equal(0, bst.Count);
        }

        [Fact]
        public void Test_NotEmpty_WhenAdding()
        {
            var bst = this.CreateFullBST();

            Assert.False(bst.IsEmpty);
        }

        [Fact]
        public void Test_Add_Get()
        {
            var bst = new BinarySearchTree<Byte, Char>();

            bst.Add(1, 'A');

            Assert.Equal('A', bst.Get(1));
        }

        [Fact]
        public void Test_Get_KeyNotFoundException()
        {
            var bst = new BinarySearchTree<Byte, Char>();

            Assert.Throws(typeof(KeyNotFoundException), () => bst.Get(100));
        }

        [Fact]
        public void Test_Delete()
        {
            var bst = new BinarySearchTree<Byte, Char>();

            bst.Add(1, 'A');
            bst.Delete(1);

            Assert.False(bst.Contains(1));
        }

        [Fact]
        public void Test_Contains()
        {
            var bst = new BinarySearchTree<Byte, Char>();

            bst.Add(1, 'A');
            bst.Add(2, 'B');

            Assert.True(bst.Contains(1));
            Assert.True(bst.Contains(2));
        }

        [Fact]
        public void Test_Min()
        {
            var bst = this.CreateFullBST();

            Assert.Equal(1, bst.Min());
        }

        [Fact]
        public void Test_Max()
        {
            var bst = this.CreateFullBST();

            Assert.Equal(11, bst.Max());
        }

        [Fact]
        public void Test_Floor()
        {
            var bst = this.CreateFullBST();

            Assert.Equal(9, bst.Floor(10));
        }

        [Fact]
        public void Test_Ceiling()
        {
            var bst = this.CreateFullBST();

            Assert.Equal(11, bst.Ceiling(10));
        }

        [Fact]
        public void Test_Rank_Root()
        {
            var bst = this.CreateFullBST();

            Assert.Equal(5, bst.Rank(6));
        }

        [Fact]
        public void Test_Rank_Left()
        {
            var bst = this.CreateFullBST();

            Assert.Equal(4, bst.Rank(5));
        }

        [Fact]
        public void Test_Rank_Right()
        {
            var bst = this.CreateFullBST();

            Assert.Equal(9, bst.Rank(11));
        }

        [Fact]
        public void Test_Select()
        {
            var bst = this.CreateFullBST();

            Assert.Equal(9, bst.Select(8));
        }

        [Fact]
        public void Test_Select_KeyNotFoundException()
        {
            var bst = this.CreateFullBST();

            Assert.Throws(typeof(KeyNotFoundException), () => bst.Select(100));
        }

        [Fact]
        private void Test_DeleteMin()
        {
            var bst = this.CreateFullBST();

            bst.DeleteMin();

            Assert.False(bst.Contains(1));
        }

        [Fact]
        private void Test_DeleteMax()
        {
            var bst = this.CreateFullBST();

            bst.DeleteMax();

            Assert.False(bst.Contains(11));
        }

        [Fact]
        public void Test_CountBetween()
        {
            var bst = this.CreateFullBST();

            Assert.Equal(9, bst.CountBetween(1, 11));
        }

        [Fact]
        public void Test_Keys()
        {
            var bst = this.CreateFullBST();
            bst.Delete(11); // GAP

            var keys = bst.Keys().GetEnumerator();

            Byte count = 1;

            while (keys.MoveNext())
                Assert.Equal(count++, keys.Current);
        }

        [Fact]
        public void Test_Keys_Range()
        {
            var bst = this.CreateFullBST();
            var keys = bst.Keys(5, 9).GetEnumerator();

            Byte count = 5;

            while (keys.MoveNext())
                Assert.Equal(count++, keys.Current);
        }

        private BinarySearchTree<Byte, Char> CreateFullBST()
        {
            // A, B, C, D, E, F, G, H, I, K
            // 1, 2, 3, 4, 5, 6, 7, 8, 9, 11
            BinarySearchTree<Byte, Char> bst = new BinarySearchTree<Byte, Char>();

            //         6F
            //       /   \
            //     4D     7G  
            //     / \     \
            //   2B  5E    11K
            //   / \       /
            // 1A  3C     9I
            //           /
            //         8H

            bst.Add(6, 'F');
            bst.Add(4, 'D');
            bst.Add(7, 'G');
            bst.Add(2, 'B');
            bst.Add(11, 'K');
            bst.Add(9, 'I');
            bst.Add(5, 'E');
            bst.Add(8, 'H');
            bst.Add(3, 'C');
            bst.Add(1, 'A');

            return bst;
        }
    }
}
