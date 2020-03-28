using System;
using Xunit;

namespace DataStructures.Test
{
    public sealed class StackTest
    {
        [Fact]
        public void Test_Empty_OnCreation()
        {
            Stack<Int32> stack = new Stack<Int32>();

            Assert.True(stack.IsEmpty);
            Assert.Equal(0, stack.Count);
        }

        [Fact]
        public void Test_Empty_WhenCleaning()
        {
            Stack<Int32> stack = new Stack<Int32>();
            stack.Push(2);
            stack.Pop();

            Assert.True(stack.IsEmpty);
            Assert.Equal(0, stack.Count);
        }

        [Fact]
        public void Test_NotEmpty_WhenPushing()
        {
            Stack<Int32> stack = new Stack<Int32>();
            stack.Push(18);
            stack.Push(2);

            Assert.False(stack.IsEmpty);
            Assert.Equal(2, stack.Count);
        }

        [Fact]
        public void Test_PushPopOrder()
        {
            Stack<Int32> stack = new Stack<Int32>();
            stack.Push(18);
            stack.Push(2);
            stack.Push(88);

            Assert.Equal(88, stack.Pop());
            Assert.Equal(2, stack.Pop());
            Assert.Equal(18, stack.Pop());
        }

        [Fact]
        public void Test_Enumeration()
        {
            Stack<Int32> stack = new Stack<Int32>();
            stack.Push(18);
            stack.Push(2);
            stack.Push(88);

            AssertUtilities.Sequence(new Int32[3] { 88, 2, 18 }, stack);
        }
    }
}
