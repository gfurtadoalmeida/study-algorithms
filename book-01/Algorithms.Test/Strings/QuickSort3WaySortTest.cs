using System;
using Algorithms.Strings;
using Xunit;

namespace Algorithms.Test.Strings
{
    public sealed class QuickSort3WaySortTest
    {
        [Fact]
        public void Test_ToChar()
        {
            String s1 = "100";
            String s2 = "10021";
            String s3 = "201341";
            String s4 = "201442";
            String s5 = "3015";
            String s6 = "400";
            String s7 = "40021";
            String s8 = "501341";
            String s9 = "501442";
            String s10 = "6015";
            String s11 = "700";
            String s12 = "70021";
            String s13 = "801341";
            String s14 = "801442";
            String s15 = "9015";
            String s16 = "9016";

            String[] sorted = new String[16] { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16 };
            String[] unsorted = new String[16] { s8, s5, s9, s15, s3, s4, s13, s1, s11, s2, s7, s16, s10, s12, s14, s6 };

            QuickSort3WaySort.Sort(unsorted);

            AssertUtilities.Sequence(sorted, unsorted);
        }
    }
}
