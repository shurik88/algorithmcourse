using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.Task7_1
{
    public class Task7_1
    {
        public static void Main(string[] args)
        {

            try
            {
                var content = File.ReadAllLines("input.txt");
                var size = Int32.Parse(content[0]);
                if (size == 0 || size == 1)
                {
                    File.WriteAllText("output.txt", "0");
                    return;
                }

                var heights = new int[content.Length];//Enumerable.Range(0, content.Length).Select(x => -1).ToArray();
                var balances = new int[content.Length];

                for(var i = content.Length - 1; i > 0; i--)
                {
                    var nodeDef = content[i].Split(new[] { ' ' });
                    var left = Int32.Parse(nodeDef[1]);
                    var right = Int32.Parse(nodeDef[2]);
                    heights[i] = Math.Max(heights[left], heights[right]) + 1;
                    balances[i] = - heights[left] + heights[right];



                }

                using (var writer = new StreamWriter("output.txt"))
                {
                    for (var i = 1; i < balances.Length; ++i)
                        writer.WriteLine(balances[i]);
                }
            }
            catch (Exception e)
            {
                File.WriteAllText("output.txt", e.Message);
            }


        }
    }
}
