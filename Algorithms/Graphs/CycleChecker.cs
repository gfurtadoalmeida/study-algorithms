using System;

namespace Algorithms.Graphs
{
    public sealed class CycleChecker
    {
        public static Boolean HasCycle(Graph graph)
        {
            Boolean[] marked = new Boolean[graph.VerticesCount];
            Boolean hasCycle = false;

            for (int i = 0; i < graph.VerticesCount; i++)
                if (!marked[i])
                    if (CheckCycle(i, i))
                        break;

            return hasCycle;

            Boolean CheckCycle(Int32 vertice, Int32 parentVertice)
            {
                marked[vertice] = true;

                using (var adjacents = graph.GetAdjacentVertices(vertice))
                {
                    while (!hasCycle && adjacents.MoveNext())
                    {
                        if (adjacents.Current == vertice) // Self-loop: my adjacent is me.
                            hasCycle = true;
                        else if (!marked[adjacents.Current])
                            CheckCycle(adjacents.Current, vertice);
                        else if (adjacents.Current != parentVertice) // Back-edge: if it's visited and not my parent so it looped to a vertice.
                            hasCycle = true;
                    }
                }

                return hasCycle;
            }
        }
    }
}
