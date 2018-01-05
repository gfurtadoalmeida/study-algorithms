using System.Collections.Generic;
using Xunit;

namespace Algorithms.Test
{
    public static class AssertExtensions
    {
        public static void Sequence<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            using (IEnumerator<T> enumeratorActual = actual.GetEnumerator())
            using (IEnumerator<T> enumeratorExpected = expected.GetEnumerator())
            {
                while (enumeratorActual.MoveNext())
                {
                    if (!enumeratorExpected.MoveNext())
                        throw new SequenceSizeException();

                    Assert.Equal(enumeratorExpected.Current, enumeratorActual.Current);
                }

                if (enumeratorExpected.MoveNext())
                    throw new SequenceSizeException();
            }
        }
    }

    public sealed class SequenceSizeException : Xunit.Sdk.XunitException
    {
        public SequenceSizeException() : base("IEnumerable 'expected' has a different size than IEnumerable 'value'.")
        {
        }
    }
}
