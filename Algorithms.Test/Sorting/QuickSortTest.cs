using System;
using Algorithms.Sorting;
using Xunit;

namespace Algorithms.Test.Sorting
{
    public sealed class QuickSortTest
    {
        [Fact]
        public void Test()
        {
            QuickSort<Int32> sort = new QuickSort<Int32>();
            Int32[] array = SortData.CreateUnsortedArray();

            sort.Sort(array);

            Assert.True(SortData.VerifyArrayIsSorted(array));
        }
    }
}
