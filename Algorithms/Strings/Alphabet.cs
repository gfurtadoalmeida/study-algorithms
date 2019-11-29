using System;
using System.Text;
using Algorithms.Structures;

namespace Algorithms.Strings
{
    public sealed class Alphabet
    {
        /// <summary>
        /// Alphabet using characters 0 through 255.
        /// </summary>
        public static readonly Alphabet Default = new Alphabet(256);
        public static readonly Alphabet Binary = new Alphabet("01");
        public static readonly Alphabet Octal = new Alphabet("01234567");
        public static readonly Alphabet Decimal = new Alphabet("0123456789");
        public static readonly Alphabet Hexadecimal = new Alphabet("0123456789ABCDEF");
        public static readonly Alphabet Lowercase = new Alphabet("abcdefghijklmnopqrstuvwxyz");
        public static readonly Alphabet Uppercase = new Alphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        public static readonly Alphabet DNA = new Alphabet("ACGT");
        public static readonly Alphabet Protein = new Alphabet("ACDEFGHIKLMNPQRSTVWY");
        public static readonly Alphabet Base64 = new Alphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/");
        public static readonly Alphabet ASCII = new Alphabet(128);
        public static readonly Alphabet ExtendeASCII = new Alphabet(256);
        public static readonly Alphabet Unicode = new Alphabet(65536);

        private readonly Char[] _alphabet;
        private readonly AlphabetCompactIndex _index;

        /// <summary>
        /// Number of unique chars.
        /// </summary>
        public Int32 Radix { get; }

        public Int32 BinaryLogRadix { get; }

        public Char MinChar { get; }

        public Char MaxChar { get; }

        public Alphabet(String alphabet)
        {
            this._alphabet = alphabet.ToCharArray();

            (Int32 minValue, Int32 maxValue) = this.CalculateBounds(this._alphabet);

            {
                (Boolean containsDuplicate, Char duplicatedChar) = this.ContainsDuplicate(this._alphabet, minValue, maxValue);

                if (containsDuplicate)
                    throw new InvalidOperationException("Illegal alphabet: repeated character = '" + duplicatedChar + "'");
            }

            this._index = new AlphabetCompactIndex(minValue, maxValue);

            for (int i = 0; i < this._index.Length; i++)
            {
                this._index[i] = -1;
            }

            for (int i = 0; i < this._alphabet.Length; i++)
            {
                this._index[this._alphabet[i]] = i;
            }

            this.Radix = alphabet.Length;
            this.BinaryLogRadix = this.CalculateBinaryLogRadix(alphabet.Length);
            this.MinChar = (Char)minValue;
            this.MaxChar = (Char)maxValue;
        }

        public Alphabet(Int32 radix)
        {
            this._alphabet = new Char[radix];
            this._index = new AlphabetCompactIndex(0, radix - 1);

            for (int i = 0; i < radix; i++)
            {
                this._alphabet[i] = (Char)i;
                this._index[i] = i;
            }

            this.Radix = radix;
            this.BinaryLogRadix = this.CalculateBinaryLogRadix(radix);
            this.MinChar = (Char)0;
            this.MaxChar = (Char)(radix - 1);
        }

        public Boolean Contains(Char c)
        {
            return this._index.InRange(c) && this._index[c] != -1;
        }

        public Int32 ToIndex(Char c)
        {
            if (!this._index.InRange(c) || this._index[c] == -1)
                throw new Exception("Character " + c + " not in alphabet");

            return this._index[c];
        }

        public Int32[] ToIndices(String s)
        {
            Char[] source = s.ToCharArray();
            Int32[] target = new Int32[s.Length];

            for (int i = 0; i < source.Length; i++)
            {
                target[i] = this.ToIndex(source[i]);
            }

            return target;
        }

        public Char ToChar(Int32 index)
        {
            if (index < 0 || index >= this.Radix)
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be between 0 and " + this.Radix + ": " + index);

            return this._alphabet[index];
        }

        public String ToChars(Int32[] indices)
        {
            StringBuilder s = new StringBuilder(indices.Length);

            for (int i = 0; i < indices.Length; i++)
            {
                s.Append(this.ToChar(indices[i]));
            }

            return s.ToString();
        }

        //Binary logarithm (rounded up) of the number of characters in this alphabet.
        private Int32 CalculateBinaryLogRadix(Int32 radix)
        {
            Int32 lgR = 0;

            for (int i = radix - 1; i >= 1; i /= 2)
            {
                lgR++;
            }

            return lgR;
        }

        private (Int32 minValue, Int32 maxValue) CalculateBounds(Char[] array)
        {
            Int32 minValue = array[0];
            Int32 maxValue = array[^1];

            foreach (Char c in array)
            {
                if (c < minValue)
                {
                    minValue = c;
                }
                else if (c > maxValue)
                {
                    maxValue = c;
                }
            }

            return (minValue, maxValue);
        }

        private (Boolean containsDuplicate, Char duplicatedChar) ContainsDuplicate(Char[] array, Int32 minValue, Int32 maxValue)
        {
            CompactBoundedArray<Boolean> processed = new CompactBoundedArray<Boolean>(minValue, maxValue);
            Boolean containsDuplicate = false;
            Char duplicatedChar = Char.MinValue;

            for (int i = 0; i < array.Length; i++)
            {
                Char c = array[i];

                if (processed[c])
                {
                    containsDuplicate = true;
                    duplicatedChar = c;

                    break;
                }

                processed[c] = true;
            }

            return (containsDuplicate, duplicatedChar);
        }

        private struct AlphabetCompactIndex
        {
            private readonly Int32[] _indexes;
            private readonly Int32 _lowerBound;
            private readonly Int32 _upperBound;

            public Int32 Length { get; }

            public AlphabetCompactIndex(Int32 lowerBound, Int32 upperBound)
            {
                this._indexes = new Int32[upperBound - lowerBound + 1];
                this._lowerBound = lowerBound;
                this._upperBound = upperBound;

                this.Length = this._indexes.Length - 1;
            }

            public Int32 this[Int32 index]
            {
                get => this._indexes[index];
                set => this._indexes[index] = value;
            }

            public Int32 this[Char c]
            {
                get => this._indexes[c - this._lowerBound];
                set => this._indexes[c - this._lowerBound] = value;
            }

            public Boolean InRange(Char c)
            {
                return c >= this._lowerBound && c <= this._upperBound;
            }
        }
    }
}
