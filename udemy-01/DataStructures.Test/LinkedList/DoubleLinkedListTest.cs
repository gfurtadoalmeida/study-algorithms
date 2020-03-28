using System;
using System.Collections.Generic;
using DataStructures.LinkedList;
using Xunit;

namespace DataStructures.Test.LinkedList
{
    public class DoubleLinkedListTest
    {
        private static readonly byte[] VALUES = new byte[5] { 1, 2, 3, 4, 5 };

        [Fact]
        public void Test_Constructor()
        {
            DoubleLinkedList<byte> ll = new DoubleLinkedList<byte>();

            Assert.True(ll.IsEmpty);
            Assert.Equal(0, ll.Count);
        }

        [Fact]
        public void Test_InsertAt_Throws_ArgumentOutOfRange()
        {
            DoubleLinkedList<byte> ll = new DoubleLinkedList<byte>();

            Assert.Throws<ArgumentOutOfRangeException>(() => ll.InsertAt(-1, default));
            Assert.Throws<ArgumentOutOfRangeException>(() => ll.InsertAt(100, default));
        }

        [Fact]
        public void Test_InsertAt()
        {
            DoubleLinkedList<byte> ll = CreateDoubleLinkedList();

            Assert.False(ll.IsEmpty);
            Assert.Equal(VALUES.Length, ll.Count);
            Assert.NotNull(ll.Head);
            Assert.NotNull(ll.Tail);
            Assert.Equal(VALUES[0], ll.Head!.Value);
            Assert.Equal(VALUES[^1], ll.Tail!.Value);
            Assert.Null(ll.Head!.Previous);
            Assert.Null(ll.Tail!.Next);
        }

        [Fact]
        public void Test_InsertAt_Start()
        {
            DoubleLinkedList<byte> ll = CreateDoubleLinkedList();

            ll.InsertAt(0, 100);

            Assert.Equal(100, ll.GetAt(0));
            Assert.Equal(100, ll.Head!.Value);
            Assert.Null(ll.Head!.Previous);
        }

        [Fact]
        public void Test_InsertAt_End()
        {
            DoubleLinkedList<byte> ll = CreateDoubleLinkedList();

            ll.InsertAt(ll.Count, 100);

            Assert.Equal(100, ll.GetAt(ll.Count - 1));
            Assert.Equal(100, ll.Tail!.Value);
            Assert.Null(ll.Tail!.Next);
        }

        [Fact]
        public void Test_InsertAt_All_Values()
        {
            DoubleLinkedList<byte> ll = CreateDoubleLinkedList();

            for (int i = 0; i < VALUES.Length; i++)
            {
                Assert.Equal(VALUES[i], ll.GetAt(i));
            }
        }

        [Fact]
        public void Test_GetAt_Throws_InvalidOperation()
        {
            DoubleLinkedList<byte> ll = new DoubleLinkedList<byte>();

            Assert.Throws<InvalidOperationException>(() => ll.GetAt(0));
        }

        [Fact]
        public void Test_GetAt_Throws_ArgumentOutOfRange()
        {
            DoubleLinkedList<byte> ll = new DoubleLinkedList<byte>();
            ll.InsertAt(0, default);

            Assert.Throws<ArgumentOutOfRangeException>(() => ll.GetAt(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => ll.GetAt(100));
        }

        [Fact]
        public void Test_GetAt()
        {
            DoubleLinkedList<byte> ll = CreateDoubleLinkedList();

            Assert.Equal(VALUES[2], ll.GetAt(2));
        }

        [Fact]
        public void Test_GetAt_Start()
        {
            DoubleLinkedList<byte> ll = CreateDoubleLinkedList();

            Assert.Equal(VALUES[0], ll.GetAt(0));
            Assert.Equal(VALUES[0], ll.Head!.Value);
        }

        [Fact]
        public void Test_GetAt_End()
        {
            DoubleLinkedList<byte> ll = CreateDoubleLinkedList();

            Assert.Equal(VALUES[^1], ll.GetAt(ll.Count - 1));
            Assert.Equal(VALUES[^1], ll.Tail!.Value);
        }

        [Fact]
        public void Test_Contains()
        {
            DoubleLinkedList<byte> ll = CreateDoubleLinkedList();

            foreach (byte value in VALUES)
            {
                Assert.True(ll.Contains(value));
            }
        }

        [Fact]
        public void Test_DeleteAt_Throws_InvalidOperation()
        {
            DoubleLinkedList<byte> ll = new DoubleLinkedList<byte>();

            Assert.Throws<InvalidOperationException>(() => ll.DeleteAt(0));
        }

        [Fact]
        public void Test_DeleteAt_Throws_ArgumentOutOfRange()
        {
            DoubleLinkedList<byte> ll = new DoubleLinkedList<byte>();
            ll.InsertAt(0, default);

            Assert.Throws<ArgumentOutOfRangeException>(() => ll.DeleteAt(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => ll.DeleteAt(100));
        }

        [Fact]
        public void Test_DeleteAt()
        {
            DoubleLinkedList<byte> ll = CreateDoubleLinkedList();
            int oldCount = ll.Count;

            ll.DeleteAt(2);

            Assert.Equal(oldCount - 1, ll.Count);
            Assert.Equal(VALUES[3], ll.GetAt(2));
        }

        [Fact]
        public void Test_DeleteAt_Start()
        {
            DoubleLinkedList<byte> ll = CreateDoubleLinkedList();
            ll.DeleteAt(0);

            Assert.Equal(VALUES[1], ll.GetAt(0));
            Assert.Equal(VALUES[1], ll.Head!.Value);
            Assert.Null(ll.Head!.Previous);
        }

        [Fact]
        public void Test_DeleteAt_End()
        {
            DoubleLinkedList<byte> ll = CreateDoubleLinkedList();
            ll.DeleteAt(ll.Count - 1);

            Assert.Equal(VALUES[^2], ll.GetAt(ll.Count - 1));
            Assert.Equal(VALUES[^2], ll.Tail!.Value);
            Assert.Null(ll.Tail!.Next);
        }

        [Fact]
        public void Test_DeleteAt_All_Values()
        {
            DoubleLinkedList<byte> ll = CreateDoubleLinkedList();

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
            DoubleLinkedList<byte> ll = CreateDoubleLinkedList();

            Assert.Equal(VALUES, ll);
        }

        [Fact]
        public void Test_Enumeration_Reverse()
        {
            DoubleLinkedList<byte> ll = CreateDoubleLinkedList();

            using IEnumerator<Byte> enumerator = ll.GetReverseEnumerator();
            int counter = 1;

            while (enumerator.MoveNext())
            {
                Assert.Equal(VALUES[^counter], enumerator.Current);

                counter++;
            }
        }

        private DoubleLinkedList<byte> CreateDoubleLinkedList()
        {
            DoubleLinkedList<byte> ll = new DoubleLinkedList<byte>();

            for (int i = 0; i < VALUES.Length; i++)
            {
                ll.InsertAt(i, VALUES[i]);
            }

            return ll;
        }
    }
}
