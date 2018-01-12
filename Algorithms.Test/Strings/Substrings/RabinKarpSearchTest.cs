using Algorithms.Strings;
using Algorithms.Strings.Substrings;
using Xunit;

namespace Algorithms.Test.Strings.Substrings
{
    public sealed class RabinKarpSearchTestTest
    {
        [Fact]
        public void Test_Found()
        {
            RabinKarpSearch bm = new RabinKarpSearch("cro", Alphabet.ASCII);

            Assert.Equal(2, bm.Search("Microsoft"));
        }

        [Fact]
        public void Test_NotFound()
        {
            RabinKarpSearch bm = new RabinKarpSearch("gl", Alphabet.ASCII);

            Assert.Equal(-1, bm.Search("Microsoft"));
        }
    }
}
