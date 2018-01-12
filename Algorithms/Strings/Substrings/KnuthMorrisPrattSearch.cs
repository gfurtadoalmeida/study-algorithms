using System;

namespace Algorithms.Strings.Substrings
{
    /// <summary>
    /// Use: highly self-repetitive patterns in highly self-repetitive text.
    /// Cost: Accesses no more than M + N characters to search for a pattern of length M in a text of length N.
    /// Example:
    /// Text: ABRAAAACADAAABRA
    /// Pattern: ABRA
    /// </summary>
    public sealed class KnuthMorrisPrattSearch
    {
        private readonly String _pattern;
        private readonly Alphabet _alphabet;

        // For every character c (taken from an alphabet), _dfa[c][j] is the pattern 
        // position to compare against the next text position after comparing c with _pattern[j].
        // DFA: deterministic finite-state automaton.
        private readonly Int32[][] _dfa;

        public KnuthMorrisPrattSearch(String pattern, Alphabet alphabet)
        {
            this._alphabet = alphabet;
            this._pattern = pattern;
            this._dfa = new Int32[alphabet.Radix][];

            Int32 patternSize = this._pattern.Length;

            for (int i = 0; i < alphabet.Radix; i++)
                this._dfa[i] = new Int32[patternSize];

            this._dfa[alphabet.ToIndex(this._pattern[0])][0] = 1;

            //  Pattern: BAABA | Size: 5
            //  Alphabet: A = 0, B = 1
            //  
            //  _dfa:
            //           B   A   A   B   A 
            //  A [0] = [0],[2],[3],[0],[5]
            //  B [1] = [1],[1],[1],[4],[1]

            for (int i = 0, j = 1; j < patternSize; j++)
            {
                Int32 indexCharPattern = alphabet.ToIndex(pattern[j]);

                for (int c = 0; c < alphabet.Radix; c++)
                    this._dfa[c][j] = this._dfa[c][i];  // Mismatch: move to the left.

                this._dfa[indexCharPattern][j] = j + 1; // Match: For a match transition, move to the right one position.

                i = this._dfa[indexCharPattern][i]; // Update restart state.
            }
        }

        public Int32 Search(String text)
        {
            Int32 i;
            Int32 j;
            Int32 textSize = text.Length;
            Int32 patternSize = this._pattern.Length;

            for (i = 0, j = 0; i < textSize && j < patternSize; i++)
                j = this._dfa[this._alphabet.ToIndex(text[i])][j];
            
            // Reached _dfa[c][j] so as the value == patternSize?
            if (j == patternSize)
                return i - patternSize; // Found.
            else
                return -1;
        }
    }
}
