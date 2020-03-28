namespace DataStructures.Hash
{
    public interface IHashTable<in T> where T : class
    {
        int Count { get; }

        int Size { get; }

        void Add(T value);

        bool Contains(T value);

        void Delete(T value);
    }
}
