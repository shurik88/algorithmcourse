using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt");
            var firstLine = content[0].Split(new[] { ' ' }).Select(x => Int32.Parse(x)).ToArray();
            var secondLine = content[1].Split(new[] { ' ' }).Select(x => Int32.Parse(x)).ToArray();

            var n = firstLine[0];
            var k1 = firstLine[1];
            var k2 = firstLine[2];
            //var arr1 = Task2_1.Task2_1.MergeSort(new long[] { 1, 8, 2, 1, 4, 7, 3, 2, 3, 6 }, 0, 10, null);
            inv = 0;
            var res = Task2_4.Task2_4.GenerateArray(n, secondLine[0], secondLine[1], secondLine[2], secondLine[3], secondLine[4]);
            Task2_4.Task2_4.QuickPartialSort(res, k1 - 1, k2 - 1, 0, res.Length - 1);
            //QuickSort(res, 0, res.Length);
            //AvailableToSort(arr, 3);
            //var arr = Task2_1.Task2_1.Merge(new long[3] { 1, 5, 6 }, new long[4] { 0, 2, 3, 8 });
        }
        static Random _rand = new Random();
        private static void QuickSort(long[] arr, int left, int right)
        {
            if (left == right)
                return;
            var x = arr[_rand.Next(left, right)];
            var i = left;
            var j = right;
            while(i <= j)
            {
                while (arr[i] < x)
                    i++;
                while (arr[j] > x)
                    j--;
                if(i <= j)
                {
                    Swap(arr, i, j);
                    i++;
                    j--;
                }
            }

            if (i < right)
                QuickSort(arr, i, right);
            if (left < j)
                QuickSort(arr, left, j);
        }

        private static void Swap(long[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        static int inv;

        private static void AvailableToSort(long[] arr, int k)
        {

            for (var i = 0; i < k; ++i)
            {
                var indexes = Enumerable.Range(0, arr.Length / k + 1).Select(x => i + x * k).Where(x => x < arr.Length).ToArray();
                SelectSort(arr, indexes);
            }
        }

        private static bool IsArraySorted(long[] arr)
        {
            for (var i = 0; i < arr.Length - 1; ++i)
            {
                if (arr[i] > arr[i + 1])
                    return false;
            }
            return true;
        }

        private static void SelectSort(long[] arr, int[] indexes)
        {
            var arrayToSort = arr.Where((x, i) => indexes.Contains(i));

            for (var i = 0; i < indexes.Length; ++i)
            {
                var min = arr[indexes[i]];
                var indexMin = indexes[i];
                for (var j = i + 1; j < indexes.Length; ++j)
                {
                    if (arr[indexes[j]] < min)
                    {
                        min = arr[indexes[j]];
                        indexMin = indexes[j];
                    }
                }
                if (indexMin != i)
                {
                    var temp = arr[indexes[i]];
                    arr[indexes[i]] = min;
                    arr[indexMin] = temp;
                }
            }
        }
    }
}
