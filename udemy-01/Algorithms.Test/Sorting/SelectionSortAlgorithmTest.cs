using Algorithms.Sorting;

namespace Algorithms.Test.Sorting
{
    public class SelectionSortAlgorithmTest : BaseSortAlgorithmTest
    {
        protected override ISortAlgorithm CreateInstance() => SelectionSortAlgorithm.Instance;
    }
}
