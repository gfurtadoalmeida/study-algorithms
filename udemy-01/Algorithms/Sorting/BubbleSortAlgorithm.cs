namespace Algorithms.Sorting
{
    public class BubbleSortAlgorithm : SortAlgorithm
    {
        public static readonly ISortAlgorithm Instance = new BubbleSortAlgorithm();

        protected override void OnSort<T>(ref T[] array, int arrayLength)
        {
            /*
             *        ┌┐
             *        ||
             *  [ .............. ]
             *        ^^   <^  
             *  unsorted   size-upperBoundOffset-1 = sorted
             */

            for (int upperBoundOffset = 0; upperBoundOffset < arrayLength - 1; upperBoundOffset++)
            {
                for (int unsortedIndex = 0; unsortedIndex < (arrayLength - upperBoundOffset - 1); unsortedIndex++)
                {
                    if (array[unsortedIndex].CompareTo(array[unsortedIndex + 1]) > 0)
                    {
                        T temp = array[unsortedIndex];
                        array[unsortedIndex] = array[unsortedIndex + 1];
                        array[unsortedIndex + 1] = temp;
                    }
                }
            }
        }
    }
}
