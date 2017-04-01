using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Task1_1
    {
        static void Main(string[] args)
        {
            var content = File.ReadAllText("input.txt");
            var numbers = content.Split(new[] {' ' });
            File.WriteAllText("output.txt", (long.Parse(numbers[0]) + long.Parse(numbers[1])).ToString());
        }
    }
}
