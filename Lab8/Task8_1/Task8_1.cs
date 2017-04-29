using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab8.Task8_1
{
    public static class Task8_1
    {
        public static void Main(string[] args)
        {
            //var content = File.ReadAllLines("input.txt");
           

            //var size = Int32.Parse(content[0]);
            using (var writer = new StreamWriter("output.txt"))
            {
                using (var reader = new StreamReader("input.txt"))
                {
                    var line = reader.ReadLine();
                    //var dict = new HashSet<long>();
                    var size = int.Parse(line);
                    var arr = new LinkedList<long>[size];
                    //var dict = new Dictionary<long, bool>(int.Parse(line) * 3);
                    while ((line = reader.ReadLine()) != null)
                    {
                        var command = line.Split(new[] {' '});
                        if(command.Length != 2)
                            throw new ArgumentNullException(string.Format("Unknown command: {0}", line));
                        var arg = long.Parse(command[1]);
                        var hashCode = (int)((arg < 0 ? -arg : arg) % size);
                        var list = arr[hashCode];
                        switch (command[0])
                        {
                            case "A":
                                if (list == null)
                                {
                                    list = new LinkedList<long>();
                                    list.AddFirst(arg);
                                    arr[hashCode] = list;
                                }
                                else if (!list.Contains(arg))
                                    list.AddLast(arg);
                                //if(!dict.ContainsKey(arg))
                                //    dict.Add(arg,true);
                                //Svar hash = arg.GetHashCode();
                                break;
                            case "D":
                                if (list != null && list.Contains(arg))
                                    list.Remove(arg);
                                //if (dict.ContainsKey(arg))
                                //    dict.Remove(arg);
                                break;
                            case "?":
                                writer.WriteLine(list != null && list.Contains(arg) ? "Y" : "N");
                                //writer.WriteLine(dict.ContainsKey(arg) ? "Y" : "N");

                                break;
                            default:
                                throw new ArgumentNullException(string.Format("Unknown command: {0}", command[0]));
                        }
                    }
                }
            }
            
        }

        private static int GetHashCode(long value)
        {
            return (int)(value % 100000);
        }
    }
}
