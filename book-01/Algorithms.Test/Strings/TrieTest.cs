using System;
using System.Collections.Generic;
using Algorithms.Strings;
using Xunit;

namespace Algorithms.Test.Strings
{
    public sealed class TrieTest
    {
        [Fact]
        public void Test_IsEmpty_OnCreation()
        {
            Trie trie = new Trie(Alphabet.Binary);

            Assert.True(trie.IsEmpty);
            Assert.Equal(0, trie.Count);
        }

        [Fact]
        public void Test_Count_WhenAdding()
        {
            Trie trie = this.CreateTrie();

            Assert.False(trie.IsEmpty);
            Assert.Equal(9, trie.Count);
        }

        [Fact]
        public void Test_Count_WhenDeleting()
        {
            Trie trie = this.CreateTrie();

            trie.Delete("1000");

            Assert.Equal(8, trie.Count);
        }

        [Fact]
        public void Test_Contains()
        {
            Trie trie = this.CreateTrie();

            Assert.True(trie.Contains("1000"));
        }

        [Fact]
        public void Test_NotContains()
        {
            Trie trie = this.CreateTrie();

            Assert.False(trie.Contains("9999"));
        }

        [Fact]
        public void Test_Add()
        {
            Trie trie = this.CreateTrie();

            trie.Add("9000", 9000);

            Assert.True(trie.Contains("9000"));
        }

        [Fact]
        public void Test_Get()
        {
            Trie trie = this.CreateTrie();

            trie.Add("9000", 9000);

            Assert.Equal(9000, trie.Get("9000"));
        }

        [Fact]
        public void Test_Get_Throws_KeyNotFound()
        {
            Trie trie = this.CreateTrie();

            Assert.Throws<KeyNotFoundException>(() => trie.Get("9000"));
        }

        [Fact]
        public void Test_Delete()
        {
            Trie trie = this.CreateTrie();

            trie.Delete("1000");

            Assert.False(trie.Contains("1000"));
        }

        [Fact]
        public void Test_GetKeys()
        {
            Trie trie = this.CreateTrie();

            AssertUtilities.Sequence(new String[9]
            {
                "1000",
                "1001",
                "1002",
                "1010",
                "1011",
                "1012",
                "1110",
                "1111",
                "1112"
            },
            trie.GetKeys());
        }

        [Fact]
        public void Test_GetKeysThatMatch()
        {
            Trie trie = this.CreateTrie();

            AssertUtilities.Sequence(new String[3]
            {
                "1110",
                "1111",
                "1112"
            },
            trie.GetKeysThatMatch("111."));
        }

        [Fact]
        public void Test_GetKeysWithPrefix()
        {
            Trie trie = this.CreateTrie();

            AssertUtilities.Sequence(new String[3]
            {
                "1010",
                "1011",
                "1012"
            },
            trie.GetKeysWithPrefix("101"));
        }

        [Fact]
        public void Test_GetLongestPrefixOf()
        {
            Trie trie = this.CreateTrie();

            Assert.Equal("1001", trie.GetLongestPrefixOf("100123"));
        }

        private Trie CreateTrie()
        {
            Trie trie = new Trie(Alphabet.Decimal);
            trie.Add("1000", 1000);
            trie.Add("1001", 1001);
            trie.Add("1002", 1002);
            trie.Add("1010", 1010);
            trie.Add("1011", 1011);
            trie.Add("1012", 1012);
            trie.Add("1110", 1110);
            trie.Add("1111", 1111);
            trie.Add("1112", 1112);

            return trie;
        }
    }
}
