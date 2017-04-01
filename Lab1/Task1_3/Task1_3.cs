using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Task1_3
{
    class Task1_3
    {
        static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt");
            var array = content[1].Split(new char[] { ' ' }).Select(x => long.Parse(x)).ToArray();
            var diplacements = new int[array.Length];
            diplacements[0] = 1;
            for (var i = 1; i < array.Length; ++i)
            {
                var j = i - 1;
                while (j >= 0 && array[j + 1] < array[j])
                {
                    var temp = array[j + 1];
                    array[j + 1] = array[j];
                    array[j] = temp;
                    j--;
                }
                diplacements[i] = j + 2;
            }
            
            File.WriteAllLines("output.txt", new string[] { string.Join(" ", diplacements), string.Join(" ", array) });
        }
    }
}
