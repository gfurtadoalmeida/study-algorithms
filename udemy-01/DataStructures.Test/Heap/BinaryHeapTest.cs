using DataStructures.Heap;

namespace DataStructures.Test.Heap
{
    public class BinaryHeapTest : BaseBinaryHeapTest
    {
        protected override IHeap<byte> CreateInstance(HeapMode mode, int initialSize)
        {
            return new BinaryHeap<byte>(mode, initialSize);
        }
    }
}
