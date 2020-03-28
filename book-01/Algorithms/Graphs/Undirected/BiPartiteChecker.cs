using System;

namespace Algorithms.Graphs.Undirected
{
    public sealed class BiPartiteChecker
    {
        public static Boolean IsBiPartite(Graph graph)
        {
            Boolean[] visited = new Boolean[graph.VerticesCount];
            Boolean[] color = new Boolean[graph.VerticesCount];
            Boolean isBiPartite = true;

            for (int i = 0; i < graph.VerticesCount; i++)
            {
                if (!visited[i])
                {
                    if (!IsBiPartite(i))
                        break;
                }
            }

            return isBiPartite;

            Boolean IsBiPartite(Int32 vertice)
            {
                visited[vertice] = true;

                using (var adjacents = graph.GetAdjacentVertices(vertice).GetEnumerator())
                {
                    while (isBiPartite && adjacents.MoveNext())
                    {
                        // If one of my adjacents was visited and his color is equal to mine
                        // it means I'm pointing to a vertice that is on the same side as me.
                        //
                        // 0 ------> 2
                        //           ^
                        //           |
                        //           |
                        // 0 ------> 1


                        if (!visited[adjacents.Current])
                        {
                            color[adjacents.Current] = !color[vertice];

                            IsBiPartite(adjacents.Current);
                        }
                        else if (color[adjacents.Current] == color[vertice])
                        {
                            isBiPartite = false;
                        }
                    }
                }

                return isBiPartite;
            }
        }
    }
}
