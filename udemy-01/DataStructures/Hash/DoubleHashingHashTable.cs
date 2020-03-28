using System.Runtime.CompilerServices;
using DataStructures.Hash.Functions;

namespace DataStructures.Hash
{
    public class DoubleHashingHashTable<T> : OpenAddressingHashTable<T> where T : class
    {
        public DoubleHashingHashTable(int size, IHashFunction hashFunction) : base(size, hashFunction)
        {
        }

        protected override int FindIndexFor(T value, T[] array)
        {
            int firstHash = this.HashFunction.Hash(value.ToString());
            int secondHash = DoSecondHash(firstHash);
            int attempt = 0;
            int index = CalculateIndex(firstHash, secondHash, attempt, array.Length);
            T valueInCell;

            while (index < array.Length)
            {
                valueInCell = array[index];

                if (valueInCell == null || valueInCell == value)
                {
                    break;
                }

                attempt++;

                index = CalculateIndex(firstHash, secondHash, attempt, array.Length);
            }

            return index < array.Length ? index : -1;
        }

        protected override int FindIndexOf(T value, T[] array)
        {
            int firstHash = this.HashFunction.Hash(value.ToString());
            int secondHash = DoSecondHash(firstHash);
            int attempt = 0;
            int index = CalculateIndex(firstHash, secondHash, attempt, array.Length);
            T valueInCell;

            while (index < array.Length)
            {
                valueInCell = array[index];

                if (valueInCell == value)
                {
                    break;
                }

                if (valueInCell == null)
                {
                    index = -1;

                    break;
                }

                attempt++;

                index = CalculateIndex(firstHash, secondHash, attempt, array.Length);
            }

            return index < array.Length ? index : -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int DoSecondHash(int firstHashValue)
        {
            // Prime must be less than tha hastable size.
            const int PRIME = 3;

            return PRIME - (firstHashValue % PRIME);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CalculateIndex(int firstHashValue, int secondHashValue, int attempt, int hashTableSize)
        {
            // Must always return positive.
            return ((firstHashValue + (attempt * secondHashValue)) % hashTableSize) & 0x7FFFFFFF;
        }
    }
}
