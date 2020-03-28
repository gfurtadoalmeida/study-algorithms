using System.Collections.Generic;
using BT = DataStructures.BinaryTree;

namespace DataStructures.BinarySearchTree
{
    public interface IBinarySearchTree<T> : IEnumerable<T>
    {
        bool IsEmpty { get; }

        void Add(T value);

        BT.Node<T>? Get(T value);

        bool Contains(T value);

        void Delete(T value);
    }
}