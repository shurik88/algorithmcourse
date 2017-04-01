using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Task2_1
{
    class Task2_1
    {
        public static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt");
            var array = content[1].Split(new char[] { ' ' }).Select(x => long.Parse(x)).ToArray();
            var writer = new StreamWriter("output.txt");

            var sorted = MergeSort(array, 0, array.Length, writer);

            writer.WriteLine(string.Join(" ", sorted));
            writer.Dispose();
           
        }

        public static long[] Merge(long[] arr1, long[] arr2)
        {
            var i = 0; 
            var j = 0;
            var arr = new long[arr1.Length + arr2.Length];
            while (i < arr1.Length || j < arr2.Length )
            {
                if( j == arr2.Length || (i < arr1.Length && arr1[i] <= arr2[j]))
                {
                    arr[i + j] = arr1[i];
                    i++;
                }
                else
                {
                    arr[i + j] = arr2[j];
                    j++;
                }
            }
            return arr;
        }

        public static long[] MergeSort(long[] arr, int lIndex, int rIndex, StreamWriter writer)
        {
            //Console.WriteLine("{0} {1}", lIndex, rIndex);
            if (lIndex + 1 == rIndex)
                return new long[] { arr[lIndex] };

            var left = MergeSort(arr, lIndex, lIndex + (rIndex - lIndex) / 2, writer);
            var right = MergeSort(arr, lIndex + (rIndex - lIndex) / 2, rIndex, writer);
            var merged = Merge(left, right);
            if(writer != null)
                writer.WriteLine("{0} {1} {2} {3}", lIndex + 1, rIndex, merged[0], merged[merged.Length - 1]);
            return merged;
        }


    }
}
