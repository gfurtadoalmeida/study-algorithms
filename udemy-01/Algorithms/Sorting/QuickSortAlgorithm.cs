namespace Algorithms.Sorting
{
    public class QuickSortAlgorithm : SortAlgorithm
    {
        public static readonly ISortAlgorithm Instance = new QuickSortAlgorithm();

        protected override void OnSort<T>(ref T[] array, int arrayLength)
        {
            QuickSort(ref array, 0, arrayLength - 1);

            static void QuickSort(ref T[] array, int bottomIndex, int topIndex)
            {
                if (bottomIndex < topIndex)
                {
                    int pivotIndex = CalculatePivot(ref array, bottomIndex, topIndex);

                    QuickSort(ref array, bottomIndex, pivotIndex - 1);
                    QuickSort(ref array, pivotIndex + 1, topIndex);
                }
            }

            static int CalculatePivot(ref T[] array, int bottomIndex, int topIndex)
            {
                T topValue = array[topIndex];
                int pivotIndex = bottomIndex - 1;

                for (int searchIndex = bottomIndex; searchIndex < topIndex; searchIndex++)
                {
                    if (array[searchIndex].CompareTo(topValue) < 0)
                    {
                        pivotIndex++;

                        Swap(ref array, pivotIndex, searchIndex);
                    }
                }

                pivotIndex++;

                Swap(ref array, pivotIndex, topIndex);

                return pivotIndex;
            }

            static void Swap(ref T[] array, int indexA, int indexB)
            {
                T temp = array[indexA];

                array[indexA] = array[indexB];
                array[indexB] = temp;
            }
        }
    }
}
