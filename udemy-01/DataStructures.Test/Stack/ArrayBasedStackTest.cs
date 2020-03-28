using System;
using DataStructures.Stack;
using Xunit;

namespace DataStructures.Test.Stack
{
    public class ArrayBasedStackTest : BaseStackTest
    {
        protected override IStack<byte> CreateInstance(int initialSize)
        {
            return new ArrayBasedStack<byte>(initialSize);
        }

        [Fact]
        public void Test_Constructor_Throws_ArgumentOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => this.CreateInstance(-1));
        }

        [Fact]
        public void Test_Push_Throws_InvalidOperation_When_Full()
        {
            IStack<byte> st = this.CreateFullStack();

            Assert.Throws<InvalidOperationException>(() => st.Push(default));
        }

        [Fact]
        public void Test_Push_Is_Full()
        {
            ArrayBasedStack<byte> st = (ArrayBasedStack<byte>)this.CreateFullStack();

            Assert.True(st.IsFull);
        }

        [Fact]
        public void Test_Dequeue_Not_Full()
        {
            ArrayBasedStack<byte> st = (ArrayBasedStack<byte>)this.CreateFullStack();

            st.Pop();

            Assert.False(st.IsFull);
        }
    }
}
