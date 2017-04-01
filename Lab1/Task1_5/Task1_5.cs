using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Task1_5
{
    class Task1_5
    {
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var content = File.ReadAllLines("input.txt");
            var array = content[1].Split(new char[] { ' ' }).Select(x => long.Parse(x)).ToArray();
            var streamWritter = new StreamWriter("output.txt");
            var builder = new StringBuilder();
            for (var i = 0; i < array.Length; ++i)
            {
                var min = array[i];
                var index = i;
                for(var j = i + 1; j < array.Length; ++j)
                {
                    if(array[j] < min)
                    {
                        min = array[j];
                        index = j;
                    }
                }
                if(index != i)
                {
                    var temp = array[index];
                    array[index] = array[i];
                    array[i] = temp;
                    streamWritter.WriteLine(string.Format("Swap elements at indices {0} and {1}.", i + 1, index + 1));
                }
                //streamWritter.WriteLine(string.Format("Swap elements at indices {0} and {1}.", j + 2, i + 1));

            }
            streamWritter.WriteLine("No more swaps needed.");
            streamWritter.WriteLine(string.Join(" ", array));
            streamWritter.Close();
            //builder.AppendLine("No more swaps needed.");
            //builder.AppendLine(string.Join(" ", array));
            //File.WriteAllText("output.txt", builder.ToString());
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            File.WriteAllText("time.txt", elapsedMs.ToString());
        }
    }
}
