using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Task2_2
{
    class Task2_2
    {
        public static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt");
            var array = content[1].Split(new char[] { ' ' }).Select(x => long.Parse(x)).ToArray();
            var writer = new StreamWriter("output.txt");

            inv = 0;
            var sorted = MergeSort(array);

            writer.WriteLine(inv);
            writer.Dispose();

        }

        static long inv;

        private static long[] Merge(long[] arr1, long[] arr2)
        {
            var i = 0;
            var j = 0;
            var arr = new long[arr1.Length + arr2.Length];
            while (i < arr1.Length || j < arr2.Length)
            {
                if (j == arr2.Length || (i < arr1.Length && arr1[i] <= arr2[j]))
                {
                    arr[i + j] = arr1[i];
                    i++;
                }
                else
                {
                    inv += (arr1.Length - i);
                    arr[i + j] = arr2[j];
                    j++;
                }
            }
            return arr;
        }

        private static long[] MergeSort(long[] arr)
        {
            //Console.WriteLine("{0} {1}", lIndex, rIndex);
            if (arr.Length == 1)
                return arr;

            var left = MergeSort(arr.Take(arr.Length / 2).ToArray());
            var right = MergeSort(arr.Skip(arr.Length / 2).ToArray());
            var merged = Merge(left, right);
            return merged;
        }
    }
}
