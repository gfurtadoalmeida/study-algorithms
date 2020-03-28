using System;
using DataStructures.LinkedList;
using Xunit;

namespace DataStructures.Test.LinkedList
{
    public class CircularLinkedListTest
    {
        private static readonly byte[] VALUES = new byte[5] { 5, 10, 15, 20, 25 };

        [Fact]
        public void Test_Constructor()
        {
            CircularSingleLinkedList<byte> ll = new CircularSingleLinkedList<byte>();

            Assert.True(ll.IsEmpty);
            Assert.Equal(0, ll.Count);
        }

        [Fact]
        public void Test_InsertAt_Throws_ArgumentOutOfRange()
        {
            CircularSingleLinkedList<byte> ll = new CircularSingleLinkedList<byte>();

            Assert.Throws<ArgumentOutOfRangeException>(() => ll.InsertAt(-1, default));
            Assert.Throws<ArgumentOutOfRangeException>(() => ll.InsertAt(100, default));
        }
              
        [Fact]
        public void Test_InsertAt()
        {
            CircularSingleLinkedList<byte> ll = CreateCircularSingleLinkedList();

            Assert.False(ll.IsEmpty);
            Assert.Equal(VALUES.Length, ll.Count);
            Assert.NotNull(ll.Head);
            Assert.NotNull(ll.Tail);
            Assert.Equal(VALUES[0], ll.Head!.Value);
            Assert.Equal(VALUES[^1], ll.Tail!.Value);
            Assert.Equal(ll.Tail!.Next, ll.Head);
        }

        [Fact]
        public void Test_InsertAt_Start()
        {
            CircularSingleLinkedList<byte> ll = CreateCircularSingleLinkedList();

            ll.InsertAt(0, 100);

            Assert.Equal(100, ll.GetAt(0));
            Assert.Equal(100, ll.Head!.Value);
            Assert.Equal(ll.Tail!.Next, ll.Head);
        }

        [Fact]
        public void Test_InsertAt_End()
        {
            CircularSingleLinkedList<byte> ll = CreateCircularSingleLinkedList();

            ll.InsertAt(ll.Count, 100);

            Assert.Equal(100, ll.GetAt(ll.Count - 1));
            Assert.Equal(100, ll.Tail!.Value);
            Assert.Equal(ll.Tail!.Next, ll.Head);
        }

        [Fact]
        public void Test_InsertAt_All_Values()
        {
            CircularSingleLinkedList<byte> ll = CreateCircularSingleLinkedList();

            for (int i = 0; i < VALUES.Length; i++)
            {
                Assert.Equal(VALUES[i], ll.GetAt(i));
            }
        }

        [Fact]
        public void Test_GetAt_Throws_InvalidOperation()
        {
            CircularSingleLinkedList<byte> ll = new CircularSingleLinkedList<byte>();

            Assert.Throws<InvalidOperationException>(() => ll.GetAt(0));
        }

        [Fact]
        public void Test_GetAt_Throws_ArgumentOutOfRange()
        {
            CircularSingleLinkedList<byte> ll = new CircularSingleLinkedList<byte>();
            ll.InsertAt(0, default);

            Assert.Throws<ArgumentOutOfRangeException>(() => ll.GetAt(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => ll.GetAt(100));
        }

        [Fact]
        public void Test_GetAt()
        {
            CircularSingleLinkedList<byte> ll = CreateCircularSingleLinkedList();

            Assert.Equal(VALUES[2], ll.GetAt(2));
        }

        [Fact]
        public void Test_GetAt_Start()
        {
            CircularSingleLinkedList<byte> ll = CreateCircularSingleLinkedList();

            Assert.Equal(VALUES[0], ll.GetAt(0));
            Assert.Equal(VALUES[0], ll.Head!.Value);
        }

        [Fact]
        public void Test_GetAt_End()
        {
            CircularSingleLinkedList<byte> ll = CreateCircularSingleLinkedList();

            Assert.Equal(VALUES[^1], ll.GetAt(ll.Count - 1));
            Assert.Equal(VALUES[^1], ll.Tail!.Value);
        }

        [Fact]
        public void Test_Contains()
        {
            CircularSingleLinkedList<byte> ll = CreateCircularSingleLinkedList();

            foreach (byte value in VALUES)
            {
                Assert.True(ll.Contains(value));
            }
        }

        [Fact]
        public void Test_DeleteAt_Throws_InvalidOperation()
        {
            CircularSingleLinkedList<byte> ll = new CircularSingleLinkedList<byte>();

            Assert.Throws<InvalidOperationException>(() => ll.DeleteAt(0));
        }

        [Fact]
        public void Test_DeleteAt_Throws_ArgumentOutOfRange()
        {
            CircularSingleLinkedList<byte> ll = new CircularSingleLinkedList<byte>();
            ll.InsertAt(0, default);

            Assert.Throws<ArgumentOutOfRangeException>(() => ll.DeleteAt(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => ll.DeleteAt(100));
        }

        [Fact]
        public void Test_DeleteAt()
        {
            CircularSingleLinkedList<byte> ll = CreateCircularSingleLinkedList();
            int oldCount = ll.Count;

            ll.DeleteAt(2);

            Assert.Equal(oldCount - 1, ll.Count);
            Assert.Equal(VALUES[3], ll.GetAt(2));
        }

        [Fact]
        public void Test_DeleteAt_Start()
        {
            CircularSingleLinkedList<byte> ll = CreateCircularSingleLinkedList();
            ll.DeleteAt(0);

            Assert.Equal(VALUES[1], ll.GetAt(0));
            Assert.Equal(VALUES[1], ll.Head!.Value);
            Assert.Equal(ll.Tail!.Next, ll.Head);
        }

        [Fact]
        public void Test_DeleteAt_End()
        {
            CircularSingleLinkedList<byte> ll = CreateCircularSingleLinkedList();
            ll.DeleteAt(ll.Count - 1);

            Assert.Equal(VALUES[^2], ll.GetAt(ll.Count - 1));
            Assert.Equal(VALUES[^2], ll.Tail!.Value);
            Assert.Equal(ll.Tail!.Next, ll.Head);
        }

        [Fact]
        public void Test_DeleteAt_All_Values()
        {
            CircularSingleLinkedList<byte> ll = CreateCircularSingleLinkedList();

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
            CircularSingleLinkedList<byte> ll = CreateCircularSingleLinkedList();

            Assert.Equal(VALUES, ll);
        }

        private CircularSingleLinkedList<byte> CreateCircularSingleLinkedList()
        {
            CircularSingleLinkedList<byte> ll = new CircularSingleLinkedList<byte>();

            for (int i = 0; i < VALUES.Length; i++)
            {
                ll.InsertAt(i, VALUES[i]);
            }

            return ll;
        }
    }
}
