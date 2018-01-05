using System;
using Algorithms.Structures;
using Xunit;

namespace Algorithms.Test.Structures
{
    public sealed class QueueTest
    {
        [Fact]
        public void Test_Empty_OnCreation()
        {
            Queue<Int32> queue = new Queue<Int32>();

            Assert.True(queue.IsEmpty);
            Assert.Equal(0, queue.Count);
        }

        [Fact]
        public void Test_Empty_WhenCleaning()
        {
            Queue<Int32> queue = new Queue<Int32>();
            queue.Enqueue(2);
            queue.Dequeue();

            Assert.True(queue.IsEmpty);
            Assert.Equal(0, queue.Count);
        }

        [Fact]
        public void Test_NotEmpty_WhenEnqueueing()
        {
            Queue<Int32> queue = new Queue<Int32>();
            queue.Enqueue(18);
            queue.Enqueue(2);

            Assert.False(queue.IsEmpty);
            Assert.Equal(2, queue.Count);
        }

        [Fact]
        public void Test_EnqueueDequeueOrder()
        {
            Queue<Int32> queue = new Queue<Int32>();
            queue.Enqueue(18);
            queue.Enqueue(2);
            queue.Enqueue(88);

            Assert.Equal(18, queue.Dequeue());
            Assert.Equal(2, queue.Dequeue());
            Assert.Equal(88, queue.Dequeue());
        }

        [Fact]
        public void Test_Enumeration()
        {
            Queue<Int32> queue = new Queue<Int32>();
            queue.Enqueue(18);
            queue.Enqueue(2);
            queue.Enqueue(88);

            AssertUtilities.Sequence(new Int32[3] { 18, 2, 88 }, queue);
        }
    }
}
