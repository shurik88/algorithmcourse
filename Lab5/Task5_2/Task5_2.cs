using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Task5_2
{
    public class Task5_2
    {
        public static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt");
            var size = Int32.Parse(content[0]);
            var insertCommands = content.Count(x => x.StartsWith("A"));
            using (var writer = new StreamWriter("output.txt"))
            {
                var currCommand = 0;
                var queue = new MinPriorityQueue(size, insertCommands);
                for(var i = 1; i < content.Length; ++i)
                {
                    var line = content[i];
                    if (line == "X")
                    {
                        writer.WriteLine(queue.IsEmpty ? "*" : queue.ExtractMin().ToString());
                    }
                    else if (line.StartsWith("A"))
                    {
                        var key = Int32.Parse(line.Split(new char[] { ' ' })[1]);
                        queue.Insert(key, currCommand);
                        currCommand++;
                    }
                    else if (line.StartsWith("D"))
                    {
                        var arguments = line.Split(new char[] { ' ' });
                        queue.DecreaseHeapKey(queue.GetIndex(Int32.Parse(arguments[1]) - 1), Int32.Parse(arguments[2]));
                    }
                    else
                    {
                        throw new ArgumentException(string.Format("Unknown command:{0}", line));
                    }
                }
                
            }
            
            //using (var reader = new StreamReader("input.txt"))
            //{
            //    var size = reader.ReadLine();//commands count
            //    if (size == null)
            //        throw new ArgumentException("Invalid file format");
            //    string line;
            //    using (var writer = new StreamWriter("output.txt"))
            //    {
            //        var currCommand = 0;
            //        var queue = new MinPriorityQueue(Int32.Parse(size)); 
            //        while ((line = reader.ReadLine()) != null)
            //        {
            //            if(line == "X")
            //            {
            //                writer.WriteLine(queue.IsEmpty ? "*" : queue.ExtractMin().ToString());
            //            }
            //            else if(line.StartsWith("A"))
            //            {
            //                var key = Int32.Parse(line.Split(new char[] { ' ' })[1]);
            //                queue.Insert(key, currCommand);
            //                currCommand++;
            //            }
            //            else if(line.StartsWith("D"))
            //            {
            //                var arguments = line.Split(new char[] { ' ' });
            //                queue.DecreaseHeapKey(queue.GetIndex(Int32.Parse(arguments[1])), Int32.Parse(arguments[2]) - 1);
            //            }
            //            else
            //            {
            //                throw new ArgumentException(string.Format("Unknown command:{0}", line));
            //            }

            //        }
            //    }
            //}

        }

        public struct Elem
        {
            public int Key { get; set; }

            public int Command { get; set; }
        }

        public class MinPriorityQueue
        {
            private readonly Elem[] _arr;

            private readonly int[] _indexes;

            private int _size = 0;
            private readonly int _maxSize;
            public MinPriorityQueue(int size, int commandInsert)
            {
                if (size <= 0)
                    throw new ArgumentException("Invalid size");
                _maxSize = size;
                _arr = new Elem[size];
                _indexes = new int[commandInsert];
            }

            public bool IsEmpty { get { return _size == 0; } }

            public int Size { get { return _size; } }
            public int Min
            {
                get
                {
                    if (_size == 0)
                        throw new ArgumentException("Queue is empty");
                    return _arr[0].Key;
                }
            }

            public int GetIndex(int command)
            {
                //if (!_commandIndexes.ContainsKey(command))
                //    throw new ArgumentException("Unknown insert command number");
                return _indexes[command];
                //for (var i = 0; i < _size; ++i)
                //    if (_arr[i].Command == command)
                //        return i;
                //throw new ArgumentException("Unknown insert command number");
            }

            public void Insert(int key, int command)
            {
                if(_size == _maxSize)
                    throw new ArgumentException("Queue overflow");

                _size++;
                _arr[_size - 1] = new Elem { Command = command, Key = Int32.MaxValue };
                _indexes[command] = _size - 1;
                DecreaseHeapKey(_size - 1, key);
            }

            public void DecreaseHeapKey(int index, int newKey)
            {
                if (_arr[index].Key < newKey)
                    throw new ArgumentException("New key is bigger than old key");
                _arr[index].Key = newKey;

                while(index > 0 && _arr[GetParent(index)].Key > _arr[index].Key)
                {
                    Swap(index, GetParent(index));
                    index = GetParent(index);
                }
            }

            public int ExtractMin()
            {
                var min = Min;

                //var command = _arr[0].Command;
                //_commandIndexes.Remove(command);

                _arr[0] = _arr[_size - 1];
                _size--;
                _indexes[_arr[0].Command] = 0;
                //_commandIndexes[_arr[0].Command] = 0;

                MinHeapify(0, _size);
                return min;
            }

            private int GetParent(int leaf)
            {
                return (leaf - 1) / 2;
            }

            private int GetLeft(int root)
            {
                return 2 * root + 1;
            }

            private int GetRight(int root)
            {
                return 2 * root + 2;
            }

            private void Swap(int index1, int index2)
            {
                _indexes[_arr[index2].Command] = index1;
                _indexes[_arr[index1].Command] = index2;

                var temp = _arr[index1];
                _arr[index1] = _arr[index2];
                _arr[index2] = temp;
                
            }

            private void MinHeapify(int i, int? heapSize = null)
            {
                var length = heapSize.HasValue ? heapSize.Value : _arr.Length;
                var left = GetLeft(i);
                var right = GetRight(i);
                var smallest = i;
                if (left < length && _arr[smallest].Key > _arr[left].Key)
                    smallest = left;
                if (right < length && _arr[smallest].Key > _arr[right].Key)
                    smallest = right;

                if (smallest != i)
                {
                    Swap(i, smallest);
                    MinHeapify(smallest, heapSize);
                }
            }
        }
    }
}
