using System;

namespace Algorithms.Structures.Union
{
    /// <summary>
    /// Represents a union–find data type (also known as disjoint-sets data type) that models connectivity 
    /// among a set of "n" sites, named 0 through "n–1".
    /// Supports:
    /// - Union and Find operations.
    /// - Connected operation for determining whether two sites are in the same component.
    /// - Count that returns the total number of components.
    /// </summary>
    public sealed class UnionFinder : IUnionFinder
    {
        private Int32[] _parent; // parent[i] = parent of i.
        private Byte[] _rank;    // rank[i] = rank of subtree rooted at i (never more than 31).

        public Int32 Count { get; private set; }

        public UnionFinder(Int32 capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this.Count = capacity;

            this._parent = new Int32[capacity];
            this._rank = new Byte[capacity];

            for (int i = 0; i < capacity; i++)
            {
                this._parent[i] = i;
                this._rank[i] = 0;
            }
        }

        /// <summary>
        /// Returns the component identifier for the object containing "id". 
        /// </summary>
        /// <param name="id">Integer representing one object.</param>
        /// <returns>Component identifier for the object containing "id".</returns>
        public Int32 Find(Int32 id)
        {
            while (id != this._parent[id])
            {
                this._parent[id] = this._parent[this._parent[id]]; // Path compression by halving.

                id = this._parent[id];
            }

            return id;
        }

        /// <summary>
        /// Returns true if the the two objects are in the same component.
        /// </summary>
        /// <param name="idOne">Integer representing one object.</param>
        /// <param name="idTwo">Integer representing the other object.</param>        
        /// <returns>If the two objects are in the same component.</returns>

        public Boolean IsConnected(Int32 idOne, Int32 idTwo)
        {
            return this.Find(idOne) == this.Find(idTwo);
        }

        /// <summary>
        /// Merges the objects containing "idOne" with the the component containing "idTwo".
        /// </summary>
        /// <param name="idOne">Integer representing one object.</param>
        /// <param name="idTwo">Integer representing the other object.</param>        
        public void Union(Int32 idOne, Int32 idTwo)
        {
            Int32 rootObjectOne = this.Find(idOne);
            Int32 rootObjectTwo = this.Find(idTwo);

            if (rootObjectOne == rootObjectTwo)
                return;

            // Make the root of the smaller rank point to the root of larger rank.
            if (this._rank[rootObjectOne] < this._rank[rootObjectTwo])
                this._parent[rootObjectOne] = rootObjectTwo;
            else if (this._rank[rootObjectOne] > this._rank[rootObjectTwo])
                this._parent[rootObjectTwo] = rootObjectOne;
            else
            {
                this._parent[rootObjectTwo] = rootObjectOne;
                this._rank[rootObjectOne]++;
            }

            this.Count--;
        }
    }
}