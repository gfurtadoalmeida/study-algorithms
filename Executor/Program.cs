using System;
using System.Collections;
using Algorithms.Sorting;

namespace Executor
{
    class Program
    {
        static void Main(String[] args)
        {
            const Int32 INPUT_SIZE = 25;
            const Int32 MAX_VALUE = 100;

            Random random = new Random();
            Int32[] input = new Int32[INPUT_SIZE];

            for (int i = 0; i < input.Length; i++)
                input[i] = random.Next(MAX_VALUE);

            Describe("Ïnput", input);

            AbstractSort<Int32> algorithm = new SelectionSort<Int32>();

            algorithm.Sort(input);

            Describe("Sorted", input);

            Console.ReadKey();
        }

        static void Describe(String label, IEnumerable enumerable )
        {
            Console.WriteLine(label + ": ");

            IEnumerator enumerator = enumerable.GetEnumerator();

            while (enumerator.MoveNext())
                Console.Write(" " + enumerator.Current);

            Console.WriteLine();
        }
    }
}
