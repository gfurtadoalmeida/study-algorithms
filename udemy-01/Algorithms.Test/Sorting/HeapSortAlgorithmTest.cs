using Algorithms.Sorting;

namespace Algorithms.Test.Sorting
{
    public class HeapSortAlgorithmTest : BaseSortAlgorithmTest
    {
        protected override ISortAlgorithm CreateInstance() => new HeapSortAlgorithm();
    }
}
