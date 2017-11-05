using System;
using Algorithms.Structures;
using Xunit;

namespace Algorithms.Test.Structures
{
    public sealed class LinkedListTest
    {
        [Fact]
        public void Test_Empty_OnCreation()
        {
            LinkedList<Int32> list = new LinkedList<Int32>();

            Assert.True(list.IsEmpty);
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void Test_Empty_WhenCleaning()
        {
            LinkedList<Int32> list = new LinkedList<Int32>();
            list.Add(2);
            list.Remove();

            Assert.True(list.IsEmpty);
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void Test_NotEmpty_WhenPushing()
        {
            LinkedList<Int32> list = new LinkedList<Int32>();
            list.Add(18);
            list.Add(2);

            Assert.False(list.IsEmpty);
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void Test_AddRemoveOrder()
        {
            LinkedList<Int32> stack = new LinkedList<Int32>();
            stack.Add(18);
            stack.Add(2);
            stack.Add(88);

            Assert.Equal(88, stack.Remove());
            Assert.Equal(2, stack.Remove());
            Assert.Equal(18, stack.Remove());
        }

        [Fact]
        public void Test_Enumeration_Count()
        {
            LinkedList<Int32> list = new LinkedList<Int32>();
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
            LinkedList<Int32> list = new LinkedList<Int32>();
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
            LinkedList<Int32> list = new LinkedList<Int32>();
            list.Add(18);
            list.Add(2);

            Assert.True(list.Contains(18));
            Assert.True(list.Contains(2));
        }

        [Fact]
        public void Test_NotContains()
        {
            LinkedList<Int32> list = new LinkedList<Int32>();
            list.Add(18);
            list.Add(2);

            Assert.False(list.Contains(88));
        }
    }
}
