using Algorithms.Strings.Substrings;
using Xunit;

namespace Algorithms.Test.Strings.Substrings
{
    public sealed class BruteForceSearchTest
    {
        [Fact]
        public void Test_Found()
        {
            Assert.Equal(2, BruteForceSearch.Search("cro", "Microsoft"));
        }

        [Fact]
        public void Test_NotFound()
        {
            Assert.Equal(-1, BruteForceSearch.Search("gl", "Microsoft"));
        }
    }
}
