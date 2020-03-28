namespace Algorithms.Sorting
{
    public class InsertionSortAlgorithm : SortAlgorithm
    {
        public static readonly ISortAlgorithm Instance = new InsertionSortAlgorithm();

        protected override void OnSort<T>(ref T[] array, int arrayLength)
        {
            /*    B┌----<----┐
             *     |  A┌┐    |     A = Moving itens grater than current
             *     |   ||    |     B = Inserting current
             *  [ .............. ]
             *              ^>   
             *              sortedIndexOffset and current
             */

            T current;
            int unsortedIndex;

            for (int sortedIndexOffset = 1; sortedIndexOffset < arrayLength; sortedIndexOffset++)
            {
                current = array[sortedIndexOffset];
                unsortedIndex = sortedIndexOffset;
                
                // Move items below current's position that are greater than current
                // to one position ahead of their current position.
                while (unsortedIndex > 0 && array[unsortedIndex - 1].CompareTo(current) > 0)
                {
                    array[unsortedIndex] = array[unsortedIndex - 1];

                    unsortedIndex--;
                }

                // Insert current in the slot after all values greater than it.
                array[unsortedIndex] = current;
            }
        }
    }
}