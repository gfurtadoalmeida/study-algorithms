using System;

namespace Algorithms.Graphs.Directed.EdgeWeighted
{
    public sealed class Edge : IComparable<Edge>, IEquatable<Edge>
    {
        public Int32 Source { get; }

        public Int32 Target { get; }

        public Double Weight { get; }

        public Edge(Int32 sourceVertice, Int32 targetVertice, Double weight)
        {
            this.Source = sourceVertice;
            this.Target = targetVertice;
            this.Weight = weight;
        }

        public Int32 Other(Int32 vertice)
        {
            if (vertice == this.Source)
                return this.Target;
            else if (vertice == this.Target)
                return this.Source;

            throw new Exception("Inconsistent edge.");
        }

        public Int32 CompareTo(Edge edge)
        {
            if (this.Weight < edge.Weight)
                return -1;
            else if
                (this.Weight > edge.Weight)
                return 1;

            return 0;
        }

        public override String ToString()
        {
            return String.Format("{0}-{1} | {2:f5}", this.Source, this.Target, this.Weight);
        }

        public Boolean Equals(Edge edge)
        {
            return edge.Source == this.Source
                && edge.Target == this.Target
                && edge.Weight == this.Weight;
        }
    }
}
