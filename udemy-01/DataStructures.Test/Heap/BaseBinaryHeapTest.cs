using System;
using DataStructures.Heap;
using Xunit;

namespace DataStructures.Test.Heap
{
    public abstract class BaseBinaryHeapTest
    {
        protected static readonly byte[] VALUES = new byte[9] { 3, 5, 8, 17, 19, 36, 40, 25, 100 };

        protected abstract IHeap<byte> CreateInstance(HeapMode mode, int initialSize);

        [Fact]
        public void Test_Constructor_Min_Throws_ArgumentOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => this.CreateInstance(HeapMode.MinHeap, 0));
        }

        [Fact]
        public void Test_Constructor_Max_Throws_ArgumentOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => this.CreateInstance(HeapMode.MaxHeap, 0));
        }

        [Fact]
        public void Test_Constructor_Min()
        {
            IHeap<byte> heap = this.CreateInstance(HeapMode.MinHeap, 1);

            Assert.True(heap.IsEmpty);
        }

        [Fact]
        public void Test_Constructor_Max()
        {
            IHeap<byte> heap = this.CreateInstance(HeapMode.MaxHeap, 1);

            Assert.True(heap.IsEmpty);
        }

        [Fact]
        public void Test_Add_Min_Throws_InvalidOperation_When_Full()
        {
            IHeap<byte> heap = this.CreateFullIHeap(HeapMode.MinHeap);

            Assert.Throws<InvalidOperationException>(() => heap.Add(default));
        }

        [Fact]
        public void Test_Add_Max_Throws_InvalidOperation_When_Full()
        {
            IHeap<byte> heap = this.CreateFullIHeap(HeapMode.MaxHeap);

            Assert.Throws<InvalidOperationException>(() => heap.Add(default));
        }

        [Fact]
        public void Test_Add_Min_When_New()
        {
            IHeap<byte> heap = this.CreateInstance(HeapMode.MinHeap, 1);

            heap.Add(100);

            Assert.Equal(100, heap.PeekTop());
            Assert.False(heap.IsEmpty);
        }

        [Fact]
        public void Test_Add_Max_When_New()
        {
            IHeap<byte> heap = this.CreateInstance(HeapMode.MaxHeap, 1);

            heap.Add(100);

            Assert.Equal(100, heap.PeekTop());
            Assert.False(heap.IsEmpty);
        }

        [Fact]
        public void Test_Add_Min()
        {
            IHeap<byte> heap = this.CreateFullIHeap(HeapMode.MinHeap);

            heap.ExtractTop();
            heap.Add(1);

            Assert.Equal(1, heap.ExtractTop());
        }

        [Fact]
        public void Test_Add_Max()
        {
            IHeap<byte> heap = this.CreateFullIHeap(HeapMode.MaxHeap);

            heap.ExtractTop();
            heap.Add(200);

            Assert.Equal(200, heap.ExtractTop());
        }

        [Fact]
        public void Test_Add_Min_Is_Full()
        {
            IHeap<byte> heap = this.CreateFullIHeap(HeapMode.MinHeap);

            Assert.True(heap.IsFull);
        }

        [Fact]
        public void Test_Add_Max_Is_Full()
        {
            IHeap<byte> heap = this.CreateFullIHeap(HeapMode.MaxHeap);

            Assert.True(heap.IsFull);
        }

        [Fact]
        public void Test_ExtractTop_Min_Throws_InvalidOperation_When_Empty()
        {
            IHeap<byte> heap = this.CreateInstance(HeapMode.MinHeap, 1);

            Assert.Throws<InvalidOperationException>(() => heap.ExtractTop());
        }

        [Fact]
        public void Test_ExtractTop_Max_Throws_InvalidOperation_When_Empty()
        {
            IHeap<byte> heap = this.CreateInstance(HeapMode.MaxHeap, 1);

            Assert.Throws<InvalidOperationException>(() => heap.ExtractTop());
        }

        [Fact]
        public void Test_ExtractTop_Min()
        {
            IHeap<byte> heap = this.CreateFullIHeap(HeapMode.MinHeap);

            Assert.Equal(VALUES[0], heap.ExtractTop());
        }

        [Fact]
        public void Test_ExtractTop_Max()
        {
            IHeap<byte> heap = this.CreateFullIHeap(HeapMode.MaxHeap);

            Assert.Equal(VALUES[^1], heap.ExtractTop());
        }

        [Fact]
        public void Test_ExtractTop_Min_Not_Full()
        {
            IHeap<byte> heap = this.CreateFullIHeap(HeapMode.MinHeap);

            heap.ExtractTop();

            Assert.False(heap.IsFull);
        }

        [Fact]
        public void Test_ExtractTop_Max_Not_Full()
        {
            IHeap<byte> heap = this.CreateFullIHeap(HeapMode.MaxHeap);

            heap.ExtractTop();

            Assert.False(heap.IsFull);
        }

        [Fact]
        public void Test_Delete_All_Values_Min()
        {
            IHeap<byte> heap = this.CreateFullIHeap(HeapMode.MinHeap);

            for (int i = 0; i < VALUES.Length; i++)
            {
                heap.ExtractTop();
            }

            Assert.True(heap.IsEmpty);
        }

        [Fact]
        public void Test_Delete_All_Values_Max()
        {
            IHeap<byte> heap = this.CreateFullIHeap(HeapMode.MaxHeap);

            for (int i = 0; i < VALUES.Length; i++)
            {
                heap.ExtractTop();
            }

            Assert.True(heap.IsEmpty);
        }

        protected IHeap<byte> CreateFullIHeap(HeapMode mode)
        {
            IHeap<byte> heap = this.CreateInstance(mode, VALUES.Length);

            foreach (byte value in VALUES)
            {
                heap.Add(value);
            }

            return heap;
        }
    }
}
