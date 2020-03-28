using System;
using DataStructures.Trie;
using Xunit;

namespace DataStructures.Test.Trie
{
    public class NodeTest
    {
        [Fact]
        public void Test_Constructor()
        {
            Node node = new Node();

            Assert.False(node.IsEnd);
            Assert.Equal(0, node.Size);
        }

        [Fact]
        public void Test_Add_ThrowsInvalidOperation_When_Found()
        {
            Node node = new Node();

            node.Add('A');

            Assert.Throws<InvalidOperationException>(() => node.Add('A'));
        }

        [Fact]
        public void Test_Add()
        {
            Node node = new Node();

            Node childNode = node.Add('A');

            Assert.False(node.IsEnd);
            Assert.Equal(1, node.Size);
            Assert.NotNull(childNode);
            Assert.False(childNode.IsEnd);
            Assert.Equal(0, childNode.Size);
        }

        [Fact]
        public void Test_Get_ThrowsInvalidOperation_When_NotFound()
        {
            Node node = new Node();

            Assert.Throws<InvalidOperationException>(() => node.Get('A'));
        }

        [Fact]
        public void Test_Get()
        {
            Node node = new Node();

            node.Add('A');

            Node childNode = node.Get('A');

            Assert.False(node.IsEnd);
            Assert.Equal(1, node.Size);
            Assert.False(childNode.IsEnd);
            Assert.Equal(0, childNode.Size);
        }

        [Fact]
        public void Test_Exists_True()
        {
            Node node = new Node();

            node.Add('A');

            Assert.True(node.Exists('A'));
        }

        [Fact]
        public void Test_Exists_False()
        {
            Node node = new Node();

            node.Add('A');

            Assert.False(node.Exists('B'));
        }

        [Fact]
        public void Test_Delete_ThrowsInvalidOperation_When_NotFound()
        {
            Node node = new Node();

            Assert.Throws<InvalidOperationException>(() => node.Delete('A'));
        }

        [Fact]
        public void Test_Delete()
        {
            Node node = new Node();

            node.Add('A');
            node.Delete('A');

            Assert.Equal(0, node.Size);
        }
    }
}
