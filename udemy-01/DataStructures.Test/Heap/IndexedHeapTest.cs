using System;
using DataStructures.Heap;
using Xunit;

namespace DataStructures.Test.Heap
{
    public class IndexedHeapTest : BaseBinaryHeapTest
    {
        protected override IHeap<byte> CreateInstance(HeapMode mode, int initialSize)
        {
            return new IndexedHeap<byte>(mode, initialSize);
        }

        [Fact]
        public void Test_GetIndex_Min_Throws_Argument()
        {
            IndexedHeap<byte> heap = (IndexedHeap<byte>)this.CreateInstance(HeapMode.MinHeap, 1);

            Assert.Throws<ArgumentException>(() => heap.GetIndex(255));
        }

        [Fact]
        public void Test_GetIndex_Max_Throws_Argument()
        {
            IndexedHeap<byte> heap = (IndexedHeap<byte>)this.CreateInstance(HeapMode.MaxHeap, 1);

            Assert.Throws<ArgumentException>(() => heap.GetIndex(255));
        }

        [Fact]
        public void Test_GetIndex_Min()
        {
            IndexedHeap<byte> heap = (IndexedHeap<byte>)this.CreateInstance(HeapMode.MinHeap, 5);

            heap.Add(10);
            heap.Add(20);
            heap.Add(30);
            heap.Add(40);
            heap.Add(50);

            Assert.Equal(2, heap.GetIndex(20));
        }

        [Fact]
        public void Test_GetIndex_Max()
        {
            IndexedHeap<byte> heap = (IndexedHeap<byte>)this.CreateInstance(HeapMode.MaxHeap, 5);

            heap.Add(10);
            heap.Add(20);
            heap.Add(30);
            heap.Add(40);
            heap.Add(50);

            Assert.Equal(1, heap.GetIndex(50));
        }

        [Fact]
        public void Test_Update_Min_Throws_Argument()
        {
            IndexedHeap<string> heap = new IndexedHeap<string>(HeapMode.MinHeap, 1);

            heap.Add("test");

            Assert.Throws<ArgumentException>(() => heap.Update(1, "not test"));
        }

        [Fact]
        public void Test_Update_Max_Throws_Argument()
        {
            IndexedHeap<string> heap = new IndexedHeap<string>(HeapMode.MaxHeap, 1);

            heap.Add("test");

            Assert.Throws<ArgumentException>(() => heap.Update(1, "not test"));
        }

        [Fact]
        public void Test_Update_Min_Throws_ArgumentOutOfRange()
        {
            IndexedHeap<byte> heap = (IndexedHeap<byte>)this.CreateInstance(HeapMode.MinHeap, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => heap.Update(100, default));
        }

        [Fact]
        public void Test_Update_Max_Throws_ArgumentOutOfRange()
        {
            IndexedHeap<byte> heap = (IndexedHeap<byte>)this.CreateInstance(HeapMode.MaxHeap, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => heap.Update(100, default));
        }

        [Fact]
        public void Test_Update_Min()
        {
            IndexedHeap<byte> heap = (IndexedHeap<byte>)this.CreateInstance(HeapMode.MinHeap, 5);

            heap.Add(10);
            heap.Add(20);
            heap.Add(30);
            heap.Add(40);
            heap.Add(50);

            int index = heap.GetIndex(40);

            heap.Update(index, 5);

            Assert.Equal(1, heap.GetIndex(5));
            Assert.Equal(2, heap.GetIndex(10));
        }

        [Fact]
        public void Test_Update_Max()
        {
            IndexedHeap<byte> heap = (IndexedHeap<byte>)this.CreateInstance(HeapMode.MaxHeap, 5);

            heap.Add(10);
            heap.Add(20);
            heap.Add(30);
            heap.Add(40);
            heap.Add(50);

            int index = heap.GetIndex(40);

            heap.Update(index, 100);

            Assert.Equal(1, heap.GetIndex(100));
            Assert.Equal(2, heap.GetIndex(50));
        }

        [Fact]
        public void Test_ExtractTop_Min_Remove_From_Index()
        {
            IndexedHeap<byte> heap = (IndexedHeap<byte>)this.CreateFullIHeap(HeapMode.MinHeap);

            byte value = heap.ExtractTop();

            Assert.Throws<ArgumentException>(() => heap.GetIndex(value));
        }

        [Fact]
        public void Test_ExtractTop_Max_Remove_From_Index()
        {
            IndexedHeap<byte> heap = (IndexedHeap<byte>)this.CreateFullIHeap(HeapMode.MaxHeap);

            byte value = heap.ExtractTop();

            Assert.Throws<ArgumentException>(() => heap.GetIndex(value));
        }
    }
}
