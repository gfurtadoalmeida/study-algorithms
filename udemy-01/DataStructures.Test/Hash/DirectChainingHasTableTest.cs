using DataStructures.Hash;
using DataStructures.Hash.Functions;

namespace DataStructures.Test.Hash
{
    public class DirectChainingHasTableTest : BaseHashTableTest
    {
        protected override IHashTable<string> CreateInstance(int initialSize, IHashFunction hashFunction)
        {
            return new DirectChainingHashTable<string>(initialSize, hashFunction);
        }
    }
}
