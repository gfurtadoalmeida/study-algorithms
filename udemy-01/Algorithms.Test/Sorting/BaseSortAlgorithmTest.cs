using System;
using System.Runtime.CompilerServices;
using Algorithms.Sorting;
using Xunit;

namespace Algorithms.Test.Sorting
{
    public abstract class BaseSortAlgorithmTest
    {
        private static readonly byte[] UNSORTED_VALUES = new byte[20] { 75, 40, 247, 3, 115, 76, 133, 43, 141, 10, 58, 126, 32, 214, 16, 10, 61, 139, 90, 194 };
        private static readonly byte[] SORTED_VALUES = new byte[20] { 3, 10, 10, 16, 32, 40, 43, 58, 61, 75, 76, 90, 115, 126, 133, 139, 141, 194, 214, 247 };

        protected abstract ISortAlgorithm CreateInstance();

        [Fact]
        public void Test_Sort_Throws_ArgumentNull()
        {
            byte[] array = null!;

            Assert.Throws<ArgumentNullException>(() => this.CreateInstance().Sort(ref array));
        }

        [Fact]
        public void Test_Sort()
        {
            byte[] array = CloneUnsortedValues();

            this.CreateInstance().Sort(ref array);

            Assert.Equal(SORTED_VALUES, array);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] CloneUnsortedValues()
        {
            byte[] array = new byte[UNSORTED_VALUES.Length];

            UNSORTED_VALUES.CopyTo(array, 0);

            return array;
        }
    }
}
