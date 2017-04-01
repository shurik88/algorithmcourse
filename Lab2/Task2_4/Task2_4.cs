using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Task2_4
{
    class Task2_4
    {
        public static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt");
            var firstLine = content[0].Split(new[] { ' ' }).Select(x => Int32.Parse(x)).ToArray();
            var secondLine = content[1].Split(new[] { ' ' }).Select(x => Int32.Parse(x)).ToArray();

            var n = firstLine[0];
            var k1 = firstLine[1];
            var k2 = firstLine[2];

            var array = GenerateArray(n, secondLine[0], secondLine[1], secondLine[2], secondLine[3], secondLine[4]);
            QuickPartialSort(array, k1 - 1, k2 - 1, 0, array.Length - 1);

            var writer = new StreamWriter("output.txt");

            writer.WriteLine(string.Join(" ", array.Skip(k1 - 1).Take(k2 - k1 + 1)));
            writer.Dispose();

        }

        public static void QuickPartialSort(int[] arr, int k1, int k2, int left, int right)
        {
            if (left == right)
                return;
            if (left < k1 && right < k1 || left > k2 && right > k2)
                return;
            var x = arr[_rand.Next(left, right)];
            var i = left;
            var j = right;
            while (i <= j)
            {
                while (arr[i] < x)
                    i++;
                while (arr[j] > x)
                    j--;
                if (i <= j)
                {
                    Swap(arr, i, j);
                    i++;
                    j--;
                }
            }

            if (i < right)
                QuickPartialSort(arr, k1, k2, i, right);
            if (left < j)
                QuickPartialSort(arr, k1, k2, left, j);
        }

        public static int[] GenerateArray(int size, int A, int B, int C, int a1, int a2)
        {
            var arr = new int[size];
            arr[0] = a1;
            arr[1] = a2;
            for(var i = 2; i < size; ++i)
            {
                arr[i] = A * arr[i - 2] + B * arr[i - 1] + C;
            }
            return arr;
        }

        static Random _rand = new Random();

        public static void QuickSort(int[] arr, int left, int right)
        {
            if (left == right)
                return;
            var x = arr[_rand.Next(left, right)];
            var i = left;
            var j = right;
            while (i <= j)
            {
                while (arr[i] < x)
                    i++;
                while (arr[j] > x)
                    j--;
                if (i <= j)
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

        private static void Swap(int[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}
