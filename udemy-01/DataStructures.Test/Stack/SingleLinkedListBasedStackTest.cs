using DataStructures.Stack;

namespace DataStructures.Test.Stack
{
    public class SingleLinkedListBasedStackTest : BaseStackTest
    {
        protected override IStack<byte> CreateInstance(int initialSize)
        {
            return new SingleLinkedListBasedStack<byte>();
        }
    }
}
