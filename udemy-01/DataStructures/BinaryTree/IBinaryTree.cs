using System.Collections.Generic;

namespace DataStructures.BinaryTree
{
    public interface IBinaryTree<T>
    {
        bool IsEmpty { get; }

        void Add(T value);

        bool Contains(T value);

        void Delete(T value);

        IEnumerator<T> GetEnumeratorStack(EnumerationMode enumerationMode);

        IEnumerator<T> GetEnumeratorRecursive(EnumerationMode enumerationMode);
    }
}
