using System.Runtime.CompilerServices;
using DataStructures.Hash.Functions;

namespace DataStructures.Hash
{
    public class LinearProbingHashTable<T> : OpenAddressingHashTable<T> where T : class
    {
        public LinearProbingHashTable(int size, IHashFunction hashFunction) : base(size, hashFunction)
        {
        }

        protected override int FindIndexFor(T value, T[] array)
        {
            int index = CalculateIndex(value.ToString(), this.HashFunction, array.Length);
            T valueInCell;

            for (; index < array.Length; index++)
            {
                valueInCell = array[index];

                if (valueInCell == null || valueInCell == value)
                {
                    break;
                }
            }

            return index < array.Length ? index : -1;
        }

        protected override int FindIndexOf(T value, T[] array)
        {
            int index = CalculateIndex(value.ToString(), this.HashFunction, array.Length);
            T valueInCell;

            for (; index < array.Length; index++)
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
            }

            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CalculateIndex(string value, IHashFunction hashFunction, int hashTableSize)
        {
            // Must always return positive.
            return (hashFunction.Hash(value.ToString()) % hashTableSize) & 0x7FFFFFFF;
        }
    }
}
