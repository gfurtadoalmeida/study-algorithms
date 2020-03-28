using System;
using Xunit;
using T = DataStructures.Trie.Trie;

namespace DataStructures.Test.Trie
{
    public class TrieTest
    {
        private readonly string VALUE_1 = "OVERKILL";

        [Fact]
        public void Test_Add_Throws_ArgumentNull_When_Null()
        {
            T trie = new T();

            Assert.Throws<ArgumentNullException>(() => trie.Add(null!));
        }

        [Fact]
        public void Test_Add_And_Get_True()
        {
            T trie = new T();

            trie.Add(VALUE_1);

            Assert.True(trie.Contains(VALUE_1));
        }

        [Fact]
        public void Test_Contains_Throws_ArgumentNull_When_Null()
        {
            T trie = new T();

            Assert.Throws<ArgumentNullException>(() => trie.Contains(null!));
        }

        [Fact]
        public void Test_Contains()
        {
            T trie = new T();

            trie.Add(VALUE_1);
            trie.Add("SAXON");

            Assert.True(trie.Contains(VALUE_1));
            Assert.True(trie.Contains("SAXON"));
        }

        [Fact]
        public void Test_Contains_False_SamePrefix()
        {
            T trie = new T();

            trie.Add(VALUE_1);

            Assert.False(trie.Contains(VALUE_1[0..4]));
        }

        [Fact]
        public void Test_Delete_Throws_ArgumentNull_When_Null()
        {
            T trie = new T();

            Assert.Throws<ArgumentNullException>(() => trie.Delete(null!));
        }

        [Fact]
        public void Test_Delete_Case_1()
        {
            T trie = new T();

            trie.Add("BCDE");
            trie.Add("BCKG");

            trie.Delete("BCDE");

            Assert.False(trie.Contains("BCDE"));
            Assert.True(trie.Contains("BCKG"));
        }

        [Fact]
        public void Test_Delete_Case_2()
        {
            T trie = new T();

            trie.Add("BCDE");
            trie.Add("BCDEF");

            trie.Delete("BCDE");

            Assert.False(trie.Contains("BCDE"));
            Assert.True(trie.Contains("BCDEF"));
        }

        [Fact]
        public void Test_Delete_Case_3()
        {
            T trie = new T();

            trie.Add("BCDE");
            trie.Add("BC");

            trie.Delete("BCDE");

            Assert.False(trie.Contains("BCDE"));
            Assert.True(trie.Contains("BC"));
        }

        [Fact]
        public void Test_Delete_Case_4()
        {
            T trie = new T();

            trie.Add(VALUE_1);
            trie.Add("W");

            trie.Delete("W");

            Assert.False(trie.Contains("W"));
            Assert.True(trie.Contains(VALUE_1));
        }

        [Fact]
        public void Test_Delete_Case_5()
        {
            T trie = new T();

            trie.Add("BCDE");
            trie.Add("BCDG");

            trie.Delete("BCDE");

            Assert.False(trie.Contains("BCDE"));
            Assert.True(trie.Contains("BCDG"));
        }
    }
}
