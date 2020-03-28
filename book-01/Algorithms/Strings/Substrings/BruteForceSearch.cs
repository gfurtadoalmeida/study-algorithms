using System;

namespace Algorithms.Strings.Substrings
{
    /// <summary>
    /// Use: low self-repetitive patterns in low self-repetitive text.
    /// Cost: ~NM character compares to search for a pattern of length M in 
    /// a text of length N, in the worst case.
    /// </summary>
    public static class BruteForceSearch
    {
        public static Int32 Search(String pattern, String text)
        {
            Int32 patternSize = pattern.Length;
            Int32 textSize = text.Length;
            
            // From a character position CP in text check if
            // all characters from CP to (CP + patternSize) are in
            // pattern.

            for (int i = 0; i <= textSize - patternSize; i++)
            {
                Int32 j;

                for (j = 0; j < patternSize; j++)
                {
                    if (text[i + j] != pattern[j])
                        break;
                }

                if (j == patternSize)
                    return i;  // Found.
            }

            return -1;
        }
    }
}
