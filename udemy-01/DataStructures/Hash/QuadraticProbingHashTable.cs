using System.Runtime.CompilerServices;
using DataStructures.Hash.Functions;

namespace DataStructures.Hash
{
    public class QuadraticProbingHashTable<T> : OpenAddressingHashTable<T> where T : class
    {
        public QuadraticProbingHashTable(int size, IHashFunction hashFunction) : base(size, hashFunction)
        {
        }

        protected override int FindIndexFor(T value, T[] array)
        {
            int hash = this.HashFunction.Hash(value.ToString());
            int attempt = 0;
            int index = CalculateIndex(hash, attempt, array.Length);
            T valueInCell;

            while (index < array.Length)
            {
                valueInCell = array[index];

                if (valueInCell == null || valueInCell == value)
                {
                    break;
                }

                attempt++;

                index = CalculateIndex(hash, attempt, array.Length);
            }

            return index < array.Length ? index : -1;
        }

        protected override int FindIndexOf(T value, T[] array)
        {
            int hash = this.HashFunction.Hash(value.ToString());
            int attempt = 0;
            int index = CalculateIndex(hash, attempt, array.Length);
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

                index = CalculateIndex(hash, attempt, array.Length);
            }

            return index < array.Length ? index : -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CalculateIndex(int hashValue, int attempt, int hashTableSize)
        {
            // Must always return positive.
            return ((hashValue + (attempt * attempt)) % hashTableSize) & 0x7FFFFFFF;
        }
    }
}
