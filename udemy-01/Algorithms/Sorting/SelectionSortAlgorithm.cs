namespace Algorithms.Sorting
{
    public class SelectionSortAlgorithm : SortAlgorithm
    {
        public static readonly ISortAlgorithm Instance = new SelectionSortAlgorithm();

        protected override void OnSort<T>(ref T[] array, int arrayLength)
        {
            /*       ┌-<-┐
             *       |   |
             *  [ .............. ]
             *       ^   ^  
             *  sorted   lastMinIndex
             */

            int lastMinIndex;

            for (int sortedIndex = 0; sortedIndex < arrayLength; sortedIndex++)
            {
                lastMinIndex = sortedIndex;

                // Find the minimum value in the unsorted part of the array.
                for (int unsortedIndex = sortedIndex + 1; unsortedIndex < arrayLength; unsortedIndex++)
                {
                    if (array[unsortedIndex].CompareTo(array[lastMinIndex]) < 0)
                    {
                        lastMinIndex = unsortedIndex;
                    }
                }

                if (lastMinIndex != sortedIndex)
                {
                    T temp = array[sortedIndex];
                    array[sortedIndex] = array[lastMinIndex];
                    array[lastMinIndex] = temp;
                }
            }
        }
    }
}
