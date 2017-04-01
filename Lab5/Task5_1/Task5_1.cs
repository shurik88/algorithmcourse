using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab5.Task5_1
{
    class Task5_1
    {
        static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt");

            var arr1 = content[1].Split(new char[] { ' ' }).Select(x => int.Parse(x)).ToArray();

            var isHeap = true;
            var count = arr1.Length / 2;
            for(var  i = 0; i <= count; ++i)
            {
                var left = (2 * i + 1) >= arr1.Length ? arr1[i] : arr1[2 * i + 1];
                var right = (2 * i + 2) >= arr1.Length ? arr1[i] : arr1[2 * i + 2]; ;
                if(arr1[i] > left || arr1[i] > right)
                {
                    isHeap = false;
                    break;
                }
            }
            var writer = new StreamWriter("output.txt");
            writer.WriteLine(isHeap ? "YES" : "NO");
            writer.Close();
        }
    }
}
