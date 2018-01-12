using Algorithms.Strings;
using Algorithms.Strings.Substrings;
using Xunit;

namespace Algorithms.Test.Strings.Substrings
{
    public sealed class BoyerMooreSearchTest
    {
        [Fact]
        public void Test_Found()
        {
            BoyerMooreSearch bm = new BoyerMooreSearch("cro", Alphabet.ASCII);

            Assert.Equal(2, bm.Search("Microsoft"));
        }

        [Fact]
        public void Test_NotFound()
        {
            BoyerMooreSearch bm = new BoyerMooreSearch("gl", Alphabet.ASCII);

            Assert.Equal(-1, bm.Search("Microsoft"));
        }
    }
}
