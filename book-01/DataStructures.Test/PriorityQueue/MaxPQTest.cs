using System;
using DataStructures.PriorityQueue;
using Xunit;

namespace DataStructures.Test.PriorityQueue
{
    public sealed class MaxPQTest
    {
        [Fact]
        public void Test_Empty_OnCreation()
        {
            MaxPQ<Int32> pq = new MaxPQ<Int32>(1);

            Assert.True(pq.IsEmpty);
            Assert.Equal(0, pq.Count);
        }

        [Fact]
        public void Test_Empty_WhenCleaning()
        {
            MaxPQ<Int32> pq = new MaxPQ<Int32>(1);
            pq.Add(2);
            pq.DeleteMax();

            Assert.True(pq.IsEmpty);
            Assert.Equal(0, pq.Count);
        }

        [Fact]
        public void Test_NotEmpty_WhenAdding()
        {
            MaxPQ<Int32> pq = new MaxPQ<Int32>(2);
            pq.Add(18);
            pq.Add(2);

            Assert.False(pq.IsEmpty);
            Assert.Equal(2, pq.Count);
        }

        [Fact]
        public void Test_Add_DeleteMax_Order()
        {
            MaxPQ<Int32> pq = new MaxPQ<Int32>(10);

            for (int i = 1; i < 11; i++)
                pq.Add(i);

            for (int i = 10; i > 0; i--)
                Assert.Equal(pq.DeleteMax(), i);
        }

        [Fact]
        public void Test_Add_Max()
        {
            MaxPQ<Int32> pq = new MaxPQ<Int32>(10);

            for (int i = 1; i < 11; i++)
                pq.Add(i);

            Assert.Equal(10, pq.Max());
        }
    }
}