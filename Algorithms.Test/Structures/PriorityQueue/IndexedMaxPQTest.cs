using System;
using Algorithms.Structures.PriorityQueue;
using Xunit;

namespace Algorithms.Test.Structures.PriorityQueue
{
    public sealed class IndexedMaxPQTest
    {
        [Fact]
        public void Test_Empty_OnCreation()
        {
            IndexedMaxPQ<Int32> pq = new IndexedMaxPQ<Int32>(1);

            Assert.True(pq.IsEmpty);
            Assert.Equal(0, pq.Count);
        }

        [Fact]
        public void Test_Empty_WhenCleaning()
        {
            IndexedMaxPQ<Int32> pq = new IndexedMaxPQ<Int32>(1);
            pq.Add(0, 2);
            pq.DeleteMax();

            Assert.True(pq.IsEmpty);
            Assert.Equal(0, pq.Count);
        }

        [Fact]
        public void Test_NotEmpty_WhenAdding()
        {
            IndexedMaxPQ<Int32> pq = new IndexedMaxPQ<Int32>(2);
            pq.Add(0, 18);
            pq.Add(1, 2);

            Assert.False(pq.IsEmpty);
            Assert.Equal(2, pq.Count);
        }

        [Fact]
        public void Test_AddDeleteMax_Order()
        {
            IndexedMaxPQ<Int32> pq = new IndexedMaxPQ<Int32>(10);

            for (int i = 1; i < 11; i++)
                pq.Add(i, i);

            for (int i = 10; i > 0; i--)
                Assert.Equal(i, pq.DeleteMax());
        }

        [Fact]
        public void Test_Add_MaxItem()
        {
            IndexedMaxPQ<Int32> pq = new IndexedMaxPQ<Int32>(10);

            for (int i = 1; i < 11; i++)
                pq.Add(i, i);

            Assert.Equal(10, pq.MaxItem());
        }

        [Fact]
        public void Test_Add_MaxIndex()
        {
            IndexedMaxPQ<Int32> pq = new IndexedMaxPQ<Int32>(10);

            for (int i = 1; i < 11; i++)
                pq.Add(i, i);

            Assert.Equal(10, pq.MaxIndex());
        }

        [Fact]
        public void Test_ItemAt()
        {
            IndexedMaxPQ<Int32> pq = new IndexedMaxPQ<Int32>(10);

            for (int i = 1; i < 11; i++)
                pq.Add(i, i);

            Assert.Equal(2, pq.ItemAt(2));
        }

        [Fact]
        public void Test_Delete()
        {
            IndexedMaxPQ<Int32> pq = new IndexedMaxPQ<Int32>(5);

            for (int i = 1; i < 6; i++)
                pq.Add(i, i);

            pq.Delete(4);

            Assert.False(pq.Contains(4));
        }

        [Fact]
        public void Test_Contains()
        {
            IndexedMaxPQ<Int32> pq = new IndexedMaxPQ<Int32>(5);

            for (int i = 1; i < 6; i++)
                pq.Add(i, i);

            Assert.True(pq.Contains(4));
        }

        [Fact]
        public void Test_IncreaseItem()
        {
            IndexedMaxPQ<Int32> pq = new IndexedMaxPQ<Int32>(5);

            for (int i = 1; i < 6; i++)
                pq.Add(i, i);

            pq.IncreaseItem(1, 50);

            Assert.Equal(50, pq.ItemAt(1));
        }

        [Fact]
        public void Test_DecreaseItem()
        {
            IndexedMaxPQ<Int32> pq = new IndexedMaxPQ<Int32>(5);

            for (int i = 1; i < 6; i++)
                pq.Add(i, i);

            pq.DecreaseItem(1, -1);

            Assert.Equal(-1, pq.ItemAt(1));
        }
    }
}