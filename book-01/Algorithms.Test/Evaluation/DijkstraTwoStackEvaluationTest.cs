using Algorithms.Evaluation;
using Xunit;

namespace Algorithms.Test.Evaluation
{
    public sealed class DijkstraTwoStackEvaluationTest
    {
        [Fact]
        public void Test_Evaluation()
        {
            Assert.Equal(-1, DijkstraTwoStackEvaluation.Evaluate("( ( ( 1 + sqrt ( 49 ) ) / ( 2 * 1 ) ) - 5 )"));
        }
    }
}
