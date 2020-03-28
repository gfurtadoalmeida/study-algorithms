using DataStructures.LinkedList;
using Xunit;

namespace DataStructures.Test.LinkedList
{
    public class NodeTest
    {
        [Fact]
        public void Test_Constructor()
        {
            Node<byte> node = new Node<byte>(10, Node<byte>.Empty);

            Assert.Equal(10, node.Value);
            Assert.Equal(node.Next, Node<byte>.Empty);
        }
    }
}