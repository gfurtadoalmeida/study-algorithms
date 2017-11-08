﻿using System;
using Algorithms.Structures;
using Xunit;

namespace Algorithms.Test.Structures
{
    public sealed class DoubleDoubleLinkedListTest
    {
        [Fact]
        public void Test_Empty_OnCreation()
        {
            DoubleLinkedList<Int32> list = new DoubleLinkedList<Int32>();

            Assert.True(list.IsEmpty);
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void Test_Empty_WhenCleaning()
        {
            DoubleLinkedList<Int32> list = new DoubleLinkedList<Int32>();
            list.Add(2);
            list.Remove();

            Assert.True(list.IsEmpty);
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void Test_Empty_WhenCleaningCustom()
        {
            DoubleLinkedList<Int32> list = new DoubleLinkedList<Int32>();
            list.Add(2);
            list.Remove(2);

            Assert.True(list.IsEmpty);
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void Test_NotEmpty_WhenAdding()
        {
            DoubleLinkedList<Int32> list = new DoubleLinkedList<Int32>();
            list.Add(18);
            list.Add(2);

            Assert.False(list.IsEmpty);
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void Test_NotEmpty_WhenAddingAfter()
        {
            DoubleLinkedList<Int32> list = new DoubleLinkedList<Int32>();
            list.Add(18);
            list.Add(88);
            list.AddAfter(18, 2);

            Assert.False(list.IsEmpty);
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void Test_NotEmpty_WhenAddingBefore()
        {
            DoubleLinkedList<Int32> list = new DoubleLinkedList<Int32>();
            list.Add(18);
            list.Add(88);
            list.AddBefore(88, 2);

            Assert.False(list.IsEmpty);
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void Test_AddRemoveOrder()
        {
            DoubleLinkedList<Int32> list = new DoubleLinkedList<Int32>();
            list.Add(18);
            list.Add(2);
            list.Add(88);

            Assert.Equal(88, list.Remove());
            Assert.Equal(2, list.Remove());
            Assert.Equal(18, list.Remove());
        }

        [Fact]
        public void Test_AddRemoveOrder_After()
        {
            DoubleLinkedList<Int32> list = new DoubleLinkedList<Int32>();
            list.Add(18);
            list.Add(88);
            list.AddAfter(88, 2);

            Assert.Equal(88, list.Remove());
            Assert.Equal(2, list.Remove());
            Assert.Equal(18, list.Remove());
        }

        [Fact]
        public void Test_AddRemoveOrder_Before()
        {
            DoubleLinkedList<Int32> list = new DoubleLinkedList<Int32>();
            list.Add(18);
            list.Add(88);
            list.AddBefore(18, 2);

            Assert.Equal(88, list.Remove());
            Assert.Equal(2, list.Remove());
            Assert.Equal(18, list.Remove());
        }

        [Fact]
        public void Test_AddRemoveOrder_End()
        {
            DoubleLinkedList<Int32> list = new DoubleLinkedList<Int32>();
            list.Add(18);
            list.Add(2);
            list.AddEnd(88);

            Assert.Equal(2, list.Remove());
            Assert.Equal(18, list.Remove());
            Assert.Equal(88, list.Remove());
        }
        
        [Fact]
        public void Test_RemoveOrder_Custom()
        {
            DoubleLinkedList<Int32> list = new DoubleLinkedList<Int32>();
            list.Add(18);
            list.Add(2);
            list.Add(88);
            
            list.Remove(2);

            Assert.Equal(88, list.Remove());
            Assert.Equal(18, list.Remove());
        }

        [Fact]
        public void Test_RemoveOrder_End()
        {
            DoubleLinkedList<Int32> list = new DoubleLinkedList<Int32>();
            list.Add(18);
            list.Add(2);
            list.Add(88);

            Assert.Equal(18, list.RemoveEnd());
        }

        [Fact]
        public void Test_Enumeration_Count()
        {
            DoubleLinkedList<Int32> list = new DoubleLinkedList<Int32>();
            list.Add(18);
            list.Add(2);

            Int32 count = 0;

            var enumerator = list.GetEnumerator();

            while (enumerator.MoveNext())
            {
                count++;
            }

            Assert.Equal(2, count);
        }

        [Fact]
        public void Test_Enumeration_Values()
        {
            DoubleLinkedList<Int32> list = new DoubleLinkedList<Int32>();
            list.Add(2);
            list.Add(4);
            list.Add(8);

            Int32 count = 8;

            var enumerator = list.GetEnumerator();

            while (enumerator.MoveNext())
            {
                Assert.Equal(enumerator.Current, count);

                count /= 2;
            }
        }

        [Fact]
        public void Test_Contains()
        {
            DoubleLinkedList<Int32> list = new DoubleLinkedList<Int32>();
            list.Add(18);
            list.Add(2);

            Assert.True(list.Contains(18));
            Assert.True(list.Contains(2));
        }

        [Fact]
        public void Test_NotContains()
        {
            DoubleLinkedList<Int32> list = new DoubleLinkedList<Int32>();
            list.Add(18);
            list.Add(2);

            Assert.False(list.Contains(88));
        }
    }
}