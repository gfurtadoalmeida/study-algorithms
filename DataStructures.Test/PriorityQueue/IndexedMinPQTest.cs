using System;
using DataStructures.PriorityQueue;
using Xunit;

namespace DataStructures.Test.PriorityQueue
{
    public sealed class IndexedMinPQTest
    {
        [Fact]
        public void Test_Empty_OnCreation()
        {
            IndexedMinPQ<Int32> pq = new IndexedMinPQ<Int32>(1);

            Assert.True(pq.IsEmpty);
            Assert.Equal(0, pq.Count);
        }

        [Fact]
        public void Test_Empty_WhenCleaning()
        {
            IndexedMinPQ<Int32> pq = new IndexedMinPQ<Int32>(1)
            {
                { 0, 2 }
            };
            pq.DeleteMin();

            Assert.True(pq.IsEmpty);
            Assert.Equal(0, pq.Count);
        }

        [Fact]
        public void Test_NotEmpty_WhenAdding()
        {
            IndexedMinPQ<Int32> pq = new IndexedMinPQ<Int32>(2)
            {
                { 0, 18 },
                { 1, 2 }
            };

            Assert.False(pq.IsEmpty);
            Assert.Equal(2, pq.Count);
        }

        [Fact]
        public void Test_AddDeleteMin_Order()
        {
            IndexedMinPQ<Int32> pq = new IndexedMinPQ<Int32>(10);

            for (int i = 1; i < 11; i++)
                pq.Add(i, i);

            for (int i = 1; i < 11; i++)
                Assert.Equal(i, pq.DeleteMin());
        }

        [Fact]
        public void Test_Add_MinItem()
        {
            IndexedMinPQ<Int32> pq = new IndexedMinPQ<Int32>(10);

            for (int i = 1; i < 11; i++)
                pq.Add(i, i);

            Assert.Equal(1, pq.MinItem());
        }

        [Fact]
        public void Test_Add_MinIndex()
        {
            IndexedMinPQ<Int32> pq = new IndexedMinPQ<Int32>(10);

            for (int i = 1; i < 11; i++)
                pq.Add(i, i);

            Assert.Equal(1, pq.MinIndex());
        }

        [Fact]
        public void Test_ItemAt()
        {
            IndexedMinPQ<Int32> pq = new IndexedMinPQ<Int32>(10);

            for (int i = 1; i < 11; i++)
                pq.Add(i, i);

            Assert.Equal(2, pq.ItemAt(2));
        }

        [Fact]
        public void Test_Delete()
        {
            IndexedMinPQ<Int32> pq = new IndexedMinPQ<Int32>(5);

            for (int i = 1; i < 6; i++)
                pq.Add(i, i);

            pq.Delete(4);

            Assert.False(pq.Contains(4));
        }

        [Fact]
        public void Test_Contains()
        {
            IndexedMinPQ<Int32> pq = new IndexedMinPQ<Int32>(5);

            for (int i = 1; i < 6; i++)
                pq.Add(i, i);

            Assert.True(pq.Contains(4));
        }

        [Fact]
        public void Test_IncreaseItem()
        {
            IndexedMinPQ<Int32> pq = new IndexedMinPQ<Int32>(5);

            for (int i = 1; i < 6; i++)
                pq.Add(i, i);

            pq.IncreaseItem(1, 50);

            Assert.Equal(50, pq.ItemAt(1));
        }

        [Fact]
        public void Test_DecreaseItem()
        {
            IndexedMinPQ<Int32> pq = new IndexedMinPQ<Int32>(5);

            for (int i = 1; i < 6; i++)
                pq.Add(i, i);

            pq.DecreaseItem(1, -1);

            Assert.Equal(-1, pq.ItemAt(1));
        }
    }
}