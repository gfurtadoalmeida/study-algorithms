using System;
using System.Collections.Generic;
using Algorithms.Sorting;
using Xunit;

namespace Algorithms.Test.Sorting
{
    public sealed class KeyIndexedCountingSortTest
    {
        [Fact]
        public void Test()
        {
            KeyValuePair<Int32, Double> kv1 = new KeyValuePair<Int32, Double>(1, 0.5);
            KeyValuePair<Int32, Double> kv2 = new KeyValuePair<Int32, Double>(1, 0.8);
            KeyValuePair<Int32, Double> kv3 = new KeyValuePair<Int32, Double>(2, 1.8);
            KeyValuePair<Int32, Double> kv4 = new KeyValuePair<Int32, Double>(2, 1.2);
            KeyValuePair<Int32, Double> kv5 = new KeyValuePair<Int32, Double>(3, 2.2);
            KeyValuePair<Int32, Double> kv6 = new KeyValuePair<Int32, Double>(3, 2.2);

            KeyValuePair<Int32, Double>[] sorted = new KeyValuePair<Int32, Double>[6]
            {
                kv1, kv2, kv3, kv4, kv5, kv6
            };

            KeyValuePair<Int32, Double>[] notSorted = new KeyValuePair<Int32, Double>[6]
            {
                kv1, kv5, kv2, kv3, kv6, kv4
            };

            KeyIndexedCountingSort.Sort(notSorted, 4);

            AssertUtilities.Sequence(sorted, notSorted);
        }
    }
}
