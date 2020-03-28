using DataStructures.BinarySearchTree;
using Xunit;
using BT = DataStructures.BinaryTree;

namespace DataStructures.Test.BinarySearchTree
{
    public class BinarySearchTreeTest : BaseBinarySearchTreeTest
    {
        //         30
        //        /  \
        //       20   40
        //      /      \
        //     10       50
        //    /          \
        //   5            60
        //  /              \
        // 3               70
        //  \              /
        //   4            65
        public override IBinarySearchTree<byte> CreateInstance()
        {
            return new BinarySearchTree<byte>();
        }

        [Fact]
        public void Test_Get()
        {
            IBinarySearchTree<byte> bt = this.CreateFullBinaryTree();
            BT.Node<byte> node = bt.Get(3)!;

            Assert.Equal(3, node.Value);
            Assert.NotNull(node.Parent);
            Assert.Null(node.Left);
            Assert.NotNull(node.Right);
            Assert.Equal(4, node.Right!.Value);
        }

        [Fact]
        public void Test_Add_Then_Verify_Properties()
        {
            //         30
            //        /  \
            //       20   40
            //      /      \
            //     10       50
            //    /        / \
            //   5      ->45  60
            //  /              \
            // 3               70
            //  \              /
            //   4            65
            IBinarySearchTree<byte> bt = this.CreateFullBinaryTree();

            bt.Add(45);

            BT.Node<byte>? node = bt.Get(45);

            Assert.False(bt.IsEmpty);
            Assert.NotNull(node);
            Assert.NotNull(node!.Parent);
            Assert.NotNull(node!.Parent!.Right);
            Assert.Equal(50, node.Parent!.Value);
            Assert.Null(node.Left);
            Assert.Null(node.Right);
        }

        [Fact]
        public void Test_Delete_Then_Verify_Properties()
        {
            //       ->30                    40
            //        /  \                  /  \
            //       20   40               20   50
            //      /      \              /      \
            //     10       50    =>     10       60
            //    /          \          /          \
            //   5            60       5            70
            //  /              \      /             /
            // 3               70    3             65
            //  \              /      \
            //   4            65       4
            IBinarySearchTree<byte> bt = this.CreateFullBinaryTree();

            bt.Delete(30);

            BT.Node<byte> node = bt.Get(40)!;

            Assert.Null(node.Parent);
            Assert.Equal(20, node.Left!.Value);
            Assert.Equal(50, node.Right!.Value);
        }

        [Fact]
        public void Test_Enumeration()
        {
            IBinarySearchTree<byte> bt = this.CreateFullBinaryTree();

            Assert.Equal(new byte[11] { 30, 20, 40, 10, 50, 5, 60, 3, 70, 4, 65 }, bt);
        }
    }
}
