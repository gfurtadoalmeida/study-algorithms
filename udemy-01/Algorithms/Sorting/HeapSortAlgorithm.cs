using DataStructures.Heap;

namespace Algorithms.Sorting
{
    public class HeapSortAlgorithm : SortAlgorithm
    {
        public static readonly ISortAlgorithm Instance = new HeapSortAlgorithm();

        protected override void OnSort<T>(ref T[] array, int arrayLength)
        {
            BinaryHeap<T> binaryHeap = new BinaryHeap<T>(HeapMode.MinHeap, arrayLength);

            for (int i = 0; i < arrayLength; i++)
            {
                binaryHeap.Add(array[i]);
            }

            for (int i = 0; i < arrayLength; i++)
            {
                array[i] = binaryHeap.ExtractTop();
            }
        }
    }
}
