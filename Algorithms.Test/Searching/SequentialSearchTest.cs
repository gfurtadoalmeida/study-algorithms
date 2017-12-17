using System;
using System.Collections.Generic;
using Algorithms.Searching;
using Xunit;

namespace Algorithms.Test.Searching
{
    public sealed class SequentialSearchTest
    {
        [Fact]
        public void Test_IsEmpty_Size_OnCreation()
        {
            var seq = new SequentialSearch<Byte, Char>();

            Assert.True(seq.IsEmpty);
            Assert.Equal(0, seq.Size);
        }

        [Fact]
        public void Test_IsEmpty_Size_WhenCleaning()
        {
            var seq = this.CreateFullSequencialSearch();

            Int32 count = seq.Size;

            for (byte i = 1; i <= count; i++)
                seq.Delete(i);

            Assert.True(seq.IsEmpty);
            Assert.Equal(0, seq.Size);
        }

        [Fact]
        public void Test_NotEmpty_WhenPutting()
        {
            var seq = this.CreateFullSequencialSearch();

            Assert.False(seq.IsEmpty);
        }

        [Fact]
        public void Test_Add_Get()
        {
            var seq = new SequentialSearch<Byte, Char>();

            seq.Add(1, 'A');

            Assert.Equal('A', seq.Get(1));
        }

        [Fact]
        public void Test_Get_KeyNotFoundException()
        {
            var seq = new SequentialSearch<Byte, Char>();

            Assert.Throws(typeof(KeyNotFoundException), () => seq.Get(100));
        }

        [Fact]
        public void Test_Delete()
        {
            var seq = new SequentialSearch<Byte, Char>();

            seq.Add(1, 'A');
            seq.Delete(1);

            Assert.False(seq.Contains(1));
        }

        [Fact]
        public void Test_Contains()
        {
            var seq = new SequentialSearch<Byte, Char>();

            seq.Add(1, 'A');
            seq.Add(2, 'B');

            Assert.True(seq.Contains(1));
            Assert.True(seq.Contains(2));
        }

        [Fact]
        public void Test_Keys()
        {
            var seq = this.CreateFullSequencialSearch();
            var keys = seq.Keys().GetEnumerator();

            Int32 count = seq.Size;

            while (keys.MoveNext())
                Assert.Equal(count--, keys.Current);
        }

        private SequentialSearch<Byte, Char> CreateFullSequencialSearch()
        {
            SequentialSearch<Byte, Char> seq = new SequentialSearch<Byte, Char>();
            
            seq.Add(1, 'A');
            seq.Add(2, 'B');
            seq.Add(3, 'C');
            seq.Add(4, 'D');
            seq.Add(5, 'E');
            seq.Add(6, 'F');

            return seq;
        }
    }
}
