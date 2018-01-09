using System;
using Algorithms.Strings;
using Xunit;

namespace Algorithms.Test.Structures.Strings
{
    public sealed class AlphabetTest
    {
        [Fact]
        public void Test_Radix()
        {
            Alphabet alphabet = Alphabet.Binary;

            Assert.Equal(2, alphabet.Radix);
        }

        [Fact]
        public void Test_BinaryLogRadix()
        {
            Alphabet alphabet = Alphabet.Decimal;

            Assert.Equal(4, alphabet.BinaryLogRadix);
        }

        [Fact]
        public void Test_Contains()
        {
            Alphabet alphabet = Alphabet.Decimal;

            Assert.True(alphabet.Contains('9'));
        }

        [Fact]
        public void Test_NotContains()
        {
            Alphabet alphabet = Alphabet.Decimal;

            Assert.False(alphabet.Contains('A'));
        }

        [Fact]
        public void Test_ToIndex()
        {
            Alphabet alphabet = Alphabet.Decimal;

            Assert.Equal(9, alphabet.ToIndex('9'));
        }

        [Fact]
        public void Test_ToIndices()
        {
            Alphabet alphabet = Alphabet.Decimal;

            AssertUtilities.Sequence(new Int32[3] { 7, 8, 9 }, alphabet.ToIndices("789"));
        }

        [Fact]
        public void Test_ToChar()
        {
            Alphabet alphabet = Alphabet.Decimal;

            Assert.Equal('9', alphabet.ToChar(9));
        }

        [Fact]
        public void Test_ToChars()
        {
            Alphabet alphabet = Alphabet.Decimal;

            AssertUtilities.Sequence(new Char[3] { '7', '8', '9' }, alphabet.ToChars(new Int32[3] { 7, 8, 9 }));
        }

    }
}
