using System;
using DataStructures.BinaryTree;
using Xunit;

namespace DataStructures.Test.BinaryTree
{
    public class ArrayBasedBinaryTreeTest : BaseBinaryTreeTest
    {
        protected override IBinaryTree<byte> CreateInstance(int initialSize)
        {
            return new ArrayBasedBinarayTree<byte>(initialSize);
        }

        [Fact]
        public void Test_Constructor_Throws_ArgumentOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => this.CreateInstance(-1));
        }

        [Fact]
        public void Test_Add_Throws_InvalidOperation_When_Full()
        {
            IBinaryTree<byte> bt = this.CreateFullBinaryTree();

            Assert.Throws<InvalidOperationException>(() => bt.Add(default));
        }

        [Fact]
        public void Test_Add_Is_Full()
        {
            ArrayBasedBinarayTree<byte> bt = (ArrayBasedBinarayTree<byte>)this.CreateFullBinaryTree();

            Assert.True(bt.IsFull);
        }

        [Fact]
        public void Test_Delete_Not_Full()
        {
            ArrayBasedBinarayTree<byte> bt = (ArrayBasedBinarayTree<byte>)this.CreateFullBinaryTree();

            bt.Delete(1);

            Assert.False(bt.IsFull);
        }
    }
}
