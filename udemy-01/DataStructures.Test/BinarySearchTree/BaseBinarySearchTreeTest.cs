using System;
using DataStructures.BinarySearchTree;
using Xunit;

namespace DataStructures.Test.BinarySearchTree
{
    public abstract class BaseBinarySearchTreeTest
    {
        private static readonly byte[] VALUES = new byte[11] { 30, 20, 40, 10, 5, 3, 4, 50, 60, 70, 65 };

        public abstract IBinarySearchTree<byte> CreateInstance();

        [Fact]
        public void Test_Constructor()
        {
            IBinarySearchTree<byte> bt = this.CreateInstance();

            Assert.True(bt.IsEmpty);
        }

        [Fact]
        public void Test_Add_When_New()
        {
            IBinarySearchTree<byte> bt = this.CreateInstance();

            bt.Add(100);

            Assert.True(bt.Contains(100));
            Assert.False(bt.IsEmpty);
        }

        [Fact]
        public void Test_Add()
        {
            IBinarySearchTree<byte> bt = this.CreateInstance();

            bt.Add(10);

            Assert.True(bt.Contains(10));
        }

        [Fact]
        public void Test_Get_Throws_InvalidOperation_When_Empty()
        {
            IBinarySearchTree<byte> bt = this.CreateInstance();

            Assert.Throws<InvalidOperationException>(() => bt.Get(10));
        }

        [Fact]
        public void Test_Get_When_NotFound()
        {
            IBinarySearchTree<byte> bt = this.CreateFullBinaryTree();

            Assert.Null(bt.Get(255));
        }

        [Fact]
        public void Test_Contains_Throws_InvalidOperation_When_Empty()
        {
            IBinarySearchTree<byte> bt = this.CreateInstance();

            Assert.Throws<InvalidOperationException>(() => bt.Contains(10));
        }

        [Fact]
        public void Test_Contains()
        {
            IBinarySearchTree<byte> bt = this.CreateFullBinaryTree();

            Assert.True(bt.Contains(VALUES[^1]));
        }

        [Fact]
        public void Test_Contains_When_NotFound()
        {
            IBinarySearchTree<byte> bt = this.CreateFullBinaryTree();

            Assert.False(bt.Contains(255));
        }

        [Fact]
        public void Test_Delete_Throws_InvalidOperation_When_Empty()
        {
            IBinarySearchTree<byte> bt = this.CreateInstance();

            Assert.Throws<InvalidOperationException>(() => bt.Delete(10));
        }

        [Fact]
        public void Test_Delete_Throws_InvalidOperation_When_NotFound()
        {
            IBinarySearchTree<byte> bt = this.CreateFullBinaryTree();

            Assert.Throws<InvalidOperationException>(() => bt.Delete(100));
        }

        [Fact]
        public void Test_Delete()
        {
            IBinarySearchTree<byte> bt = this.CreateFullBinaryTree();

            foreach(byte value in VALUES)
            {
                bt.Delete(value);
            }

            Assert.True(bt.IsEmpty);
        }

        protected IBinarySearchTree<byte> CreateFullBinaryTree()
        {
            IBinarySearchTree<byte> bt = this.CreateInstance();

            foreach (byte value in VALUES)
            {
                bt.Add(value);
            }

            return bt;
        }
    }
}
