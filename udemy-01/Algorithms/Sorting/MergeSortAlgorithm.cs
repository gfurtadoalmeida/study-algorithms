namespace Algorithms.Sorting
{
    public class MergeSortAlgorithm : SortAlgorithm
    {
        public static readonly ISortAlgorithm Instace = new MergeSortAlgorithm();

        protected override void OnSort<T>(ref T[] array, int arrayLength)
        {
            MergeSort(ref array, 0, arrayLength - 1);

            static void MergeSort(ref T[] array, int bottomIndex, int topIndex)
            {
                if (bottomIndex < topIndex)
                {
                    int middle = (bottomIndex + topIndex) / 2;

                    MergeSort(ref array, bottomIndex, middle);
                    MergeSort(ref array, middle + 1, topIndex);

                    Merge(ref array, bottomIndex, middle, topIndex);
                }
            }

            static void Merge(ref T[] array, int bottomIndex, int middleIndex, int topIndex)
            {
                int leftArraySize = middleIndex - bottomIndex + 1;
                int rightArraySize = topIndex - middleIndex;

                T[] leftArray = new T[leftArraySize];
                T[] rightArray = new T[rightArraySize];

                for (int i = 0; i <= middleIndex - bottomIndex; i++)
                {
                    leftArray[i] = array[bottomIndex + i];
                }

                for (int i = 0; i < topIndex - middleIndex; i++)
                {
                    rightArray[i] = array[middleIndex + 1 + i];
                }

                int leftArrayIndex = 0;
                int rightArrayIndex = 0;

                int index = bottomIndex;

                // Compare items in the arrays.
                while (leftArrayIndex < leftArraySize && rightArrayIndex < rightArraySize)
                {
                    if (leftArray[leftArrayIndex].CompareTo(rightArray[rightArrayIndex]) <= 0)
                    {
                        array[index] = leftArray[leftArrayIndex];

                        leftArrayIndex++;
                    }
                    else
                    {
                        array[index] = rightArray[rightArrayIndex];

                        rightArrayIndex++;
                    }

                    index++;
                }

                // Copy remaining itens.
                while (leftArrayIndex < leftArraySize)
                {
                    array[index] = leftArray[leftArrayIndex];

                    index++;
                    leftArrayIndex++;
                }

                // Copy remaining itens.
                while (rightArrayIndex < rightArraySize)
                {
                    array[index] = rightArray[rightArrayIndex];

                    index++;
                    rightArrayIndex++;
                }
            }
        }
    }
}
