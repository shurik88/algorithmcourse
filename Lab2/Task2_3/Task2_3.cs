using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Task2_3
{
    class Task2_3
    {
        public static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt");
            var size = Int32.Parse(content[0]);
            var writer = new StreamWriter("output.txt");

            var arr = GetBadArrayForQuickSort(size);

            writer.WriteLine(string.Join(" ", arr));
            writer.Dispose();

        }

        public static long[] GetBadArrayForQuickSort(int size)
        {
            if (size == 1)
                return new long[] { 1 };
            else if (size == 2)
                return new long[] { 1, 2 };
            //else if (size == 3)
            //    return new long[3] { 1, 3, 2 };

            var arr = new long[size];
            arr[0] = 1;
            arr[1] = 2;
            //arr[2] = 2;
            //arr[3] = 3;
            for (var i = 4; i < size; i++)
            {
                int p = (i) / 2;
                arr[i] = arr[p];
                arr[p] = i + 1;
            }
            return arr;
        }
    }
}
