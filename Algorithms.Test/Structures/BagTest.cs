using System;
using Algorithms.Structures;
using Xunit;

namespace Algorithms.Test.Structures
{
    public sealed class BagTest
    {
        [Fact]
        public void Test_Empty_OnCreation()
        {
            Bag<Int32> bag = new Bag<Int32>();

            Assert.True(bag.IsEmpty);
            Assert.Equal(0, bag.Count);
        }

        [Fact]
        public void Test_NotEmpty_WhenAdding()
        {
            Bag<Int32> bag = new Bag<Int32>();
            bag.Add(18);
            bag.Add(2);

            Assert.False(bag.IsEmpty);
            Assert.Equal(2, bag.Count);
        }

        [Fact]
        public void Test_Iteration()
        {
            Bag<Int32> bag = new Bag<Int32>();
            bag.Add(18);
            bag.Add(2);
            bag.Add(88);

            AssertUtilities.Sequence(new Int32[3] { 88, 2, 18 }, bag);
        }
    }
}
