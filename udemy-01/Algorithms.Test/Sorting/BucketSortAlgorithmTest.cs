using Algorithms.Sorting;

namespace Algorithms.Test.Sorting
{
    public class BucketSortAlgorithmTest : BaseSortAlgorithmTest
    {
        protected override ISortAlgorithm CreateInstance() => BucketSortAlgorithm.Instance;
    }
}
