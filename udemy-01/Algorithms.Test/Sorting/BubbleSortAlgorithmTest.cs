using Algorithms.Sorting;

namespace Algorithms.Test.Sorting
{
    public class BubbleSortAlgorithmTest : BaseSortAlgorithmTest
    {
        protected override ISortAlgorithm CreateInstance() => BubbleSortAlgorithm.Instance;
    }
}
