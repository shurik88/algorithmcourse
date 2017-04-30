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

            //var set = new HashSet<long>();

            const int size = 16785407;//10000079;//16777216;
            const int m_prime = 1073807359;//10000019
            var arr = new long[size];
            for (var i = 0; i < size; ++i)
                arr[i] = -1;
            for (var i = 0; i < n; ++i)
            {
                var j = 1;
                var exists = false;

                var x_key = x;
                //if (x_key == Int32.MinValue)
                //    x_key = Int32.MaxValue;
                //else if (x_key < 0)
                //    x_key = Math.Abs(x_key);

                var h1 = x_key % size; //x_key.GetHashCode() & Lower31BitMask;
                var h2 = x_key % m_prime + 1;//(size - 1) + 1;
                int key = 0;
                //Console.Write(x.ToString() + " ");
                do
                {
                    key = Math.Abs((int)(h1 + j * h2) % size);
                    //Console.Write(key.ToString() + " ");
                    if (arr[key] == x)
                    {
                        exists = true;
                        //Console.WriteLine($"{i} {j}");
                        break;
                    }

                    j++;


                } while (arr[key] != -1 && j != size);
                //Console.WriteLine();
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
                //if(set.Contains(x))
                //{
                //    a = (a + ac) % 1000;
                //    b = (b + bc) % 1000000000000000;
                //}
                //else
                //{
                //    a = (a + ad) % 1000;
                //    b = (b + bd) % 1000000000000000;
                //    set.Add(x);
                //}
                //x = (x * a + b) % 1000000000000000;

            }
            //Console.ReadKey();
            File.WriteAllText("output.txt", string.Join(" ", x, a, b));
        }

        private const int Lower31BitMask = 0x7FFFFFFF;
    }
}
