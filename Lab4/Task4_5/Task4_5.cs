using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Task4_5
{
    class Task4_5
    {
        public static void Main(string[] args)
        {
            
            var queue = new Queue<ushort>();
            var register = Enumerable.Range('a', 'z' - 'a' + 1).ToDictionary(x => (Char)x, x => (ushort)0);
            var commands = File.ReadAllLines("input.txt");
            var labels = GetLabels(commands);
            const long mod = 65536;
            using (var writer = new StreamWriter("output.txt"))
            {
                for (var i = 0; i < commands.Length; ++i)
                {
                    var line = commands[i];
                    //Console.WriteLine(line);
                    try
                    {
                        if (line == "Q")
                        {
                            return;
                        }
                        else if (line.StartsWith(":"))
                        {

                        }
                        else if (line.StartsWith("J"))
                        {
                            var jumpTo = labels[ParseJumpLabelCommand(line)];
                            i = jumpTo--;
                        }
                        else if (line == "+")
                        {
                            var first = queue.Count != 0 ? queue.Dequeue() : 0;
                            var second = queue.Count != 0 ? queue.Dequeue() : 0;
                            queue.Enqueue(Convert.ToUInt16((first + second) % mod));
                        }
                        else if (line == "*")
                        {
                            var first = (long)(queue.Count != 0 ? queue.Dequeue() : 0);
                            var second = queue.Count != 0 ? queue.Dequeue() : 0;
                            var value = (first * second) % mod;
                            queue.Enqueue(Convert.ToUInt16(value));
                        }
                        else if (line == "-")
                        {
                            var first = queue.Count != 0 ? queue.Dequeue() : 0;
                            var second = queue.Count != 0 ? queue.Dequeue() : 0;
                            queue.Enqueue(Convert.ToUInt16(((first - second) & ushort.MaxValue) % mod));
                        }
                        else if (line == "/")
                        {
                            var first = queue.Count != 0 ? queue.Dequeue() : 0;
                            var second = queue.Count != 0 ? queue.Dequeue() : 0;
                            queue.Enqueue(Convert.ToUInt16(second == 0 ? 0 : first / second));
                        }
                        else if (line == "%")
                        {
                            var first = queue.Count != 0 ? queue.Dequeue() : 0;
                            var second = queue.Count != 0 ? queue.Dequeue() : 0;
                            queue.Enqueue(Convert.ToUInt16(second == 0 ? 0 : first % second));
                        }
                        else if (line == "P")
                        {
                            writer.WriteLine(queue.Count != 0 ? queue.Dequeue() : 0);
                        }
                        else if (line.StartsWith("P"))
                        {
                            writer.WriteLine(register[line[1]]);
                        }
                        else if (line == "C")
                        {
                            var value = queue.Dequeue() % 256;
                            if (value == 10)
                                writer.WriteLine();
                            else
                                writer.Write((Char)value);
                        }
                        else if (line.StartsWith("C"))
                        {
                            var value = register[line[1]] % 256;
                            if (value == 10)
                                writer.WriteLine();
                            else
                                writer.Write((Char)value);
                        }
                        else if (line.StartsWith("Z"))
                        {
                            if (register[line[1]] == 0)
                            {
                                var jumpTo = labels[line.Substring(2)];
                                i = jumpTo--;
                            }
                        }
                        else if (line.StartsWith("G"))
                        {
                            if (register[line[1]] > register[line[2]])
                            {
                                var jumpTo = labels[line.Substring(3)];
                                i = jumpTo--;
                            }
                        }
                        else if (line.StartsWith("E"))
                        {
                            if (register[line[1]] == register[line[2]])
                            {
                                var jumpTo = labels[line.Substring(3)];
                                i = jumpTo--;
                            }
                        }
                        else if (line.StartsWith(">"))
                        {
                            if (queue.Count == 0)
                            {

                            }
                            register[line[1]] = queue.Dequeue();
                        }
                        else if (line.StartsWith("<"))
                        {
                            queue.Enqueue(register[line[1]]);
                        }
                        else //number
                        {
                            queue.Enqueue(ushort.Parse(line));
                        }
                    }
                    catch(Exception e)
                    {
                        writer.WriteLine(line);
                    }
                    
                }
            }
                
        }

        private static Dictionary<string, int> GetLabels(string[] commands)
        {
            var labels = new Dictionary<string, int>();
            for (var i = 0; i < commands.Length; ++i)
            {
                var line = commands[i];
                Console.WriteLine(line);
                if (line.StartsWith(":"))
                {
                    var label = ParseLabelCommand(line);
                    labels.Add(label, i);
                }
            }
            return labels;
        }

        private static string ParseLabelCommand(string command)
        {
            return command.Substring(1, command.Length - 1);
        }
        private static string ParseJumpLabelCommand(string command)
        {
            return command.Substring(1, command.Length - 1);
        }
    }
}
