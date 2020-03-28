using System;
using Algorithms.Sorting;
using Xunit;

namespace Algorithms.Test.Sorting
{
    public sealed class MergeSortTest
    {
        [Fact]
        public void Test_TopDown()
        {
            MergeSort<Int32> sort = new MergeSort<Int32>(MergeSortType.TopDown);
            Int32[] array = SortData.CreateUnsortedArray();

            sort.Sort(array);

            Assert.True(SortData.VerifyArrayIsSorted(array));
        }

        [Fact]
        public void Test_BottomUp()
        {
            MergeSort<Int32> sort = new MergeSort<Int32>(MergeSortType.TopDown);
            Int32[] array = SortData.CreateUnsortedArray();

            sort.Sort(array);

            Assert.True(SortData.VerifyArrayIsSorted(array));
        }
    }
}
