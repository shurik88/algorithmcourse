using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6.Task6_1
{
    class Task6_1
    {
        public static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt");
            var arr = content[1].Split(new char[] { ' ' }).Select(x => Int32.Parse(x)).ToArray();
            var queries = content[3].Split(new char[] { ' ' }).Select(x => Int32.Parse(x)).ToArray();
            using (var writer = new StreamWriter("output.txt"))
            {
                for (var i = 0; i < queries.Length; ++i)
                {
                    var first = BinarySearch(arr, queries[i], true);

                    writer.WriteLine("{0} {1}", first == -1 ? -1 : first + 1, first == -1 ? first : BinarySearch(arr, queries[i], false) + 1);
                }
            }
        }

        public static int BinarySearch(int[] arr, int key, bool first)
        {
            var l = -1;
            var r = arr.Length;

            while (r > l + 1)
            {
                var m = (r + l) / 2;
                if (first == true)
                {
                    if (arr[m] < key)
                        l = m;
                    else
                        r = m;
                }
                else
                {
                    if (arr[m] <= key)
                        l = m;
                    else
                        r = m;
                }
            }
            if (first)
            {
                if (r < arr.Length && arr[r] == key)
                    return r;
                return -1;
            }
            else
            {
                if (l >= 0 && arr[l] == key)
                    return l;
                return -1;
            }
        }


    }
}
