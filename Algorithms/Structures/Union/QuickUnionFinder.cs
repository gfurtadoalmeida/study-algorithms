using System;

namespace Algorithms.Structures.Union
{
    public sealed class QuickUnionFinder : IUnionFinder
    {
        private Int32[] _parent; // parent[i] = parent of i.

        public Int32 Count { get; private set; }

        public QuickUnionFinder(Int32 capacity)
        {
            this._parent = new Int32[capacity];

            this.Count = capacity;

            for (int i = 0; i < capacity; i++)
            {
                this._parent[i] = i;
            }
        }

        public Int32 Find(Int32 id)
        {
            while (id != this._parent[id])
                id = this._parent[id];

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

            this._parent[rootObjectOne] = rootObjectTwo;

            this.Count--;
        }
    }
}
