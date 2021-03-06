using System;
using DataStructures.LinkedList;
using Xunit;

namespace DataStructures.Test.LinkedList
{
    public class SingleLinkedListTest
    {
        private static readonly byte[] VALUES = new byte[5] { 5, 10, 15, 20, 25 };

        [Fact]
        public void Test_Constructor()
        {
            SingleLinkedList<byte> ll = new SingleLinkedList<byte>();

            Assert.True(ll.IsEmpty);
            Assert.Equal(0, ll.Count);
        }

        [Fact]
        public void Test_InsertAt_Throws_ArgumentOutOfRange()
        {
            SingleLinkedList<byte> ll = new SingleLinkedList<byte>();

            Assert.Throws<ArgumentOutOfRangeException>(() => ll.InsertAt(-1, default));
            Assert.Throws<ArgumentOutOfRangeException>(() => ll.InsertAt(100, default));
        }

        [Fact]
        public void Test_InsertAt()
        {
            SingleLinkedList<byte> ll = CreateSingleLinkedList();

            Assert.False(ll.IsEmpty);
            Assert.Equal(VALUES.Length, ll.Count);
            Assert.NotNull(ll.Head);
            Assert.NotNull(ll.Tail);
            Assert.Equal(VALUES[0], ll.Head!.Value);
            Assert.Equal(VALUES[^1], ll.Tail!.Value);
        }

        [Fact]
        public void Test_InsertAt_Start()
        {
            SingleLinkedList<byte> ll = CreateSingleLinkedList();

            ll.InsertAt(0, 100);

            Assert.Equal(100, ll.GetAt(0));
            Assert.Equal(100, ll.Head!.Value);
        }

        [Fact]
        public void Test_InsertAt_End()
        {
            SingleLinkedList<byte> ll = CreateSingleLinkedList();

            ll.InsertAt(ll.Count, 100);

            Assert.Equal(100, ll.GetAt(ll.Count - 1));
            Assert.Equal(100, ll.Tail!.Value);
        }

        [Fact]
        public void Test_InsertAt_All_Values()
        {
            SingleLinkedList<byte> ll = CreateSingleLinkedList();

            for (int i = 0; i < VALUES.Length; i++)
            {
                Assert.Equal(VALUES[i], ll.GetAt(i));
            }
        }

        [Fact]
        public void Test_GetAt_Throws_InvalidOperation()
        {
            SingleLinkedList<byte> ll = new SingleLinkedList<byte>();

            Assert.Throws<InvalidOperationException>(() => ll.GetAt(0));
        }

        [Fact]
        public void Test_GetAt_Throws_ArgumentOutOfRange()
        {
            SingleLinkedList<byte> ll = new SingleLinkedList<byte>();
            ll.InsertAt(0, default);

            Assert.Throws<ArgumentOutOfRangeException>(() => ll.GetAt(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => ll.GetAt(100));
        }

        [Fact]
        public void Test_GetAt()
        {
            SingleLinkedList<byte> ll = CreateSingleLinkedList();

            Assert.Equal(VALUES[2], ll.GetAt(2));
        }

        [Fact]
        public void Test_GetAt_Start()
        {
            SingleLinkedList<byte> ll = CreateSingleLinkedList();

            Assert.Equal(VALUES[0], ll.GetAt(0));
            Assert.Equal(VALUES[0], ll.Head!.Value);
        }

        [Fact]
        public void Test_GetAt_End()
        {
            SingleLinkedList<byte> ll = CreateSingleLinkedList();

            Assert.Equal(VALUES[^1], ll.GetAt(ll.Count - 1));
            Assert.Equal(VALUES[^1], ll.Tail!.Value);
        }

        [Fact]
        public void Test_Contains()
        {
            SingleLinkedList<byte> ll = CreateSingleLinkedList();

            foreach (byte value in VALUES)
            {
                Assert.True(ll.Contains(value));
            }
        }

        [Fact]
        public void Test_DeleteAt_Throws_InvalidOperation()
        {
            SingleLinkedList<byte> ll = new SingleLinkedList<byte>();

            Assert.Throws<InvalidOperationException>(() => ll.DeleteAt(0));
        }

        [Fact]
        public void Test_DeleteAt_Throws_ArgumentOutOfRange()
        {
            SingleLinkedList<byte> ll = new SingleLinkedList<byte>();
            ll.InsertAt(0, default);

            Assert.Throws<ArgumentOutOfRangeException>(() => ll.DeleteAt(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => ll.DeleteAt(100));
        }

        [Fact]
        public void Test_DeleteAt()
        {
            SingleLinkedList<byte> ll = CreateSingleLinkedList();
            int oldCount = ll.Count;

            ll.DeleteAt(2);

            Assert.Equal(oldCount - 1, ll.Count);
            Assert.Equal(VALUES[3], ll.GetAt(2));
        }

        [Fact]
        public void Test_DeleteAt_Start()
        {
            SingleLinkedList<byte> ll = CreateSingleLinkedList();
            ll.DeleteAt(0);

            Assert.Equal(VALUES[1], ll.GetAt(0));
            Assert.Equal(VALUES[1], ll.Head!.Value);
        }

        [Fact]
        public void Test_DeleteAt_End()
        {
            SingleLinkedList<byte> ll = CreateSingleLinkedList();
            ll.DeleteAt(ll.Count - 1);

            Assert.Equal(VALUES[^2], ll.GetAt(ll.Count - 1));
            Assert.Equal(VALUES[^2], ll.Tail!.Value);
        }

        [Fact]
        public void Test_DeleteAt_All_Values()
        {
            SingleLinkedList<byte> ll = CreateSingleLinkedList();

            for (int i = 0; i < VALUES.Length; i++)
            {
                ll.DeleteAt(0);
            }

            Assert.True(ll.IsEmpty);
            Assert.Equal(0, ll.Count);
            Assert.Null(ll.Head);
            Assert.Null(ll.Tail);
        }

        [Fact]
        public void Test_Enumeration()
        {
            SingleLinkedList<byte> ll = CreateSingleLinkedList();

            Assert.Equal(VALUES, ll);
        }

        private SingleLinkedList<byte> CreateSingleLinkedList()
        {
            SingleLinkedList<byte> ll = new SingleLinkedList<byte>();

            for (int i = 0; i < VALUES.Length; i++)
            {
                ll.InsertAt(i, VALUES[i]);
            }

            return ll;
        }
    }
}
