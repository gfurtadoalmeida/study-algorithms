using System;
using DataStructures.BinaryTree;
using Xunit;

namespace DataStructures.Test.BinaryTree
{
    public class LinkedListBasedBinaryTreeTest : BaseBinaryTreeTest
    {
        protected override IBinaryTree<byte> CreateInstance(int initialSize)
        {
            return new LinkedListBasedBinaryTree<byte>();
        }

        [Fact]
        public void Test_Add_Then_Verify_Properties()
        {
            LinkedListBasedBinaryTree<byte> bt = (LinkedListBasedBinaryTree<byte>)this.CreateFullBinaryTree();

            bt.Add(10);

            Node<byte>? node = bt.Get(10);

            Assert.False(bt.IsEmpty);
            Assert.NotNull(node);
            Assert.NotNull(node!.Parent);
            Assert.Equal(5, node.Parent!.Value);
            Assert.Null(node.Left);
            Assert.Null(node.Right);
        }

        [Fact]
        public void Test_Get_Throws_InvalidOperation_When_Empty()
        {
            LinkedListBasedBinaryTree<byte> bt = (LinkedListBasedBinaryTree<byte>)this.CreateInstance(0);

            Assert.Throws<InvalidOperationException>(() => bt.Get(10));
        }

        [Fact]
        public void Test_Get()
        {
            LinkedListBasedBinaryTree<byte> bt = (LinkedListBasedBinaryTree<byte>)this.CreateFullBinaryTree();

            Node<byte>? node = bt.Get(4);

            Assert.NotNull(node);
            Assert.Equal(4, node!.Value);
            Assert.NotNull(node.Parent);
            Assert.NotNull(node.Left);
            Assert.NotNull(node.Right);
        }

        [Fact]
        public void Test_Get_When_NotFound()
        {
            LinkedListBasedBinaryTree<byte> bt = (LinkedListBasedBinaryTree<byte>)this.CreateFullBinaryTree();

            Assert.Null(bt.Get(255));
        }

        [Fact]
        public void Test_Delete_Then_Verify_Properties()
        {
            LinkedListBasedBinaryTree<byte> bt = (LinkedListBasedBinaryTree<byte>)this.CreateFullBinaryTree();

            //        1
            //      /   \
            //  -> 2     3
            //    / \   / \
            //   4   5 6   7
            //  / \
            // 8   9 <-

            bt.Delete(2);

            Node<byte>? node = bt.Get(9);

            Assert.Equal(1, node!.Parent!.Value);
            Assert.Equal(4, node!.Left!.Value);
            Assert.Equal(5, node!.Right!.Value);
        }
    }
}
