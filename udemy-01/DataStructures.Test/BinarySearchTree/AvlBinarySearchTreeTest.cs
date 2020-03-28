using DataStructures.BinarySearchTree;
using Xunit;
using BT = DataStructures.BinaryTree;

namespace DataStructures.Test.BinarySearchTree
{
    public class AvlBinarySearchTreeTest : BaseBinarySearchTreeTest
    {
        //       10
        //      /   \
        //     4     50
        //    / \   /  \
        //   3   5 30   65
        //        / \   / \ 
        //       20 40 60  70

        public override IBinarySearchTree<byte> CreateInstance()
        {
            return new AvlBinarySearchTree<byte>();
        }

        [Fact]
        public void Test_Add_Then_Verify_Properties()
        {
            //     10                    10
            //    /   \                 /   \
            //   4     50              4     50
            //  / \   /  \      =>    / \   /  \
            // 3   5 30   65         3   5 30   70
            //      / \   / \       /     / \   / \
            //     20 40 60  70    2     20 40 65  70
            IBinarySearchTree<byte> bt = this.CreateFullBinaryTree();

            bt.Add(2);

            BT.Node<byte> node = bt.Get(2)!;

            Assert.False(bt.IsEmpty);
            Assert.NotNull(node.Parent);
            Assert.NotNull(node.Parent!.Left);
            Assert.Null(node.Left);
            Assert.Null(node.Right);
            Assert.Equal(3, node.Parent!.Value);
        }

        [Fact]
        public void Test_Delete_Then_Verify_Properties()
        {
            //     10                  10
            //    /   \               /   \
            //   4     50            4     50
            //  / \   /  \      =>  / \   /  \
            // 3   5 30   65       3   5 30   65
            //      / \   / \           / \   / \ 
            //     20 40 60  70        20 40 60  70
            IBinarySearchTree<byte> bt = this.CreateFullBinaryTree();

            bt.Delete(60);
            bt.Delete(70);

            BT.Node<byte> node = bt.Get(65)!;

            Assert.False(bt.IsEmpty);
            Assert.NotNull(node.Parent);
            Assert.Equal(50, node.Parent!.Value);
            Assert.Null(node.Left!);
            Assert.Null(node.Right!);
        }

        [Fact]
        public void Test_Enumeration()
        {
            IBinarySearchTree<byte> bt = this.CreateFullBinaryTree();

            Assert.Equal(new byte[11] { 10, 4, 50, 3, 5, 30, 65, 20, 40, 60, 70 }, bt);
        }
    }
}
