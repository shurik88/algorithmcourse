using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6.Task6_4
{
    public class Task6_4
    {
        public static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt").ToList();

            var arraySize = Int32.Parse(content[0]);
            if(arraySize == 0)
            {
                File.WriteAllText("output.txt", "0");
                return;
            }

            var treeDef = content.GetRange(0, arraySize + 1);
            var commands = content[arraySize + 2];

        }

        public class BinaryTreeToDelete
        {
            public BinaryTreeToDelete(int maxHeight, int maxValue)
            {

            }
        }
    }
}
