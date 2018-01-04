using System;
using System.Collections.Generic;
using Algorithms.Searching;

namespace Algorithms.Graphs.Undirected
{
    public sealed class SymbolGraph
    {
        private readonly ISymbolTable<String, Int32> _symboTable; // String to index.
        private readonly String[] _keys; // Index to string = inverted index.

        public Graph Graph { get; }

        public static SymbolGraph Create(IEnumerable<KeyValuePair<String, String>> edges)
        {
            return new SymbolGraph(edges);
        }

        private SymbolGraph(IEnumerable<KeyValuePair<String,String>> edges)
        {
            this._symboTable = new SeparateChainHash<String, Int32>();

            using (var enumerator = edges.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    // Building the symbol table with Key (source) and Value (destination)
                    // using the Count as index. It gets incremented, so we have an index.

                    if (!this._symboTable.Contains(enumerator.Current.Key))
                        this._symboTable.Add(enumerator.Current.Key, this._symboTable.Count);

                    if (!this._symboTable.Contains(enumerator.Current.Value))
                        this._symboTable.Add(enumerator.Current.Value, this._symboTable.Count);
                }
            }

            this._keys = new String[this._symboTable.Count];

            // For each key on the symbol table we get its index and set on the 
            // inverted index its value.

            using (var keys = this._symboTable.Keys().GetEnumerator())
            {
                while(keys.MoveNext())
                    this._keys[this._symboTable.Get(keys.Current)] = keys.Current;
            }

            this.Graph = new Graph(this._symboTable.Count);

            using (var enumerator = edges.GetEnumerator())
            {
                // For each edge on input we create an edge on the graph
                // using the index of it value as vertice index.

                while (enumerator.MoveNext())
                {
                    Int32 vertice = this._symboTable.Get(enumerator.Current.Key);

                    this.Graph.AddEdge(vertice, this._symboTable.Get(enumerator.Current.Value));
                }
            }
        }

        public Boolean Contains(String value) => this._symboTable.Contains(value);

        public Int32 Index(String value) => this._symboTable.Get(value);

        public String Name(Int32 index) => this._keys[index];
    }
}
