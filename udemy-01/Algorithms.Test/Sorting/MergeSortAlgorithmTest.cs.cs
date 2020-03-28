using Algorithms.Sorting;

namespace Algorithms.Test.Sorting
{
    public class MergeSortAlgorithmTest : BaseSortAlgorithmTest
    {
        protected override ISortAlgorithm CreateInstance() => MergeSortAlgorithm.Instace;
    }
}
