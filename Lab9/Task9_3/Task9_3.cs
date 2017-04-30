using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Task9_3
{
    class Task9_3
    {
        public static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt");
            var pattern = content[0];
            var text = content[1];
            if(pattern.Length > text.Length)
            {
                File.WriteAllText("input.txt", "0");
                return;
            }
            var indexes = new List<int>();
            //PreCalculateP();
            var h = PreCalculateH(text, pattern);
            var patternHash = CalculateHash(pattern);//Math.Abs(pattern.GetHashCode());
            for (var i = 0; i < text.Length - pattern.Length + 1; ++i)
            {
                //var compareString = text.Substring(i, pattern.Length);
                //var hash = CalculateHash(compareString);//Math.Abs(compareString.GetHashCode());
                var hash = h[i];
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
                i += pattern.Length - 1;
            }

            using (var writer = new StreamWriter("output.txt"))
            {
                writer.WriteLine(indexes.Count);
                writer.WriteLine(string.Join(" ", indexes));
            }
        }

        //private static ulong CalculateHash(string s)
        //{
        //    ulong hash = 0;
        //    for(var i = 0; i < s.Length; ++i)
        //    {
        //        hash += (ulong)(s[i] - 'a' + 1) * Pow[i];//(ulong)((s[i] >= 'A' ? s[i] - 'A' : s[i] - 'a') + 1) * Pow[i];
        //    }
        //    return hash;
        //}

        private static ulong CalculateHash(string s)
        {
            ulong hash = (ulong)(s[0] - 'a' + 1) * Pow[0];
            for (var i = 1; i < s.Length; ++i)
            {
                hash = (hash * p + (ulong)(s[i] - 'a' + 1));//(ulong)((s[i] >= 'A' ? s[i] - 'A' : s[i] - 'a') + 1) * Pow[i];
            }
            return hash;// % m;
        }

        private static ulong[] Pow;
        public const long p = 53;
        public const ulong m = 10000079;
        private static ulong[] PreCalculateP(int size)
        {
            Pow = new ulong[size];
            Pow[0] = 1;
            for(var  i = 1; i < Pow.Length; ++i)
            {
                Pow[i] = Pow[i - 1] * p;
            }
            return Pow;
        }

        private static ulong[] PreCalculateH(string text, string pattern)
        {
            var pow = PreCalculateP(pattern.Length + 1);

            var h = new ulong[text.Length];
            var length = pattern.Length;
            h[0] = CalculateHash(text.Substring(0, length));
            for(var i = 1; i < text.Length - length + 1; ++i)
            {
                h[i] = (h[i - 1] * p + (ulong)(text[i + length - 1] - 'a' + 1) - (ulong)(text[i - 1] - 'a' + 1) * pow[length]); //% m;
            }
            return h;
        }
    }


}
