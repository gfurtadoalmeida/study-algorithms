using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Algorithms.Sorting
{
    public class BucketSortAlgorithm : SortAlgorithm
    {
        public static readonly BucketSortAlgorithm Instance = new BucketSortAlgorithm();

        protected override void OnSort<T>(ref T[] array, int arrayLength)
        {
            if (!(array[0] is byte))
                throw new ArgumentException("Only byte arrays are supported.", nameof(array));

            unsafe
            {
                this.SortCore(ref Unsafe.AsRef<byte[]>(Unsafe.AsPointer(ref array!)), arrayLength);
            }
        }

        private void SortCore(ref byte[] array, int arrayLength)
        {
            byte maxValue = 0;
            int numBuckets = (int)Math.Ceiling(Math.Sqrt(arrayLength));
            List<byte>[] buckets = new List<byte>[numBuckets];

            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<byte>();
            }

            // Find max value
            foreach (byte value in array)
            {
                if (maxValue < value)
                {
                    maxValue = value;
                }
            }

            // Scatter the values.
            foreach (byte value in array)
            {
                buckets[(int)Math.Ceiling((double)(value * numBuckets) / maxValue) - 1].Add(value);
            }

            // Sort each bucket.
            foreach (List<byte> bucket in buckets)
            {
                bucket.Sort();
            }

            int resultIndex = 0;

            // Merge the results back.
            foreach (List<byte> bucket in buckets)
            {
                foreach (byte value in bucket)
                {
                    array[resultIndex++] = value;
                }
            }
        }
    }
}
