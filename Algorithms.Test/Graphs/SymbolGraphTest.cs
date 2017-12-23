using System;
using System.Collections.Generic;
using Algorithms.Graphs;
using Xunit;

namespace Algorithms.Test.Graphs
{
    public sealed class SymbolGraphTest
    {
        private readonly SymbolGraph _symbolGraph;

        public SymbolGraphTest()
        {
            KeyValuePair<String, String>[] graph = new KeyValuePair<String, String>[5]
            {
                new KeyValuePair<String, String>("MG","SP"),
                new KeyValuePair<String, String>("MG","RJ"),
                new KeyValuePair<String, String>("MG","ES"),
                new KeyValuePair<String, String>("SP","RS"),
                new KeyValuePair<String, String>("RJ","RS")
            };

            this._symbolGraph = SymbolGraph.Create(graph);
        }

        [Fact]
        public void Test_Contains()
        {
            Assert.True(this._symbolGraph.Contains("RJ"));
        }

        [Fact]
        public void Test_NotContains()
        {
            Assert.False(this._symbolGraph.Contains("AM"));
        }

        [Fact]
        public void Test_Index()
        {
            Assert.Equal(3, this._symbolGraph.Index("ES"));
        }

        [Fact]
        public void Test_Name()
        {
            Assert.Equal("RS", this._symbolGraph.Name(4));
        }
    }
}
