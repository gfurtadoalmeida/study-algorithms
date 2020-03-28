using System;
using DataStructures.Hash;
using DataStructures.Hash.Functions;
using Xunit;

namespace DataStructures.Test.Hash
{
    public abstract class BaseHashTableTest
    {
        private readonly string[] VALUES = new string[4]
        {
            "Saxon",
            "Overkill",
            "Flotsam and Jetsam",
            "Metal Church"
        };

        protected abstract IHashTable<string> CreateInstance(int initialSize, IHashFunction hashFunction);

        [Fact]
        public void Test_Constructor_Throws_ArgumentOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => this.CreateInstance(0));
        }

        [Fact]
        public void Test_Constructor_Throws_ArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => this.CreateInstance(2, null!));
        }

        [Fact]
        public void Test_Constructor()
        {
            IHashTable<string> ht = this.CreateInstance(2);

            Assert.Equal(2, ht.Size);
            Assert.Equal(0, ht.Count);
        }

        [Fact]
        public void Test_Add_Throws_ArgumentNull()
        {
            IHashTable<string> ht = this.CreateInstance(2);

            Assert.Throws<ArgumentNullException>(() => ht.Add(null!));
        }

        [Fact]
        public void Test_Add()
        {
            IHashTable<string> ht = this.CreateInstance(2);

            ht.Add("Metal Church");

            Assert.True(ht.Contains("Metal Church"));
            Assert.Equal(1, ht.Count);
        }

        [Fact]
        public void Test_Contains_Throws_ArgumentNull()
        {
            IHashTable<string> ht = this.CreateInstance(2);

            Assert.Throws<ArgumentNullException>(() => ht.Contains(null!));
        }

        [Fact]
        public void Test_Contains()
        {
            IHashTable<string> ht = this.CreateFullHashTable();

            foreach (string value in VALUES)
            {
                Assert.True(ht.Contains(value));
            }
        }

        [Fact]
        public void Test_Delete_Throws_ArgumentNull()
        {
            IHashTable<string> ht = this.CreateInstance(2);

            Assert.Throws<ArgumentNullException>(() => ht.Delete(null!));
        }

        [Fact]
        public void Test_Delete()
        {
            IHashTable<string> ht = this.CreateFullHashTable();

            foreach (string value in VALUES)
            {
                ht.Delete(value);

                Assert.False(ht.Contains(value));
            }

            Assert.Equal(0, ht.Count);
        }

        protected IHashTable<string> CreateInstance(int initialSize)
        {
            return this.CreateInstance(initialSize, AsciiCharSumHashFunction.Instance);
        }

        protected IHashTable<string> CreateFullHashTable()
        {
            IHashTable<string> hashTable = this.CreateInstance(VALUES.Length, AsciiCharSumHashFunction.Instance);

            foreach (string value in VALUES)
            {
                hashTable.Add(value);
            }

            return hashTable;
        }
    }
}
