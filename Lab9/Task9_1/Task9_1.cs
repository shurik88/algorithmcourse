using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Task9_1
{
    public class Task9_1
    {
        public static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt");
            var pattern = content[0];
            var text = content[1];
            var indexes = new List<int>();
            var patternHash = Math.Abs(pattern.GetHashCode());
            for (var i = 0; i < text.Length - pattern.Length + 1; ++i)
            {
                var compareString = text.Substring(i, pattern.Length);
                var hash = Math.Abs(compareString.GetHashCode());
                if (hash != patternHash)
                    continue;
                var exists = true;
                for (var j = 0; j < pattern.Length; ++j)
                {
                    if (pattern[j] != text[i + j])
                    {
                        exists = false;
                        break;
                    }
                }
                if (!exists)
                    continue;
                indexes.Add(i + 1);
                //i += pattern.Length - 1;
            }

            using (var writer = new StreamWriter("output.txt"))
            {
                writer.WriteLine(indexes.Count);
                writer.WriteLine(string.Join(" ", indexes));
            }
        }
    }
}
