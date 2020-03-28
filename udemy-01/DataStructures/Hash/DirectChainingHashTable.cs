using System;
using DataStructures.Hash.Functions;
using DataStructures.LinkedList;

namespace DataStructures.Hash
{
    public class DirectChainingHashTable<T> : IHashTable<T> where T : class
    {
        private readonly SingleLinkedList<T>[] _array;
        private readonly IHashFunction _hashFunction;

        public int Size { get; private set; }

        public int Count { get; private set; }

        public DirectChainingHashTable(int size, IHashFunction hashFunction)
        {
            if (size < 1)
                throw new ArgumentOutOfRangeException(nameof(size));

            this._hashFunction = hashFunction ?? throw new ArgumentNullException(nameof(hashFunction));
            this._array = new SingleLinkedList<T>[size];

            this.Size = size;
        }

        public void Add(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            int index = this._hashFunction.Hash(value.ToString()) % this.Size;

            if (this._array[index] == null)
            {
                SingleLinkedList<T> ll = new SingleLinkedList<T>();
                ll.InsertAt(ll.Count, value);

                this._array[index] = ll;
            }
            else
            {
                SingleLinkedList<T> ll = this._array[index];

                if (!ll.IsEmpty && !ll.Contains(value))
                {
                    ll.InsertAt(ll.Count, value);
                }
            }

            this.Count++;
        }

        public void Delete(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            int index = this._hashFunction.Hash(value.ToString()) % this.Size;

            SingleLinkedList<T> ll = this._array[index];

            if (ll != null)
            {
                Node<T>? current = ll.Head;
                int indexCurrent = -1;

                do
                {
                    indexCurrent++;

                    if (current!.Value!.Equals(value))
                    {
                        ll.DeleteAt(indexCurrent);

                        if (ll.Count == 0)
                        {
                            this._array[index] = null!;
                        }

                        this.Count--;

                        break;
                    }

                    current = current.Next;
                } while (current != null);
            }
        }

        public bool Contains(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            SingleLinkedList<T> ll = this._array[this._hashFunction.Hash(value.ToString()) % this.Size];

            return ll != null && !ll.IsEmpty && ll.Contains(value);
        }
    }
}
