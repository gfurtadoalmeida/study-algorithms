using System;
using Algorithms.Sorting;
using Xunit;

namespace Algorithms.Test.Sorting
{
    public sealed class ShellSortTest
    {
        [Fact]
        public void Test()
        {
            ShellSort<Int32> sort = new ShellSort<Int32>();
            Int32[] array = SortData.CreateUnsortedArray();

            sort.Sort(array);

            Assert.True(SortData.VerifyArrayIsSorted(array));
        }
    }
}
