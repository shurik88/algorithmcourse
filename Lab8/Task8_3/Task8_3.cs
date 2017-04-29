using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8.Task8_3
{
    public class Task8_3
    {
        public static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt");
            var line1 = content[0].Split(new[] {' '});
            var line2 = content[1].Split(new[] { ' ' });

            var n = int.Parse(line1[0]);
            var x = long.Parse(line1[1]);
            var a = int.Parse(line1[2]);
            var b = long.Parse(line1[3]);

            var ac = int.Parse(line2[0]);
            var bc = long.Parse(line2[1]);
            var ad = int.Parse(line2[2]);
            var bd = long.Parse(line2[3]);

            var arr = new long[n];
            for (var i = 0; i < n; ++i)
                arr[i] = -1;
            //var set = new HashSet<long>();
            for (var i = 0; i < n; ++i)
            {
                var j = 0;
                int key;
                var exists = false;
                do
                {
                    key = (int) (x + (n - 1) * j);
                    key = key == Int32.MinValue ? Int32.MaxValue % n : Math.Abs(key) % n;
                    //key = //(int)((x +  x % n * j) % n);
                    if (arr[key] == x)
                    {
                        exists = true;
                        break;
                    }
                    j++;


                } while (arr[key] != -1 && j != n);
                if (exists)
                {
                    a = (a + ac) % 1000;
                    b = (b + bc) % 1000000000000000;
                }
                else
                {
                    a = (a + ad) % 1000;
                    b = (b + bd) % 1000000000000000;
                    arr[key] = x;
                    //set.Add(x);
                }
                x = (x * a + b) % 1000000000000000;
            }
            File.WriteAllText("output.txt", string.Join(" ", x, a, b));
        }
    }
}
