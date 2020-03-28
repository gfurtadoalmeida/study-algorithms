using System;
using Algorithms.Sorting;
using Xunit;

namespace Algorithms.Test.Sorting
{
    public sealed class HeapSortTest
    {
        [Fact]
        public void Test()
        {
            HeapSort<Int32> sort = new HeapSort<Int32>();
            Int32[] array = SortData.CreateUnsortedArray();

            sort.Sort(array);

            Assert.True(SortData.VerifyArrayIsSorted(array));
        }
    }
}
