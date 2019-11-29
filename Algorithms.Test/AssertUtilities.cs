using System;
using System.Collections.Generic;
using Xunit;

namespace Algorithms.Test
{
    public static class AssertUtilities
    {
        public static void Sequence<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            Int32 countExpected = 0;
            Int32 countActual = 0;

            using IEnumerator<T> enumeratorActual = actual.GetEnumerator();
            using IEnumerator<T> enumeratorExpected = expected.GetEnumerator();

            while (enumeratorActual.MoveNext())
            {
                countExpected++;
                countActual++;

                if (!enumeratorExpected.MoveNext())
                {
                    // Unroll to count.
                    while (enumeratorActual.MoveNext())
                        countActual++;

                    throw new SequenceSizeException(countExpected, countActual);
                }

                Assert.Equal(enumeratorExpected.Current, enumeratorActual.Current);
            }

            if (enumeratorExpected.MoveNext())
            {
                countExpected++;

                // Unroll to count.
                while (enumeratorExpected.MoveNext())
                    countExpected++;

                throw new SequenceSizeException(countExpected, countActual);
            }
        }
    }

    public sealed class SequenceSizeException : Xunit.Sdk.XunitException
    {
        public SequenceSizeException(Int32 countExpected, Int32 countActual) : base($"Sequence count not equal. Expected: {countExpected} | Actual: {countActual}.")
        {
        }
    }
}
