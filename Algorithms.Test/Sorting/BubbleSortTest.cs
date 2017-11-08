using System;
using Algorithms.Sorting;
using Xunit;

namespace Algorithms.Test.Sorting
{
    public sealed class BubbleSortTest
    {
        [Fact]
        public void Test()
        {
            BubbleSort<Int32> sort = new BubbleSort<Int32>();
            Int32[] array = SortData.CreateUnsortedArray();

            sort.Sort(array);

            Assert.True(SortData.VerifyArrayIsSorted(array));
        }
    }
}
