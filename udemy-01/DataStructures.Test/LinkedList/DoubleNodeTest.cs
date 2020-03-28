using DataStructures.LinkedList;
using Xunit;

namespace DataStructures.Test.LinkedList
{
    public class DoubleNodeTest
    {
        [Fact]
        public void Test_Constructor()
        {
            DoubleNode<byte> previousNode = new DoubleNode<byte>(default, DoubleNode<byte>.Empty, DoubleNode<byte>.Empty);
            DoubleNode<byte> nextNode = new DoubleNode<byte>(default, DoubleNode<byte>.Empty, DoubleNode<byte>.Empty);
            DoubleNode<byte> node = new DoubleNode<byte>(10, previousNode, nextNode);

            Assert.Equal(10, node.Value);
            Assert.Equal(node.Previous, previousNode);
            Assert.Equal(node.Next, nextNode);
        }
    }
}