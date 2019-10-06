using System;

namespace Algorithms.Strings.Substrings
{
    /// <summary>
    /// I'm just a human. This one I just copied. Too much for me.
    /// The random prime number returns the same number.
    /// Does not use this class.
    /// </summary>
    public sealed class RabinKarpSearch
    {
        private readonly String _pattern;
        private readonly Int64 _patternHash;
        private readonly Int32 _patternLength;
        private readonly Int64 _largePrime; // Small enough to avoid Int64 overflow.
        private readonly Int32 _radix;
        private readonly Int64 _rm; // R^(M-1) % Q

        public RabinKarpSearch(String pattern, Alphabet alphabet)
        {
            this._pattern = pattern;
            this._radix = alphabet.Radix;
            this._patternLength = pattern.Length;
            this._largePrime = this.GetLongRandomPrime();

            // Precompute R^(m-1) % q for use in removing leading digit.
            this._rm = 1;

            for (int i = 1; i <= this._patternLength - 1; i++)
            {
                this._rm = (this._radix * this._rm) % this._largePrime;
            }

            this._patternHash = this.Hash(pattern, this._patternLength);
        }

        public Int32 Search(String text)
        {
            Int32 textSize = text.Length;

            if (textSize < this._patternLength)
                return -1;

            Int64 textHash = this.Hash(text, this._patternLength);

            // Check for match at offset 0
            if (this._patternHash == textHash && this.Check(text, 0))
                return 0;

            // Check for hash match; if hash match, check for exact match.
            for (int i = this._patternLength; i < textSize; i++)
            {
                // Remove leading digit, add trailing digit, check for match. 
                textHash = (textHash + this._largePrime - this._rm * text[i - this._patternLength] % this._largePrime) % this._largePrime;
                textHash = (textHash * this._radix + text[i]) % this._largePrime;

                // Match
                Int32 offset = i - this._patternLength + 1;

                if (this._patternHash == textHash && this.Check(text, offset))
                    return offset;
            }

            return -1;
        }

        private Int64 Hash(String key, Int32 m)
        {
            Int64 hash = 0;

            for (int i = 0; i < m; i++)
            {
                hash = (this._radix * hash + key[i]) % this._largePrime;
            }

            return hash;
        }

        private Boolean Check(String text, Int32 index)
        {
            // Las Vegas: does _pattern[] match text[i..i-m+1] ?

            for (int i = 0; i < this._patternLength; i++)
            {
                if (this._pattern[i] != text[index + i])
                    return false;
            }

            return true;
        }

        // If you know how to generate a random prime number in C#,
        // please show me.
        private Int32 GetLongRandomPrime()
        {
            return 982451653;
        }
    }
}
