using Algorithms.Strings.Substrings;
using Xunit;

namespace Algorithms.Test.Strings.Substrings
{
    public sealed class NFARegularExpressionSearchTest
    {
        [Fact]
        public void Test_Found()
        {
            NFARegularExpressionSearch nfa = new NFARegularExpressionSearch("(1000((10)*)1000)");

            Assert.True(nfa.Match("10001010101000"));
        }

        [Fact]
        public void Test_NotFound()
        {
            NFARegularExpressionSearch nfa = new NFARegularExpressionSearch("(1000((10)*))");

            Assert.False(nfa.Match("ASD"));
        }
    }
}
