using System;
using System.Collections.Generic;
using Algorithms.Graphs.Directed;
using Xunit;

namespace Algorithms.Test.Graphs.Directed
{
    public sealed class CycleCheckerTest
    {
        [Fact]
        public void Test_NoCycle()
        {
            Digraph graph = new Digraph(3);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);

            CycleChecker checker = new CycleChecker(graph);

            Assert.False(checker.HasCycle);
        }

        [Fact]
        public void Test_HasCycle()
        {
            Digraph graph = new Digraph(3);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);

            CycleChecker checker = new CycleChecker(graph);

            Assert.True(checker.HasCycle);
        }

        [Fact]
        public void Test_GetCycle()
        {
            Digraph graph = new Digraph(3);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);

            CycleChecker checker = new CycleChecker(graph);

            using (IEnumerator<Int32> enumerator = checker.GetCycle().GetEnumerator())
            {
                enumerator.MoveNext();

                Assert.Equal(2, enumerator.Current);

                enumerator.MoveNext();

                Assert.Equal(0, enumerator.Current);

                enumerator.MoveNext();

                Assert.Equal(1, enumerator.Current);

                enumerator.MoveNext();

                Assert.Equal(2, enumerator.Current);

                Assert.False(enumerator.MoveNext());
            }
        }
    }
}
