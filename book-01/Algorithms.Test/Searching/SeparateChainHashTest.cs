using System;
using System.Collections.Generic;
using Algorithms.Searching;
using Xunit;

namespace Algorithms.Test.Searching
{
    public sealed class SeparateChainHashTest
    {
        [Fact]
        public void Test_IsEmpty_Count_OnCreation()
        {
            var hash = new SeparateChainHash<Byte, Char>();

            Assert.True(hash.IsEmpty);
            Assert.Equal(0, hash.Count);
        }

        [Fact]
        public void Test_IsEmpty_Count_WhenCleaning()
        {
            var hash = this.CreateFullSeparateChainHash();

            Int32 count = hash.Count;

            for (byte i = 1; i <= count; i++)
                hash.Delete(i);

            Assert.True(hash.IsEmpty);
            Assert.Equal(0, hash.Count);
        }

        [Fact]
        public void Test_NotEmpty_WhenAdding()
        {
            var hash = this.CreateFullSeparateChainHash();

            Assert.False(hash.IsEmpty);
        }

        [Fact]
        public void Test_Add_Get()
        {
            var hash = new SeparateChainHash<Byte, Char>();

            hash.Add(1, 'A');

            Assert.Equal('A', hash.Get(1));
        }

        [Fact]
        public void Test_Get_KeyNotFoundException()
        {
            var hash = new SeparateChainHash<Byte, Char>();

            Assert.Throws<KeyNotFoundException>(() => hash.Get(100));
        }

        [Fact]
        public void Test_Delete()
        {
            var hash = new SeparateChainHash<Byte, Char>();

            hash.Add(1, 'A');
            hash.Delete(1);

            Assert.False(hash.Contains(1));
        }

        [Fact]
        public void Test_Contains()
        {
            var hash = new SeparateChainHash<Byte, Char>();

            hash.Add(1, 'A');
            hash.Add(2, 'B');

            Assert.True(hash.Contains(1));
            Assert.True(hash.Contains(2));
        }

        [Fact]
        public void Test_Keys()
        {
            var hash = this.CreateFullSeparateChainHash();
            var keys = hash.Keys().GetEnumerator();

            Int32 count = 1;

            while (keys.MoveNext())
                Assert.Equal(count++, keys.Current);
        }

        private SeparateChainHash<Byte, Char> CreateFullSeparateChainHash()
        {
            SeparateChainHash<Byte, Char> hash = new SeparateChainHash<Byte, Char>(7);

            hash.Add(1, 'A');
            hash.Add(2, 'B');
            hash.Add(3, 'C');
            hash.Add(4, 'D');
            hash.Add(5, 'E');
            hash.Add(6, 'F');

            return hash;
        }
    }
}
