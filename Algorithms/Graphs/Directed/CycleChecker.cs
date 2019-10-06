using System;
using System.Collections.Generic;
using AST = Algorithms.Structures;

namespace Algorithms.Graphs.Directed
{
    public sealed class CycleChecker
    {
        // Mark if the vertice was visited.
        private readonly Boolean[] _visited;
        // Vertices on the stack of "vertices being processed".
        private readonly Boolean[] _onStack;
        // Parent vertice of "_edgeto[vertice]".
        private readonly Int32[] _edgeTo;

        // Vertices on a cycle, from the end to start.
        private AST.Stack<Int32> _cycle;

        public Boolean HasCycle => this._cycle != null;

        public static CycleChecker Create(Digraph digraph)
        {
            if (digraph == null)
                throw new ArgumentNullException(nameof(digraph));

            return new CycleChecker(digraph);
        }

        private CycleChecker(Digraph digraph)
        {
            if (digraph == null)
                throw new ArgumentNullException(nameof(digraph));

            this._visited = new Boolean[digraph.VerticesCount];
            this._onStack = new Boolean[digraph.VerticesCount];
            this._edgeTo = new Int32[digraph.VerticesCount];

            for (int i = 0; i < digraph.VerticesCount; i++)
            {
                if (!this._visited[i])
                {
                    this.DFS(digraph, i);
                }
            }
        }

        public IEnumerable<Int32> GetCycle()
        {
            return this._cycle;
        }

        private void DFS(Digraph digraph, Int32 vertice)
        {
            this._onStack[vertice] = true;
            this._visited[vertice] = true;

            foreach (Int32 adjacent in digraph.GetAdjacentVertices(vertice))
            {
                if (this.HasCycle)
                    return;

                // The cycle occurrs when an adjacent vertice is on the stack of 
                // "vertices being processed".
                // If vertice A is being processed (onStack) and one of its adjacents
                // points to A again, we have a cycle.

                if (!this._visited[adjacent])
                {
                    this._edgeTo[adjacent] = vertice;

                    this.DFS(digraph, adjacent);
                }
                else if (this._onStack[adjacent])
                {
                    this._cycle = new AST.Stack<Int32>();

                    for (int i = vertice; i != adjacent; i = this._edgeTo[i])
                    {
                        this._cycle.Push(i);
                    }

                    this._cycle.Push(adjacent);
                    this._cycle.Push(vertice);
                }
            }
            
            // If we fully processed a vertice, it means the vertice is no more on
            // the stack.

            this._onStack[vertice] = false;
        }
    }
}