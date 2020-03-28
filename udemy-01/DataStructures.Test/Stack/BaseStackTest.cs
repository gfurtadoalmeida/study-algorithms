using System;
using System.Linq;
using DataStructures.Stack;
using Xunit;

namespace DataStructures.Test.Stack
{
    public abstract class BaseStackTest
    {
        private static readonly byte[] VALUES = new byte[5] { 1, 2, 3, 4, 5 };

        protected abstract IStack<byte> CreateInstance(int initialSize);

        [Fact]
        public void Test_Constructor()
        {
            IStack<byte> st = this.CreateInstance(1);

            Assert.True(st.IsEmpty);
        }

        [Fact]
        public void Test_Push_When_New()
        {
            IStack<byte> st = this.CreateInstance(2);

            st.Push(100);

            Assert.Equal(100, st.Peek(0));
            Assert.False(st.IsEmpty);
        }

        [Fact]
        public void Test_Push_When_Full()
        {
            IStack<byte> st = this.CreateFullStack();

            Assert.False(st.IsEmpty);
        }

        [Fact]
        public void Test_Pop_Throws_InvalidOperation_When_Empty()
        {
            IStack<byte> st = this.CreateInstance(1);

            Assert.Throws<InvalidOperationException>(() => st.Pop());
        }

        [Fact]
        public void Test_Pop()
        {
            IStack<byte> st = this.CreateFullStack();

            for (int i = VALUES.Length - 1; i >= 0; i--)
            {
                Assert.Equal(VALUES[i], st.Pop());
            }

            Assert.True(st.IsEmpty);
        }

        [Fact]
        public void Test_Peek()
        {
            IStack<byte> st = this.CreateFullStack();

            for (int i = 0; i < VALUES.Length; i++)
            {
                Assert.Equal(VALUES[^(i + 1)], st.Peek(i));
            }

            Assert.False(st.IsEmpty);
        }

        [Fact]
        public void Test_Enumeration()
        {
            IStack<byte> st = CreateFullStack();

            Assert.Equal(VALUES.Reverse(), st);
        }

        protected IStack<byte> CreateFullStack()
        {
            IStack<Byte> st = this.CreateInstance(VALUES.Length);

            foreach (byte value in VALUES)
            {
                st.Push(value);
            }

            return st;
        }
    }
}