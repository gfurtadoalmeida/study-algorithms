using System;
using Algorithms.Structures;
using Xunit;

namespace Algorithms.Test.Structures
{
    public sealed class HeapTest
    {
        [Fact]
        public void Test_Empty_OnCreation()
        {
            Heap<Int32> heap = new Heap<Int32>(1);

            Assert.True(heap.IsEmpty);
            Assert.Equal(0, heap.Count);
        }

        [Fact]
        public void Test_Empty_WhenCleaning()
        {
            Heap<Int32> heap = new Heap<Int32>(1);
            heap.Insert(2);
            heap.RemoveTop();

            Assert.True(heap.IsEmpty);
            Assert.Equal(0, heap.Count);
        }

        [Fact]
        public void Test_NotEmpty_WhenInserting()
        {
            Heap<Int32> heap = new Heap<Int32>(2);
            heap.Insert(18);
            heap.Insert(2);

            Assert.False(heap.IsEmpty);
            Assert.Equal(2, heap.Count);
        }

        [Fact]
        public void Test_InsertRemoveTop_Max_Order()
        {
            Heap<Int32> heap = new Heap<Int32>(HeapType.Max, 10);

            for (int i = 1; i < 11; i++)
                heap.Insert(i);

            for (int i = 10; i > 0; i--)
                Assert.Equal(heap.RemoveTop(), i);
        }

        [Fact]
        public void Test_InsertRemoveTop_Min_Order()
        {
            Heap<Int32> heap = new Heap<Int32>(HeapType.Min, 10);

            for (int i = 1; i < 11; i++)
                heap.Insert(i);

            for (int i = 1; i < 11; i++)
                Assert.Equal(heap.RemoveTop(), i);
        }
        
        [Fact]
        public void Test_ArrayResize()
        {
            Heap<Int32> heap = new Heap<Int32>(10);

            for (int i = 1; i < 31; i++)
                heap.Insert(i);

            for (int i = 30; i > 0; i--)
                Assert.Equal(heap.RemoveTop(), i);

            for (int i = 1; i < 31; i++)
                heap.Insert(i);

            for (int i = 30; i > 0; i--)
                Assert.Equal(heap.RemoveTop(), i);
        }
    }
}