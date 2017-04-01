using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLab
{
    public static class DataGenerator
    {
        static Random rand = new Random();

        public static int[] GenerateArray(int min, int max, int size)
        {
            return Enumerable.Range(0, size).Select(x => rand.Next(min, max)).ToArray();
        }
    }
}
