using System;
using DataStructures.Hash;
using Xunit;

namespace DataStructures.Test.Hash
{
    public abstract class BaseOpenAddressingHashTableTest : BaseHashTableTest
    {
        [Fact]
        public void Test_Constructor_Throws_ArgumenException()
        {
            Assert.Throws<ArgumentException>(() => this.CreateInstance(1, null!));
        }

        [Fact]
        public void Test_Resize_Up()
        {
            IHashTable<string> ht = this.CreateInstance(10);

            int oldSize = ht.Size;

            // It's needed more than 75% to trigger
            // a resize up.
            for (int i = 0; i < oldSize; i++)
            {
                ht.Add(i.ToString());
            }

            for (int i = 0; i < oldSize; i++)
            {
                Assert.True(ht.Contains(i.ToString()));
            }

            Assert.Equal(oldSize * 2, ht.Size);
            Assert.Equal(oldSize, ht.Count);
        }

        [Fact]
        public void Test_Resize_Down()
        {
            IHashTable<string> ht = this.CreateInstance(10);

            int oldSize = ht.Size;

            // It's needed more than 75% to trigger
            // a resize up.
            for (int i = 0; i < oldSize; i++)
            {
                ht.Add(i.ToString());
            }

            oldSize = ht.Size;

            // Resize up = 20
            // 20 - 20 * 0.75 = 5
            // Trigger count = 5
            int triggerCount = (int)(oldSize - oldSize * 0.75);

            // It's needed less than 25% to trigger
            // a resize down.
            // Removing the upper values eases testing.
            for (int i = oldSize; i >= triggerCount; i--)
            {
                ht.Delete(i.ToString());
            }

            for (int i = 0; i < (oldSize - oldSize * 0.75); i++)
            {
                Assert.True(ht.Contains(i.ToString()));
            }

            Assert.Equal(oldSize / 2, ht.Size);
            Assert.Equal(triggerCount, ht.Count);
        }
    }
}
