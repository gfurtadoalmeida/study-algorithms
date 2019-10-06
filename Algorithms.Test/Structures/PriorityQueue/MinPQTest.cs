using System;
using Algorithms.Structures.PriorityQueue;
using Xunit;

namespace Algorithms.Test.Structures.PriorityQueue
{
    public sealed class MinPQTest
    {
        [Fact]
        public void Test_Empty_OnCreation()
        {
            MinPQ<Int32> pq = new MinPQ<Int32>(1);

            Assert.True(pq.IsEmpty);
            Assert.Equal(0, pq.Count);
        }

        [Fact]
        public void Test_Empty_WhenCleaning()
        {
            MinPQ<Int32> pq = new MinPQ<Int32>(1);
            pq.Add(2);
            pq.DeleteMin();

            Assert.True(pq.IsEmpty);
            Assert.Equal(0, pq.Count);
        }

        [Fact]
        public void Test_NotEmpty_WhenAdding()
        {
            MinPQ<Int32> pq = new MinPQ<Int32>(2);
            pq.Add(18);
            pq.Add(2);

            Assert.False(pq.IsEmpty);
            Assert.Equal(2, pq.Count);
        }

        [Fact]
        public void Test_AddDeleteMin_Order()
        {
            MinPQ<Int32> pq = new MinPQ<Int32>(10);

            for (int i = 1; i < 11; i++)
                pq.Add(i);

            for (int i = 1; i < 11; i++)
                Assert.Equal(pq.DeleteMin(), i);
        }

        [Fact]
        public void Test_Add_Min()
        {
            MinPQ<Int32> pq = new MinPQ<Int32>(10);

            for (int i = 1; i < 11; i++)
                pq.Add(i);

            Assert.Equal(1, pq.Min());
        }
    }
}