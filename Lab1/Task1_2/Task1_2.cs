using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Task1_2
    {
        static void Main(string[] args)
        {
            var content = File.ReadAllText("input.txt");
            var numbers = content.Split(new[] {' ' });
            var number1 = long.Parse(numbers[0]);
            var number2 = long.Parse(numbers[1]);
            File.WriteAllText("output.txt", (number1 + number2 * number2).ToString());
        }
    }
}
