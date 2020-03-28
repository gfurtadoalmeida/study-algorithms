using System;

namespace DataStructures.Union
{
    public sealed class WeightedQuickUnionFinder : IUnionFinder
    {
        private readonly Int32[] _parent;   // parent[i] = parent of i.
        private readonly Int32[] _size;     // size[i] = number of sites in subtree rooted at i.

        public Int32 Count { get; private set; }

        public WeightedQuickUnionFinder(Int32 capacity)
        {
            this.Count = capacity;

            this._parent = new Int32[capacity];
            this._size = new Int32[capacity];

            for (int i = 0; i < capacity; i++)
            {
                this._parent[i] = i;
                this._size[i] = 1;
            }
        }

        public Int32 Find(Int32 id)
        {
            while (id != this._parent[id])
            {
                id = this._parent[id];
            }

            return id;
        }

        public Boolean IsConnected(Int32 idOne, Int32 idTwo)
        {
            return this.Find(idOne) == this.Find(idTwo);
        }

        public void Union(Int32 idOne, Int32 idTwo)
        {
            Int32 rootObjectOne = this.Find(idOne);
            Int32 rootObjectTwo = this.Find(idTwo);

            if (rootObjectOne == rootObjectTwo)
                return;

            // make smaller root point to larger one
            if (this._size[rootObjectOne] < this._size[rootObjectTwo])
            {
                this._parent[rootObjectOne] = rootObjectTwo;
                this._size[rootObjectTwo] += this._size[rootObjectOne];
            }
            else
            {
                this._parent[rootObjectTwo] = rootObjectOne;
                this._size[rootObjectOne] += this._size[rootObjectTwo];
            }

            this.Count--;
        }
    }
}
