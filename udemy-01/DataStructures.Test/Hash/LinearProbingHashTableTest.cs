using DataStructures.Hash;
using DataStructures.Hash.Functions;

namespace DataStructures.Test.Hash
{
    public class LinearProbingHashTableTest : BaseOpenAddressingHashTableTest
    {
        protected override IHashTable<string> CreateInstance(int initialSize, IHashFunction hashFunction)
        {
            return new LinearProbingHashTable<string>(initialSize, hashFunction);
        }
    }
}
