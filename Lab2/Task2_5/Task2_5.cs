using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Task2_5
{
    class Task2_5
    {
        public static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt");
            var array = content[1].Split(new char[] { ' ' }).Select(x => long.Parse(x)).ToArray();
            var k = content[0].Split(new char[] {' ' }).Select(x => int.Parse(x)).ToArray()[1];
            if(k != 1)
                ScareCrowSort(array, k);
           
            var writer = new StreamWriter("output.txt");
            //writer.WriteLine(string.Join(" ", array));
            writer.WriteLine(k== 1 || IsArraySorted(array) ? "YES" : "NO");
            writer.Dispose();

        }

        static Random _rand = new Random();
        private static void QuickSort(long[] arr, int[] indexes, int left, int right)
        {
            if (left == right)
                return;
            var x = arr[indexes[_rand.Next(left, right)]];
            var i = left;
            var j = right;
            while (i <= j)
            {
                while (arr[indexes[i]] < x)
                    i++;
                while (arr[indexes[j]] > x)
                    j--;
                if (i <= j)
                {
                    Swap(arr, indexes, i, j);
                    i++;
                    j--;
                }
            }

            if (i < right)
                QuickSort(arr, indexes, i, right);
            if (left < j)
                QuickSort(arr, indexes, left, j);
        }

        private static void Swap(long[] arr, int[] indexes, int i, int j)
        {
            var temp = arr[indexes[i]];
            //var tempIndex = indexes[i];
            arr[indexes[i]] = arr[indexes[j]];
            //indexes[i] = indexes[j];
            arr[indexes[j]] = temp;
            //indexes[j] = tempIndex;
        }

        //private static void QuickSort(long[] arr, int[] indexes, int left, int right)
        //{
        //    var x = arr[indexes[left + (right - left) / 2]];
        //    int i = left;
        //    int j = right;
        //    while (i <= j)
        //    {
        //        while (a[i] < x) i++;
        //        while (a[j] > x) j--;
        //        if (i <= j)
        //        {
        //            temp = a[i];
        //            a[i] = a[j];
        //            a[j] = temp;
        //            i++;
        //            j--;
        //        }
        //    }

        //}

        public static void ScareCrowSort(long[] arr, int k)
        {
            for (var i = 0; i < k; ++i)
            {
                var indexes = Enumerable.Range(0, arr.Length / k + 1).Select(x => i + x * k).Where(x => x < arr.Length).ToArray();
                QuickSort(arr, indexes, 0, indexes.Length - 1);
                //SelectSort(arr, indexes);
            }
        }

        public static bool IsArraySorted(long[] arr)
        {
            for (var i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] > arr[i + 1])
                    return false;
            }
            return true;
        }

        private static void SelectSort(long[] arr, int[] indexes)
        {
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
