using System;
using DataStructures.Queue;
using Xunit;

namespace DataStructures.Test.Queue
{
    public class ArrayBasedQueueTest : BaseQueueTest
    {
        protected override IQueue<byte> CreateInstance(int initialSize)
        {
            return new ArrayBasedQueue<byte>(initialSize);
        }

        [Fact]
        public void Test_Constructor_Throws_ArgumentOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => this.CreateInstance(-1));
        }

        [Fact]
        public void Test_Enqueue_Throws_InvalidOperation_When_Full()
        {
            IQueue<byte> qe = this.CreateFullQueue();

            Assert.Throws<InvalidOperationException>(() => qe.Enqueue(default));
        }

        [Fact]
        public void Test_Enqueue_Is_Full()
        {
            ArrayBasedQueue<byte> qe = (ArrayBasedQueue<byte>)this.CreateFullQueue();

            Assert.True(qe.IsFull);
        }

        [Fact]
        public void Test_Dequeue_Not_Full()
        {
            ArrayBasedQueue<byte> qe = (ArrayBasedQueue<byte>)this.CreateFullQueue();

            qe.Dequeue();

            Assert.False(qe.IsFull);
        }
    }
}
