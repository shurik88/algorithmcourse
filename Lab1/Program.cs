using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = Enumerable.Range(1, 2198).OrderByDescending(x => x);
            File.WriteAllLines(@"D:\Projects\AlgorithmCourse\Lab1\Task1_5\input.txt", new string[] { array.Count().ToString(), string.Join(" ", array) });
            return;
            var contentInput = File.ReadAllLines(@"D:\Projects\AlgorithmCourse\Lab1\Task1_5\input.txt");
            //var contentOutput = File.ReadAllLines("Task1_5\\output.txt");
            var arrayInput = contentInput[1].Split(new char[] { ' ' }).Select(x => long.Parse(x)).ToArray();
            var arrayOutput = new long[arrayInput.Length];

            var reader = new StreamReader(@"D:\Projects\AlgorithmCourse\Lab1\Task1_5\output.txt");
            var template = "Swap elements at indices {0} and {1}.";
            while(!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if(line == "No more swaps needed.")
                {
                    arrayOutput = reader.ReadLine().Split(new char[] { ' ' }).Select(x => long.Parse(x)).ToArray();
                }
                else
                {
                    var indexes = line.ParseExact(template).Select(x => long.Parse(x)).ToArray();
                    var temp = arrayInput[indexes[0] - 1];
                    arrayInput[indexes[0] - 1] = arrayInput[indexes[1] - 1];
                    arrayInput[indexes[1] - 1] = temp;
                }

            }
            var res = arrayInput.ToList().SequenceEqual(arrayOutput.ToList());
        }

        

    }

    static class StringExtensions
    {
        public static string[] ParseExact(this string data, string format)
        {
            return ParseExact(data, format, false);
        }

        public static string[] ParseExact(this string data, string format, bool ignoreCase)
        {
            string[] values;

            if (TryParseExact(data, format, out values, ignoreCase))
                return values;
            else
                throw new ArgumentException("Format not compatible with value.");
        }
        public static bool TryExtract(
        this string data,
        string format,
        out string[] values)
        {
            return TryParseExact(data, format, out values, false);
        }

        public static bool TryParseExact(
            this string data,
            string format,
            out string[] values,
            bool ignoreCase)
        {
            int tokenCount = 0;
            format = Regex.Escape(format).Replace("\\{", "{");

            for (tokenCount = 0; ; tokenCount++)
            {
                string token = string.Format("{{{0}}}", tokenCount);
                if (!format.Contains(token)) break;
                format = format.Replace(token,
                    string.Format("(?'group{0}'.*)", tokenCount));
            }

            RegexOptions options =
                ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;

            Match match = new Regex(format, options).Match(data);

            if (tokenCount != (match.Groups.Count - 1))
            {
                values = new string[] { };
                return false;
            }
            else
            {
                values = new string[tokenCount];
                for (int index = 0; index < tokenCount; index++)
                    values[index] =
                        match.Groups[string.Format("group{0}", index)].Value;
                return true;
            }
        }
    }
}
