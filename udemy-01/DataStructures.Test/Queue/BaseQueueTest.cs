using System;
using DataStructures.Queue;
using Xunit;

namespace DataStructures.Test.Queue
{
    public abstract class BaseQueueTest
    {
        private static readonly byte[] VALUES = new byte[5] { 1, 2, 3, 4, 5 };

        protected abstract IQueue<byte> CreateInstance(int initialSize);

        [Fact]
        public void Test_Constructor()
        {
            IQueue<byte> qe = this.CreateInstance(1);

            Assert.True(qe.IsEmpty);
        }

        [Fact]
        public void Test_Enqueue_When_New()
        {
            IQueue<byte> qe = this.CreateInstance(2);

            qe.Enqueue(100);

            Assert.Equal(100, qe.Peek(0));
            Assert.False(qe.IsEmpty);
        }

        [Fact]
        public void Test_Enqueue_When_Full()
        {
            IQueue<byte> qe = this.CreateFullQueue();

            Assert.False(qe.IsEmpty);
        }

        [Fact]
        public void Test_Dequeue_Throws_InvalidOperation_When_Empty()
        {
            IQueue<byte> qe = this.CreateInstance(1);

            Assert.Throws<InvalidOperationException>(() => qe.Dequeue());
        }

        [Fact]
        public void Test_Dequeue()
        {
            IQueue<byte> qe = this.CreateFullQueue();

            for (int i = 0; i < VALUES.Length; i++)
            {
                Assert.Equal(VALUES[i], qe.Dequeue());
            }

            Assert.True(qe.IsEmpty);
        }

        [Fact]
        public void Test_Peek()
        {
            IQueue<byte> qe = this.CreateFullQueue();

            for (int i = 0; i < VALUES.Length; i++)
            {
                Assert.Equal(VALUES[i], qe.Peek(i));
            }

            Assert.False(qe.IsEmpty);
        }

        [Fact]
        public void Test_Peek_Varios_Operations()
        {
            IQueue<byte> qe = this.CreateFullQueue();
            qe.Dequeue(); // 2, 3, 4, 5
            qe.Dequeue(); // 3, 4, 5
            qe.Enqueue(6); // 3, 4, 5, 6
            qe.Enqueue(7); // 3, 4, 5, 6, 7

            for (int i = 0; i < VALUES.Length - 2 + 2; i++)
            {
                Assert.Equal(3 + i, qe.Peek(i));
            }

            Assert.False(qe.IsEmpty);
        }

        [Fact]
        public void Test_Enumeration()
        {
            IQueue<byte> qe = CreateFullQueue();

            Assert.Equal(VALUES, qe);
        }

        protected IQueue<byte> CreateFullQueue()
        {
            IQueue<Byte> qe = this.CreateInstance(VALUES.Length);

            foreach (byte value in VALUES)
            {
                qe.Enqueue(value);
            }

            return qe;
        }
    }
}