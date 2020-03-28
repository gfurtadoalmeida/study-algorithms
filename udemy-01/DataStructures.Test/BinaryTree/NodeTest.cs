using DataStructures.BinaryTree;
using Xunit;

namespace DataStructures.Test.BinaryTree
{
    public class NodeTest
    {
        [Fact]
        public void Test_Constructor()
        {
            Node<byte> node = new Node<byte>(10);

            Assert.False(node.HasAllChildren);
            Assert.False(node.HasChildren);
            Assert.False(node.IsLeft);
            Assert.False(node.IsRight);
            Assert.Null(node.Parent);
            Assert.Null(node.Right);
            Assert.Null(node.Left);
            Assert.Equal(10, node.Value);
        }

        [Fact]
        public void Test_Set_Left()
        {
            Node<byte> parent = new Node<byte>(5);
            Node<byte> node = new Node<byte>(10);

            parent.Left = node;

            Assert.Null(parent.Right);
            Assert.True(parent.HasChildren);
            Assert.False(parent.HasAllChildren);
            Assert.Equal(node, parent.Left);
            Assert.True(node.IsLeft);
            Assert.False(node.IsRight);
            Assert.Equal(parent, node.Parent);
        }

        [Fact]
        public void Test_Set_Left_Null()
        {
            Node<byte> parent = new Node<byte>(5);
            Node<byte> node = new Node<byte>(10);

            parent.Left = null;

            Assert.Null(parent.Left);
            Assert.Null(parent.Right);
            Assert.False(parent.HasChildren);
            Assert.False(parent.HasAllChildren);
            Assert.False(node.IsLeft);
            Assert.False(node.IsRight);
            Assert.Null(node.Parent);
        }

        [Fact]
        public void Test_Set_Right()
        {
            Node<byte> parent = new Node<byte>(5);
            Node<byte> node = new Node<byte>(10);

            parent.Right = node;

            Assert.Null(parent.Left);
            Assert.True(parent.HasChildren);
            Assert.False(parent.HasAllChildren);
            Assert.Equal(node, parent.Right);
            Assert.False(node.IsLeft);
            Assert.True(node.IsRight);
            Assert.Equal(parent, node.Parent);
        }

        [Fact]
        public void Test_Set_Right_Null()
        {
            Node<byte> parent = new Node<byte>(5);
            Node<byte> node = new Node<byte>(10);

            parent.Right = null;

            Assert.Null(parent.Left);
            Assert.Null(parent.Right);
            Assert.False(parent.HasChildren);
            Assert.False(parent.HasAllChildren);
            Assert.False(node.IsLeft);
            Assert.False(node.IsRight);
            Assert.Null(node.Parent);
        }

        [Fact]
        public void Test_Detach()
        {
            Node<byte> parent = new Node<byte>(5);
            Node<byte> node = new Node<byte>(10);

            parent.Right = node;

            node.Detach();

            Assert.Null(parent.Left);
            Assert.Null(parent.Right);
            Assert.False(parent.HasChildren);
            Assert.False(parent.HasAllChildren);
            Assert.False(node.IsLeft);
            Assert.False(node.IsRight);
            Assert.Null(node.Parent);
        }

        [Fact]
        public void Test_Transfer_Left()
        {
            Node<byte> parent = new Node<byte>(5);
            Node<byte> newParent = new Node<byte>(6);
            Node<byte> node = new Node<byte>(10);

            parent.Left = node;
            newParent.Left = node;

            Assert.Null(parent.Left);
            Assert.Null(parent.Right);
            Assert.False(parent.HasChildren);
            Assert.False(parent.HasAllChildren);
            Assert.Null(newParent.Right);
            Assert.True(newParent.HasChildren);
            Assert.False(newParent.HasAllChildren);
            Assert.Equal(node, newParent.Left);
            Assert.True(node.IsLeft);
            Assert.False(node.IsRight);
            Assert.Equal(newParent, node.Parent);
        }

        [Fact]
        public void Test_Transfer_Right()
        {
            Node<byte> parent = new Node<byte>(5);
            Node<byte> newParent = new Node<byte>(6);
            Node<byte> node = new Node<byte>(10);

            parent.Right = node;
            newParent.Right = node;

            Assert.Null(parent.Left);
            Assert.Null(parent.Right);
            Assert.False(parent.HasChildren);
            Assert.False(parent.HasAllChildren);
            Assert.Null(newParent.Left);
            Assert.True(newParent.HasChildren);
            Assert.False(newParent.HasAllChildren);
            Assert.Equal(node, newParent.Right);
            Assert.False(node.IsLeft);
            Assert.True(node.IsRight);
            Assert.Equal(newParent, node.Parent);
        }

        [Fact]
        public void Test_Orphan_Left()
        {
            Node<byte> parent = new Node<byte>(5);
            Node<byte> orphan = new Node<byte>(10);
            Node<byte> node = new Node<byte>(15);

            parent.Left = orphan;
            parent.Left = node;

            Assert.Null(orphan.Parent);
        }

        [Fact]
        public void Test_Orphan_Right()
        {
            Node<byte> parent = new Node<byte>(5);
            Node<byte> orphan = new Node<byte>(10);
            Node<byte> node = new Node<byte>(15);

            parent.Right = orphan;
            parent.Right = node;

            Assert.Null(orphan.Parent);
        }
    }
}
