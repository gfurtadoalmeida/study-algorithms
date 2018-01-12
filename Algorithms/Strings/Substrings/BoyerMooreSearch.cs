using System;

namespace Algorithms.Strings.Substrings
{
    /// <summary>
    /// Use: low self-repetitive patterns in low self-repetitive text.
    /// Cost: ~N/M character compares to search for a pattern of length M in a text of length N.
    /// Very efficient.
    /// </summary>
    public sealed class BoyerMooreSearch
    {
        // _right gives, for each character in the alphabet, the index of its 
        // rightmost occurrence in the pattern (or -1 if the character is not in the pattern).
        // It tells us how far to skip if that character appears in the text and causes a 
        // mismatch during the string search.
        // 
        // Example:
        // Pattern: BOOLEAN
        // Alphabet: A = 0, B = 1, C = 2 ...
        //
        // _right:
        // A [0]  =  5
        // B [1]  =  0
        // C [2]  = -1
        // D [3]  = -1 
        // E [4]  =  4
        // ...
        // L [11] =  3
        // M [12] = -1
        // N [13] =  6
        // O [14] =  2

        private readonly Int32[] _right;
        private readonly String _pattern;
        private readonly Alphabet _alphabet;

        public BoyerMooreSearch(String pattern, Alphabet alphabet)
        {
            this._alphabet = alphabet;
            this._pattern = pattern;
            this._right = new Int32[alphabet.Radix];

            Int32 patternSize = pattern.Length;

            for (int c = 0; c < alphabet.Radix; c++)
                this._right[c] = -1; // -1 for chars not in pattern.

            // Computes the rightmost position for chars in pattern.
            for (int j = 0; j < patternSize; j++)
                this._right[alphabet.ToIndex(pattern[j])] = j;
        }

        public Int32 Search(String text)
        {
            //Scans the text left to right while scanning the pattern right to left.

            Int32 textSize = text.Length;
            Int32 patternSize = this._pattern.Length;
            Int32 skip;

            for (int i = 0; i <= textSize - patternSize; i += skip)
            {
                // Controls how many chars to skip when reading the text.
                skip = 0;

                for (int j = patternSize - 1; j >= 0; j--)
                {
                    // Compares by aligning the rightmost char at pattern[j] with the text.
                    if (this._pattern[j] != text[i + j])
                    {
                        // Pattern: BOOLEAN
                        // Text: BAOOBABOOLEAN
                        //
                        // For: i = 3 | j = 6 | i+j = 9
                        //
                        //    i
                        // BAOOBABOOLEAN
                        //          ^
                        //    BOOLEAN
                        //          j
                        //
                        // skip = 6 (J) - 3 (L) = 3
                        // skip = 3
                        //
                        // On the next loop we'll have the letters aligned.
                        //
                        //       i
                        // BAOOBABOOLEAN
                        //             ^
                        //       BOOLEAN
                        //             j
                        
                        skip = j - this._right[this._alphabet.ToIndex(text[i + j])];

                        if (skip < 1)
                            skip = 1;

                        break;
                    }
                }

                if (skip == 0)
                    return i; // Found.
            }

            return -1;
        }
    }
}