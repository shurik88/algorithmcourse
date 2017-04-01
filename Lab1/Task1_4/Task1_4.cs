using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Task1_4
{
    class Task1_4
    {
        static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt");
            var array = content[1].Split(new char[] { ' ' }).Select(x => decimal.Parse(x, CultureInfo.InvariantCulture)).ToArray();
            var indexes = Enumerable.Range(1, array.Length).ToArray();
            for (var i = 1; i < array.Length; ++i)
            {
                var j = i - 1;
                while (j >= 0 && array[j + 1] < array[j])
                {
                    var temp = array[j + 1];
                    array[j + 1] = array[j];
                    array[j] = temp;
                    var tempIndex = indexes[j + 1];
                    indexes[j + 1] = indexes[j];
                    indexes[j] = tempIndex;
                    j--;
                }
            }
            
            File.WriteAllText("output.txt", string.Join(" ", indexes.First(), indexes[indexes.Length / 2], indexes.Last()));
        }
    }
}
