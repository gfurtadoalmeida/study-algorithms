using System;

namespace Algorithms.Graphs.Directed
{
    /// <summary>
    /// Finds how many strongly connected compopnentes are in a digraph.
    /// </summary>
    public sealed class KosarajuSharirSCC
    {
        private Boolean[] _visited;
        private Int32[] _ids;

        /// <summary>
        /// How many componentes.
        /// Being a component an isolated web of strongly connected vertices or a sole vertice.
        /// </summary>
        public Int32 Count { get; private set; }

        public static KosarajuSharirSCC  Create(Digraph digraph)
        {
            return new KosarajuSharirSCC(digraph);
        }

        private KosarajuSharirSCC(Digraph digraph)
        {
            DepthFirstOrder dfs = new DepthFirstOrder(digraph.Reverse());

            this._visited = new Boolean[digraph.VerticesCount];
            this._ids = new Int32[digraph.VerticesCount];

            foreach (Int32 vertice in dfs.ReversePost())
            {
                if (!this._visited[vertice])
                {
                    this.DFS(digraph, vertice);

                    this.Count++;
                }
            }
        }

        public Boolean IsStronglyConnected(Int32 sourceVertice, Int32 targetVertice)
        {
            return this._ids[sourceVertice] == this._ids[targetVertice];
        }

        public Int32 Id(Int32 vertice)
        {
            return this._ids[vertice];
        }

        private void DFS(Digraph digraph, Int32 vertice)
        {
            this._visited[vertice] = true;
            this._ids[vertice] = this.Count;

            foreach (Int32 adjacent in digraph.GetAdjacentVertices(vertice))
            {
                if (!this._visited[adjacent])
                    this.DFS(digraph, adjacent);
            }
        }
    }
}