using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lab8.Task8_2
{
    public class Task8_2
    {
        public static void Main(string[] args)
        {
            //var content = File.ReadAllLines("input.txt");


            using (var writer = new StreamWriter("output.txt"))
            {
                using (var reader = new StreamReader("input.txt"))
                {
                    var line = reader.ReadLine();
                    var size = int.Parse(line);
                    string prevKey = null;
                    var arr = new LinkedList<Elem>[size];
                    while ((line = reader.ReadLine()) != null)
                    {
                        var command = line.Split(new[] {' '});
                        var x = command[1];
                        var hashCode = GetHashCode(x, size);
                        var list = arr[hashCode];
                        LinkedListNode<Elem> elem;
                        switch (command[0])
                        {
                            case "get":
                                if (list == null)
                                {
                                    writer.WriteLine("<none>");
                                    break;
                                }

                                elem = list.Find(new Elem {Key = x});
                                writer.WriteLine(elem != null ? elem.Value.Value : "<none>");
                                break;
                            case "put":
                                
                                if (list == null)
                                {
                                    list = new LinkedList<Elem>();
                                    list.AddFirst(new Elem { Key = x, Value = command[2], PrevKey = prevKey });
                                    arr[hashCode] = list;                                    
                                    if (!string.IsNullOrEmpty(prevKey))
                                    {
                                        var prevElem = FindWithKey(prevKey, arr);
                                        prevElem.Value =
                                            new Elem
                                            {
                                                NextKey = x,
                                                Value = prevElem.Value.Value,
                                                Key = prevElem.Value.Key,
                                                PrevKey = prevElem.Value.PrevKey
                                            };
                                    }
                                    prevKey = x;
                                }
                                else if (!list.Contains(new Elem {Key = x}))
                                {
                                    list.AddLast(new Elem { Key = x, Value = command[2], PrevKey = prevKey });
                                    if (!string.IsNullOrEmpty(prevKey))
                                    {
                                        var prevElem = FindWithKey(prevKey, arr);
                                        prevElem.Value =
                                            new Elem
                                            {
                                                NextKey = x,
                                                Value = prevElem.Value.Value,
                                                Key = prevElem.Value.Key,
                                                PrevKey = prevElem.Value.PrevKey
                                            };
                                        //prevElem.NextKey = x;
                                    }
                                    prevKey = x;
                                }
                                else
                                {
                                    var updated = list.Find(new Elem { Key = x});
                                    updated.Value =
                                        new Elem
                                        {
                                            Value = command[2],
                                            Key = updated.Value.Key,
                                            NextKey = updated.Value.NextKey,
                                            PrevKey = updated.Value.PrevKey
                                        };
                                }
                                break;
                            case "delete":
                                if (list != null && list.Contains(new Elem { Key = x}))
                                {
                                    var deleted = list.First(y => y.Key == x);

                                    if (!string.IsNullOrEmpty(deleted.PrevKey))
                                    {
                                        var prevElem = FindWithKey(deleted.PrevKey, arr);
                                        prevElem.Value =
                                            new Elem
                                            {
                                                NextKey = deleted.NextKey,
                                                Value = prevElem.Value.Value,
                                                Key = prevElem.Value.Key,
                                                PrevKey = prevElem.Value.PrevKey
                                            };
                                        //prevElem.NextKey = deleted.NextKey;
                                    }
                                    if (!string.IsNullOrEmpty(deleted.NextKey))
                                    {
                                        var nextElem = FindWithKey(deleted.NextKey, arr);
                                        nextElem.Value =
                                            new Elem
                                            {
                                                NextKey = nextElem.Value.NextKey,
                                                Value = nextElem.Value.Value,
                                                Key = nextElem.Value.Key,
                                                PrevKey = deleted.PrevKey
                                            };
                                        //nextElem.PrevKey = deleted.PrevKey;
                                    }

                                    list.Remove(deleted);

                                    if (deleted.Key == prevKey)
                                        prevKey = deleted.PrevKey;
                                }
                                break;
                            case "next":
                                if (list == null)
                                {
                                    writer.WriteLine("<none>");
                                    break;
                                }

                                elem = list.Find(new Elem { Key = x });
                                if (elem == null || string.IsNullOrEmpty(elem.Value.NextKey))
                                {
                                    writer.WriteLine("<none>");
                                    break;
                                }
                                var afterX = FindWithKey(elem.Value.NextKey, arr);
                                writer.WriteLine(afterX.Value.Value);
                                break;
                            case "prev":
                                if (list == null)
                                {
                                    writer.WriteLine("<none>");
                                    break;
                                }
                                elem = list.Find(new Elem { Key = x });
                                if (elem == null || string.IsNullOrEmpty(elem.Value.PrevKey))
                                {
                                    writer.WriteLine("<none>");
                                    break;
                                }

                                elem = list.Find(new Elem { Key = x });
                                var beforeX = FindWithKey(elem.Value.PrevKey, arr);
                                writer.WriteLine(beforeX.Value.Value);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(string.Format("Unknown command: {0}", line));
                        }
                    }
                }
                    
            }
        }

        private static LinkedListNode<Elem> FindWithKey(string key, LinkedList<Elem>[] table)
        {
            var hashCode = GetHashCode(key, table.Length);
            var list = table[hashCode];
            return list.Find(new Elem { Key = key});
        }

        private static int GetHashCode(string key, int hashTableSize)
        {
            //var hashKey = 0;
            //for (var i = key.Length - 1; i >= 0; --i)
            //{
            //    hashKey+=
            //}
            var hash = key.GetHashCode();
            return (hash < 0 ? -hash : hash) % hashTableSize;
        }

        public struct Elem
        {
            public string Key { get; set; }

            public string Value { get; set; }

            public string NextKey { get; set; }

            public string PrevKey { get; set; }

            public override bool Equals(object obj)
            {
                var node = (Elem) obj;
                return Key == node.Key;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }
    }
}
