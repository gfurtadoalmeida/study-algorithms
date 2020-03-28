using System;
using Algorithms.Strings;
using Xunit;

namespace Algorithms.Test.Strings
{
    public sealed class LeastSignificantDigitFirstSortTest
    {
        [Fact]
        public void Test_ToChar()
        {
            String s1 = "1001";
            String s2 = "1002";
            String s3 = "2013";
            String s4 = "2014";
            String s5 = "3015";

            String[] sorted = new String[5] { s1, s2, s3, s4, s5 };
            String[] unsorted = new String[5] { s5, s3, s4, s1, s2 };

            LeastSignificantDigitFirstSort.Sort(unsorted, 4, Alphabet.Decimal);

            AssertUtilities.Sequence(sorted, unsorted);
        }
    }
}
