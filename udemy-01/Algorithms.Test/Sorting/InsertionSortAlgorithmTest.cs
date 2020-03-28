using Algorithms.Sorting;

namespace Algorithms.Test.Sorting
{
    public class InsertionSortAlgorithmTest : BaseSortAlgorithmTest
    {
        protected override ISortAlgorithm CreateInstance() => InsertionSortAlgorithm.Instance;
    }
}
