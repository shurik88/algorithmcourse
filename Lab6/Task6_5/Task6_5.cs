using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6.Task6_5
{
    public class Task6_5
    {
        public static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt");

            if (content.Length < 3)
            {
                File.WriteAllText("output.txt", "YES");
                return;
            }
            var first = content[1].Split(new[] {' '});

            var stack = new Stack<Info>();
            stack.Push(new Info { Key = int.Parse(first[0]), Left = int.Parse(first[1]), Right = int.Parse(first[2])});
            while (stack.Any())
            {
                var elem = stack.Pop();
                if (elem.Left != 0)
                {
                    var left = content[elem.Left].Split(new[] { ' ' });
                    var leftKey = int.Parse(left[0]);
                    if ( leftKey >= elem.Key || (elem.MaxRight.HasValue && elem.MaxRight.Value <= leftKey) || (elem.MinLeft.HasValue && elem.MinLeft.Value >= leftKey))
                    {
                        File.WriteAllText("output.txt", "NO");
                        return;
                    }
                    stack.Push(new Info
                    {
                        Key = leftKey,
                        Left = int.Parse(left[1]),
                        Right = int.Parse(left[2]),
                        MaxRight = elem.Key,
                        MinLeft = elem.MinLeft
                    });
                }
                if (elem.Right != 0)
                {
                    var right = content[elem.Right].Split(new[] { ' ' });
                    var rightKey = int.Parse(right[0]);
                    if (rightKey <= elem.Key || (elem.MaxRight.HasValue && elem.MaxRight.Value <= rightKey) || (elem.MinLeft.HasValue && elem.MinLeft.Value >= rightKey))
                    {
                        File.WriteAllText("output.txt", "NO");
                        return;
                    }
                    stack.Push(new Info
                    {
                        Key = rightKey,
                        Left = int.Parse(right[1]),
                        Right = int.Parse(right[2]),
                        MinLeft = elem.Key,
                        MaxRight = elem.MaxRight
                    });
                }
            }

            File.WriteAllText("output.txt", "YES");
        }

        public struct Info
        {
            public int Key { get; set; }

            public int Left { get; set; }

            public int Right { get; set; }

            public int? MaxRight { get; set; }

            public int? MinLeft { get; set; }
        }
    }
}
