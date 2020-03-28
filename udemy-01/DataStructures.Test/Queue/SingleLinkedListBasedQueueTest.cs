using DataStructures.Queue;

namespace DataStructures.Test.Queue
{
    public class SingleLinkedListBasedQueueTest : BaseQueueTest
    {
        protected override IQueue<byte> CreateInstance(int initialSize)
        {
            return new SingleLinkedListBasedQueue<byte>();
        }
    }
}
