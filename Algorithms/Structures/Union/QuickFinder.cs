using System;

namespace Algorithms.Structures.Union
{
    public sealed class QuickFinder : IUnionFinder
    {
        private Int32[] _ids; // id[i] = component identifier of i

        public Int32 Count { get; private set; }

        public QuickFinder(Int32 capacity)
        {
            this.Count = capacity;

            this._ids = new Int32[capacity];

            for (int i = 0; i < capacity; i++)
                this._ids[i] = i;
        }

        public Int32 Find(Int32 id)
        {
            return this._ids[id];
        }

        public Boolean IsConnected(Int32 idOne, Int32 idTwo)
        {
            return this._ids[idOne] == this._ids[idTwo];
        }

        public void Union(Int32 idOne, Int32 idTwo)
        {
            Int32 idOneId = this._ids[idOne];
            Int32 idTwoId = this._ids[idTwo];

            // idOne and idTwo are already in the same component.
            if (idOneId == idTwoId)
                return;

            for (int i = 0; i < this._ids.Length; i++)
            {
                if (this._ids[i] == idOneId)
                    this._ids[i] = idTwoId;
            }

            this.Count--;
        }
    }
}
