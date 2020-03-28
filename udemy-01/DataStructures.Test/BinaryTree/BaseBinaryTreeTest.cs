using System;
using DataStructures.BinaryTree;
using Xunit;

namespace DataStructures.Test.BinaryTree
{
    public abstract class BaseBinaryTreeTest
    {
        //        1
        //      /   \
        //     2     3
        //    / \   / \
        //   4   5 6   7
        //  / \
        // 8   9
        private static readonly byte[] VALUES = new byte[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        protected abstract IBinaryTree<byte> CreateInstance(int initialSize);

        [Fact]
        public void Test_Constructor()
        {
            IBinaryTree<byte> bt = this.CreateInstance(1);

            Assert.True(bt.IsEmpty);
        }

        [Fact]
        public void Test_Add_When_New()
        {
            IBinaryTree<byte> bt = this.CreateInstance(1);

            bt.Add(100);

            Assert.True(bt.Contains(100));
            Assert.False(bt.IsEmpty);
        }

        [Fact]
        public void Test_Add()
        {
            IBinaryTree<byte> bt = this.CreateInstance(1);

            bt.Add(10);

            Assert.True(bt.Contains(10));
        }

        [Fact]
        public void Test_Contains_Throws_InvalidOperation_When_Empty()
        {
            IBinaryTree<byte> bt = this.CreateInstance(1);

            Assert.Throws<InvalidOperationException>(() => bt.Contains(10));
        }

        [Fact]
        public void Test_Contains()
        {
            IBinaryTree<byte> bt = this.CreateFullBinaryTree();

            Assert.True(bt.Contains(VALUES[^1]));
        }

        [Fact]
        public void Test_Contains_When_NotFound()
        {
            IBinaryTree<byte> bt = this.CreateFullBinaryTree();

            Assert.False(bt.Contains(255));
        }

        [Fact]
        public void Test_Delete_Throws_InvalidOperation_When_Empty()
        {
            IBinaryTree<byte> bt = this.CreateInstance(1);

            Assert.Throws<InvalidOperationException>(() => bt.Delete(10));
        }

        [Fact]
        public void Test_Delete_Throws_InvalidOperation_When_NotFound()
        {
            IBinaryTree<byte> bt = this.CreateFullBinaryTree();

            Assert.Throws<InvalidOperationException>(() => bt.Delete(100));
        }

        [Fact]
        public void Test_Delete()
        {
            IBinaryTree<byte> bt = this.CreateFullBinaryTree();

            foreach (byte value in VALUES)
            {
                bt.Delete(value);
            }

            Assert.True(bt.IsEmpty);
        }

        [Theory]
        [InlineData(EnumerationMode.PreOrder, new byte[9] { 1, 2, 4, 8, 9, 5, 3, 6, 7 })]
        [InlineData(EnumerationMode.InOrder, new byte[9] { 8, 4, 9, 2, 5, 1, 6, 3, 7 })]
        [InlineData(EnumerationMode.PostOrder, new byte[9] { 8, 9, 4, 5, 2, 6, 7, 3, 1 })]
        [InlineData(EnumerationMode.LevelOrder, new byte[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
        public void Test_Enumerations_Stack(EnumerationMode enumerationMode, byte[] expectedOrder)
        {
            IBinaryTree<byte> bt = this.CreateFullBinaryTree();

            AssertExtensions.Equal(expectedOrder, bt.GetEnumeratorStack(enumerationMode));
        }

        [Theory]
        [InlineData(EnumerationMode.PreOrder, new byte[9] { 1, 2, 4, 8, 9, 5, 3, 6, 7 })]
        [InlineData(EnumerationMode.InOrder, new byte[9] { 8, 4, 9, 2, 5, 1, 6, 3, 7 })]
        [InlineData(EnumerationMode.PostOrder, new byte[9] { 8, 9, 4, 5, 2, 6, 7, 3, 1 })]
        [InlineData(EnumerationMode.LevelOrder, new byte[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
        public void Test_Enumerations_Recursive(EnumerationMode enumerationMode, byte[] expectedOrder)
        {
            IBinaryTree<byte> bt = this.CreateFullBinaryTree();

            AssertExtensions.Equal(expectedOrder, bt.GetEnumeratorRecursive(enumerationMode));
        }

        protected IBinaryTree<byte> CreateFullBinaryTree()
        {
            IBinaryTree<byte> bt = this.CreateInstance(VALUES.Length);

            foreach (byte value in VALUES)
            {
                bt.Add(value);
            }

            return bt;
        }
    }
}
