using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch
            var content = File.ReadAllLines("input.txt");
            var arr1 = content[1].Split(new char[] { ' ' }).Select(x => int.Parse(x)).ToArray();
            var arr2 = content[2].Split(new char[] { ' ' }).Select(x => int.Parse(x)).ToArray();
            stopwatch.Stop();
            var prepare = stopwatch.ElapsedMilliseconds;

            var sum = Task3_1.Task3_1.GetSumOfKNumberOfSortedArrayWithMatrix2(arr1, arr2, 10);

            //var arr1 = new int[] { 2, 4, 2, 2, 2 };
            //var arr2 = new int[] { 1, 1, 1, 1, 1, };

            //var countingSum = Task3_1.Task3_1.GetSumOfKNumberOfSortedArrayWithMatrix2(arr1, arr2, 10);
            //var arr = new int[] { 3, 7, 0, 2, 3, 6, 9, 10, 1, 2, 6, 6, 5 };
            //var sorted = Task3_1.Task3_1.CountingSort(arr, 11);
        }

        static Random rand = new Random();
    }
}
