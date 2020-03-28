using System;
using System.Collections.Generic;
using DataStructures.LinkedList;
using Xunit;

namespace DataStructures.Test.LinkedList
{
    public class CircularDoubleLinkedListTest
    {
        private static readonly byte[] VALUES = new byte[5] { 1, 2, 3, 4, 5 };

        [Fact]
        public void Test_Constructor()
        {
            CircularDoubleLinkedList<byte> ll = new CircularDoubleLinkedList<byte>();

            Assert.True(ll.IsEmpty);
            Assert.Equal(0, ll.Count);
        }

        [Fact]
        public void Test_InsertAt_Throws_ArgumentOutOfRange()
        {
            CircularDoubleLinkedList<byte> ll = new CircularDoubleLinkedList<byte>();

            Assert.Throws<ArgumentOutOfRangeException>(() => ll.InsertAt(-1, default));
            Assert.Throws<ArgumentOutOfRangeException>(() => ll.InsertAt(100, default));
        }

        [Fact]
        public void Test_InsertAt()
        {
            CircularDoubleLinkedList<byte> ll = CreateCircularDoubleLinkedList();

            Assert.False(ll.IsEmpty);
            Assert.Equal(VALUES.Length, ll.Count);
            Assert.NotNull(ll.Head);
            Assert.NotNull(ll.Tail);
            Assert.Equal(VALUES[0], ll.Head!.Value);
            Assert.Equal(VALUES[^1], ll.Tail!.Value);
            Assert.NotNull(ll.Head!.Previous);
            Assert.NotNull(ll.Tail!.Next);
        }

        [Fact]
        public void Test_InsertAt_Start()
        {
            CircularDoubleLinkedList<byte> ll = CreateCircularDoubleLinkedList();

            ll.InsertAt(0, 100);

            Assert.Equal(100, ll.GetAt(0));
            Assert.Equal(100, ll.Head!.Value);
            Assert.Equal(ll.Head!.Previous, ll.Tail);
        }

        [Fact]
        public void Test_InsertAt_End()
        {
            CircularDoubleLinkedList<byte> ll = CreateCircularDoubleLinkedList();

            ll.InsertAt(ll.Count, 100);

            Assert.Equal(100, ll.GetAt(ll.Count - 1));
            Assert.Equal(100, ll.Tail!.Value);
            Assert.Equal(ll.Tail!.Next, ll.Head);
        }

        [Fact]
        public void Test_InsertAt_All_Values()
        {
            CircularDoubleLinkedList<byte> ll = CreateCircularDoubleLinkedList();

            for (int i = 0; i < VALUES.Length; i++)
            {
                Assert.Equal(VALUES[i], ll.GetAt(i));
            }
        }

        [Fact]
        public void Test_GetAt_Throws_InvalidOperation()
        {
            CircularDoubleLinkedList<byte> ll = new CircularDoubleLinkedList<byte>();

            Assert.Throws<InvalidOperationException>(() => ll.GetAt(0));
        }

        [Fact]
        public void Test_GetAt_Throws_ArgumentOutOfRange()
        {
            CircularDoubleLinkedList<byte> ll = new CircularDoubleLinkedList<byte>();
            ll.InsertAt(0, default);

            Assert.Throws<ArgumentOutOfRangeException>(() => ll.GetAt(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => ll.GetAt(100));
        }

        [Fact]
        public void Test_GetAt()
        {
            CircularDoubleLinkedList<byte> ll = CreateCircularDoubleLinkedList();

            Assert.Equal(VALUES[2], ll.GetAt(2));
        }

        [Fact]
        public void Test_GetAt_Start()
        {
            CircularDoubleLinkedList<byte> ll = CreateCircularDoubleLinkedList();

            Assert.Equal(VALUES[0], ll.GetAt(0));
            Assert.Equal(VALUES[0], ll.Head!.Value);
        }

        [Fact]
        public void Test_GetAt_End()
        {
            CircularDoubleLinkedList<byte> ll = CreateCircularDoubleLinkedList();

            Assert.Equal(VALUES[^1], ll.GetAt(ll.Count - 1));
            Assert.Equal(VALUES[^1], ll.Tail!.Value);
        }

        [Fact]
        public void Test_Contains()
        {
            CircularDoubleLinkedList<byte> ll = CreateCircularDoubleLinkedList();

            foreach (byte value in VALUES)
            {
                Assert.True(ll.Contains(value));
            }
        }

        [Fact]
        public void Test_DeleteAt_Throws_InvalidOperation()
        {
            CircularDoubleLinkedList<byte> ll = new CircularDoubleLinkedList<byte>();

            Assert.Throws<InvalidOperationException>(() => ll.DeleteAt(0));
        }

        [Fact]
        public void Test_DeleteAt_Throws_ArgumentOutOfRange()
        {
            CircularDoubleLinkedList<byte> ll = new CircularDoubleLinkedList<byte>();
            ll.InsertAt(0, default);

            Assert.Throws<ArgumentOutOfRangeException>(() => ll.DeleteAt(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => ll.DeleteAt(100));
        }

        [Fact]
        public void Test_DeleteAt()
        {
            CircularDoubleLinkedList<byte> ll = CreateCircularDoubleLinkedList();
            int oldCount = ll.Count;

            ll.DeleteAt(2);

            Assert.Equal(oldCount - 1, ll.Count);
            Assert.Equal(VALUES[3], ll.GetAt(2));
        }

        [Fact]
        public void Test_DeleteAt_Start()
        {
            CircularDoubleLinkedList<byte> ll = CreateCircularDoubleLinkedList();
            ll.DeleteAt(0);

            Assert.Equal(VALUES[1], ll.GetAt(0));
            Assert.Equal(VALUES[1], ll.Head!.Value);
            Assert.Equal(ll.Head!.Previous, ll.Tail);
            Assert.Equal(ll.Tail!.Next, ll.Head);
        }

        [Fact]
        public void Test_DeleteAt_End()
        {
            CircularDoubleLinkedList<byte> ll = CreateCircularDoubleLinkedList();
            ll.DeleteAt(ll.Count - 1);

            Assert.Equal(VALUES[^2], ll.GetAt(ll.Count - 1));
            Assert.Equal(VALUES[^2], ll.Tail!.Value);
            Assert.Equal(ll.Head!.Previous, ll.Tail);
            Assert.Equal(ll.Tail!.Next, ll.Head);
        }

        [Fact]
        public void Test_DeleteAt_All_Values()
        {
            CircularDoubleLinkedList<byte> ll = CreateCircularDoubleLinkedList();

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
            CircularDoubleLinkedList<byte> ll = CreateCircularDoubleLinkedList();

            Assert.Equal(VALUES, ll);
        }

        [Fact]
        public void Test_Enumeration_Reverse()
        {
            CircularDoubleLinkedList<byte> ll = CreateCircularDoubleLinkedList();

            using IEnumerator<Byte> enumerator = ll.GetReverseEnumerator();
            int counter = 1;

            while (enumerator.MoveNext())
            {
                Assert.Equal(VALUES[^counter], enumerator.Current);

                counter++;
            }
        }

        private CircularDoubleLinkedList<byte> CreateCircularDoubleLinkedList()
        {
            CircularDoubleLinkedList<byte> ll = new CircularDoubleLinkedList<byte>();

            for (int i = 0; i < VALUES.Length; i++)
            {
                ll.InsertAt(i, VALUES[i]);
            }

            return ll;
        }
    }
}
