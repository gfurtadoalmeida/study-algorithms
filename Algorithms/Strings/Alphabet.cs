using System;
using System.Text;

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

        public Int32 BinaryLogRadix { get; }

        /// <summary>
        /// Number of unique chars.
        /// </summary>
        public Int32 Radix { get; }

        public Alphabet(String alphabet)
        {
            // Check that alphabet contains no duplicate chars.
            Boolean[] processed = new Boolean[Char.MaxValue];

            for (int i = 0; i < alphabet.Length; i++)
            {
                Char c = alphabet[i];

                if (processed[c])
                    throw new InvalidOperationException("Illegal alphabet: repeated character = '" + c + "'");

                processed[c] = true;
            }

            this._alphabet = alphabet.ToCharArray();
            this._index = new AlphabetCompactIndex(this._alphabet);

            this.Radix = alphabet.Length;

            for (int i = 0; i < this._index.Length; i++)
                this._index[i] = -1;

            for (int i = 0; i < this.Radix; i++)
                this._index[this._alphabet[i]] = i;

            this.BinaryLogRadix = this.CalculateBinaryLogRadix();
        }

        private Alphabet(Int32 radix)
        {
            this.Radix = radix;

            this._alphabet = new Char[this.Radix];
            this._index = new AlphabetCompactIndex(this.Radix);

            for (int i = 0; i < this.Radix; i++)
            {
                this._alphabet[i] = (Char)i;
                this._index[i] = i;
            }

            this.BinaryLogRadix = this.CalculateBinaryLogRadix();
        }

        public Boolean Contains(Char c)
        {
            return this._index.InRange(c) && this._index[c] != -1;
        }

        //Binary logarithm (rounded up) of the number of characters in this alphabet.</returns>
        private Int32 CalculateBinaryLogRadix()
        {
            Int32 lgR = 0;

            for (int i = this.Radix - 1; i >= 1; i /= 2)
                lgR++;

            return lgR;
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
                target[i] = this.ToIndex(source[i]);

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
                s.Append(this.ToChar(indices[i]));

            return s.ToString();
        }

        private struct AlphabetCompactIndex
        {
            private readonly Int32[] _indexes;
            private readonly Int32 _minValue;
            private readonly Int32 _maxValue;

            public Int32 Length { get; }

            public AlphabetCompactIndex(Int32 radix)
            {
                this._indexes = new Int32[radix];

                this.Length = radix;
                this._minValue = 0;
                this._maxValue = radix;
            }

            public AlphabetCompactIndex(Char[] alphabet)
            {
                Int32 minValue = alphabet[0];
                Int32 maxValue = alphabet[alphabet.Length - 1];

                foreach (Char c in alphabet)
                {
                    if (c < minValue)
                        minValue = c;
                    else if (c > maxValue)
                        maxValue = c;
                }

                this._indexes = new Int32[maxValue - minValue + 1];
                this._minValue = minValue;
                this._maxValue = maxValue;

                this.Length = this._indexes.Length;
            }

            public Int32 this[Int32 index]
            {
                get => this._indexes[index];
                set => this._indexes[index] = value;
            }

            public Int32 this[Char c]
            {
                get => this._indexes[c - this._minValue];
                set => this._indexes[c - this._minValue] = value;
            }

            public Boolean InRange(Char c)
            {
                return c >= this._minValue && c <= this._maxValue;
            }
        }
    }
}
