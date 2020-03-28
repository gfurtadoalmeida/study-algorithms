using DataStructures.Hash;
using DataStructures.Hash.Functions;

namespace DataStructures.Test.Hash
{
    public class QuadraticProbingHashTableTest : BaseOpenAddressingHashTableTest
    {
        protected override IHashTable<string> CreateInstance(int initialSize, IHashFunction hashFunction)
        {
            return new QuadraticProbingHashTable<string>(initialSize, hashFunction);
        }
    }
}
