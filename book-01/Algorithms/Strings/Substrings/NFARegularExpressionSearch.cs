using System;
using Algorithms.Graphs.Directed;
using AST = DataStructures;

namespace Algorithms.Strings.Substrings
{
    public sealed class NFARegularExpressionSearch
    {
        private readonly Digraph _graph; // Digraph of transitions.
        private readonly String _expression;
        private readonly Int32 _expressionSize;

        public NFARegularExpressionSearch(String expression)
        {
            this._expression = expression;
            this._expressionSize = expression.Length;
            this._graph = new Digraph(this._expressionSize + 1);

            AST.Stack<Int32> operations = new AST.Stack<Int32>();

            for (int i = 0; i < _expressionSize; i++)
            {
                int lp = i;

                if (expression[i] == '(' || expression[i] == '|')
                {
                    operations.Push(i);
                }
                else if (expression[i] == ')')
                {
                    Int32 or = operations.Pop();

                    // 2-way OR operator.
                    if (expression[or] == '|')
                    {
                        lp = operations.Pop();

                        this._graph.AddEdge(lp, or + 1);
                        this._graph.AddEdge(or, i);
                    }
                    else if (expression[or] == '(')
                    {
                        lp = or;
                    }
                }

                // Closure operator (uses 1-character lookahead).
                if (i < this._expressionSize - 1 && expression[i + 1] == '*')
                {
                    this._graph.AddEdge(lp, i + 1);
                    this._graph.AddEdge(i + 1, lp);
                }

                if (expression[i] == '(' || expression[i] == '*' || expression[i] == ')')
                {
                    this._graph.AddEdge(i, i + 1);
                }
            }

            if (operations.Count != 0)
                throw new ArgumentException("Invalid regular expression.", nameof(expression));
        }

        public Boolean Match(String text)
        {
            DirectedDepthFirstSearch dfs = new DirectedDepthFirstSearch(this._graph, 0);
            AST.Bag<Int32> pc = new AST.Bag<Int32>();

            for (int i = 0; i < this._graph.VerticesCount; i++)
            {
                if (dfs.HasDirectPathTo(i))
                {
                    pc.Add(i);
                }
            }

            // Compute possible NFA states for text[i+1]
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '*' || text[i] == '|' || text[i] == '(' || text[i] == ')')
                    throw new ArgumentException("Text contains the metacharacter '" + text[i] + "'", nameof(text));

                AST.Bag<Int32> match = new AST.Bag<Int32>();

                foreach (Int32 vertice in pc)
                {
                    if (vertice == this._expressionSize)
                        continue;

                    if ((this._expression[vertice] == text[i]) || this._expression[vertice] == '.')
                    {
                        match.Add(vertice + 1);
                    }
                }

                dfs = new DirectedDepthFirstSearch(this._graph, match);
                pc = new AST.Bag<Int32>();

                for (int v = 0; v < this._graph.VerticesCount; v++)
                {
                    if (dfs.HasDirectPathTo(v))
                    {
                        pc.Add(v);
                    }
                }

                // Optimization if no states reachable.
                if (pc.Count == 0)
                    return false;
            }

            foreach (Int32 vertice in pc)
            {
                if (vertice == this._expressionSize)
                    return true;
            }

            return false;
        }
    }
}
