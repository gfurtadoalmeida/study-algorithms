using DataStructures.Hash;
using DataStructures.Hash.Functions;

namespace DataStructures.Test.Hash
{
    public class DoubleHashingHashTableTest : BaseOpenAddressingHashTableTest
    {
        protected override IHashTable<string> CreateInstance(int initialSize, IHashFunction hashFunction)
        {
            return new DoubleHashingHashTable<string>(initialSize, hashFunction);
        }
    }
}
