using System.Collections.Generic;
using Xunit;

namespace DataStructures.Test
{
    public static class AssertExtensions
    {
        public static void Equal<T>(T[] expected, IEnumerator<T> actual)
        {
            byte i = 0;

            while (actual.MoveNext())
            {
                Assert.Equal(expected[i], actual.Current);

                i++;
            }
        }
    }
}
