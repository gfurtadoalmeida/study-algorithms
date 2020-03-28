using System;
using Algorithms.Sorting;
using Xunit;

namespace Algorithms.Test.Sorting
{
    public sealed class InsertionSortTest
    {
        [Fact]
        public void Test()
        {
            InsertionSort<Int32> sort = new InsertionSort<Int32>();
            Int32[] array = SortData.CreateUnsortedArray();

            sort.Sort(array);

            Assert.True(SortData.VerifyArrayIsSorted(array));
        }
    }
}
