using Algorithms.Sorting;

namespace Algorithms.Test.Sorting
{
    public class QuickSortAlgorithmTest : BaseSortAlgorithmTest
    {
        protected override ISortAlgorithm CreateInstance() => QuickSortAlgorithm.Instance;
    }
}
