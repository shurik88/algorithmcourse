using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6.Task6_2
{
    public class Task6_2
    {
        public static void Main(string[] args)
        {
            var content = File.ReadAllText("input.txt");
            var data = content.Split(new[] {' '});
            if (data.Length != 2)
                throw new ArgumentException("Invalid file format");

            var n = Int32.Parse(data[0]);
            var a = double.Parse(data[1], CultureInfo.InvariantCulture);
            if (n < 3)
                throw new ArgumentException("Invalid N");
            if (n == 3)
            {
                if(n < 2)
                    File.WriteAllText("output.txt", GetNext(a, 0).ToString(CultureInfo.InvariantCulture));
                else
                {
                    File.WriteAllText("output.txt", "0.0");
                }
                return;
            }

            const double E = 0.00000000001;
            var up = a;
            var down = 0.0;
            while(true)
            {
                var h2 = (up + down) / 2.0;
                var numbers = new double[n];
                numbers[0] = a;
                numbers[1] = h2;
                for (var j = 2; j < n; ++j)
                {
                    numbers[j] = 2 * (numbers[j - 1] + 1) - numbers[j - 2];
                }
                
                //Console.WriteLine($" {h2}: {string.Join(" ", numbers)}");
                var count = numbers.Count(x => x <= 0 || Math.Abs(x) < E);
                if (count == 1 && Math.Abs(numbers.First(x => x <= 0 || Math.Abs(x) < E)) <= E)
                {
                    File.WriteAllText("output.txt", numbers[n - 1].ToString(CultureInfo.InvariantCulture));
                    return;
                }
                if (count == 0)
                {
                    up = h2;
                }
                else
                {
                    down = h2;
                }
            }

        }

        private static double GetNext(double hi0, double hi1)
        {
            return 2 * (hi1 + 1) - hi0;
        }
    }
}
