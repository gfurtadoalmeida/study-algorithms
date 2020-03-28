using Algorithms.Strings;
using Algorithms.Strings.Substrings;
using Xunit;

namespace Algorithms.Test.Strings.Substrings
{
    public sealed class KnuthMorrisPrattSearchTest
    {
        [Fact]
        public void Test_Found()
        {
            KnuthMorrisPrattSearch kmp = new KnuthMorrisPrattSearch("ABA", Alphabet.ASCII);

            Assert.Equal(10, kmp.Search("AAAABBAAAAABAAAA"));
        }

        [Fact]
        public void Test_NotFound()
        {
            KnuthMorrisPrattSearch kmp = new KnuthMorrisPrattSearch("BAB", Alphabet.ASCII);

            Assert.Equal(-1, kmp.Search("AAAABBAAAAABAAAA"));
        }
    }
}
